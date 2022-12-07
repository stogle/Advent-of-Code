using System.Globalization;

namespace AdventOfCode2022.Day20;

public class GrovePositioningSystem : IDay
{
    public int DayNumber => 20;

    public string PartOne(TextReader input)
    {
        List<Node> list = ReadInput(input);

        Node? start = Mix(list);
        long result = Decrypt(start, list.Count);
        return result.ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        const long decryptionKey = 811589153L;
        List<Node> list = ReadInput(input, decryptionKey);

        const int mixCount = 10;
        Node? start = Mix(list, mixCount);
        long result = Decrypt(start, list.Count);
        return result.ToString(CultureInfo.InvariantCulture);
    }

    private static List<Node> ReadInput(TextReader input, long decryptionKey = 1)
    {
        var list = new List<Node>();

        Node? previous = null;
        while (input.ReadLine() is { } line)
        {
            var node = new Node
            {
                Value = long.Parse(line, CultureInfo.InvariantCulture) * decryptionKey,
                Previous = previous
            };
            if (previous != null)
            {
                previous.Next = node;
            }
            list.Add(node);
            previous = node;
        }

        Node first = list.First();
        Node last = list.Last();
        last.Next = first;
        first.Previous = last;

        return list;
    }

    private static Node? Mix(List<Node> list, int mixCount = 1)
    {
        Node? start = null;
        for (int i = 0; i < mixCount; i++)
        {
            foreach (Node node in list)
            {
                long? value = node.Value % (list.Count - 1);
                Node destination = node;
                if (value == 0)
                {
                    start = node;
                    continue;
                }

                // Remove node
                node.Previous!.Next = node.Next;
                node.Next!.Previous = node.Previous;

                if (value > 0)
                {
                    do
                    {
                        destination = destination.Next!;
                    } while (--value > 0);
                }
                else
                {
                    do
                    {
                        destination = destination.Previous!;
                    } while (++value <= 0);
                }

                // Insert node
                node.Previous = destination;
                node.Next = destination.Next;
                destination.Next!.Previous = node;
                destination.Next = node;
            }
        }

        return start;
    }

    private static long Decrypt(Node? zero, int listCount)
    {
        long result = 0L;
        int[] coordinateIndices =
        {
            1000 % listCount,
            2000 % listCount,
            3000 % listCount
        };
        Array.Sort(coordinateIndices);
        int coordinate = 0;
        int i = 0;
        Node next = zero!;
        while (coordinate < coordinateIndices.Length)
        {
            while (coordinate < coordinateIndices.Length && i == coordinateIndices[coordinate])
            {
                result += next.Value ?? 0;
                coordinate++;
            }

            i++;
            next = next.Next!;
        }

        return result;
    }
}
