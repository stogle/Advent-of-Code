namespace AdventOfCode2022.Day05;

public class SupplyStacks : IDay
{
    public int DayNumber => 5;

    public string PartOne(TextReader input)
    {
        Stack<char>[] stacks = ReadInput(input);

        // Read rearrangement procedure
        while (input.ReadLine() is { } line)
        {
            int[] move = line.Split(' ').Where((_, i) => i % 2 == 1).Select(int.Parse).ToArray();
            int count = move[0];
            int from = move[1];
            int to = move[2];
            for (int i = 0; i < count; i++)
            {
                stacks[to - 1].Push(stacks[from - 1].Pop());
            }
        }

        return new string(stacks.Select(stack => stack.Peek()).ToArray());
    }

    public string PartTwo(TextReader input)
    {
        Stack<char>[] stacks = ReadInput(input);

        // Read rearrangement procedure
        while (input.ReadLine() is { } line)
        {
            int[] move = line.Split(' ').Where((_, i) => i % 2 == 1).Select(int.Parse).ToArray();
            int count = move[0];
            int from = move[1];
            int to = move[2];

            Stack<char> temp = new();
            for (int i = 0; i < count; i++)
            {
                temp.Push(stacks[from - 1].Pop());
            }

            for (int i = 0; i < count; i++)
            {
                stacks[to - 1].Push(temp.Pop());
            }
        }

        return new string(stacks.Select(stack => stack.Peek()).ToArray());
    }

    private static Stack<char>[] ReadInput(TextReader input)
    {
        // Read diagram
        var diagram = new List<List<char>>();
        while (input.ReadLine() is { } line && line.Contains('['))
        {
            var diagramLine = new List<char>();
            for (int i = 0; i < line.Length; i += 4)
            {
                diagramLine.Add(line[i + 1]);
            }

            diagram.Add(diagramLine);
        }

        input.ReadLine();

        // Convert to stacks
        Stack<char>[] stacks = Enumerable.Range(0, diagram.Max(line => line.Count))
            .Select(_ => new Stack<char>())
            .ToArray();
        for (int i = diagram.Count - 1; i >= 0; i--)
        {
            List<char> diagramLine = diagram[i];
            for (int j = 0; j < diagramLine.Count; j++)
            {
                char c = diagramLine[j];
                if (char.IsLetter(c))
                {
                    stacks[j].Push(c);
                }
            }
        }

        return stacks;
    }
}
