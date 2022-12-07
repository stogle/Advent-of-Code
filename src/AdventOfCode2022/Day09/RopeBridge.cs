using System.Globalization;

namespace AdventOfCode2022.Day09;

public class RopeBridge : IDay
{
    public int DayNumber => 9;

    public string PartOne(TextReader input)
    {
        (int X, int Y) head = (0, 0);
        (int X, int Y) tail = (0, 0);
        var visited = new HashSet<(int X, int Y)> { tail };

        while (input.ReadLine() is { } line)
        {
            char direction = line[0];
            int steps = int.Parse(line[2..], CultureInfo.InvariantCulture);

            for (int i = 0; i < steps; i++)
            {
                switch (direction)
                {
                    case 'U':
                        head.Y++;
                        break;
                    case 'D':
                        head.Y--;
                        break;
                    case 'L':
                        head.X--;
                        break;
                    case 'R':
                        head.X++;
                        break;
                }

                int x = head.X - tail.X;
                int xAbs = Math.Abs(x);
                int y = head.Y - tail.Y;
                int yAbs = Math.Abs(y);
                if (xAbs > 1 || yAbs > 1 || xAbs + yAbs > 2)
                {
                    tail.X += Math.Sign(x);
                    tail.Y += Math.Sign(y);
                }
                visited.Add(tail);
            }
        }

        return visited.Count.ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        (int X, int Y) head = (0, 0);
        (int X, int Y)[] tails = Enumerable.Repeat((0, 0), 9).ToArray();
        var visited = new HashSet<(int X, int Y)> { tails.Last() };

        while (input.ReadLine() is { } line)
        {
            char direction = line[0];
            int steps = int.Parse(line[2..], CultureInfo.InvariantCulture);

            for (int i = 0; i < steps; i++)
            {
                switch (direction)
                {
                    case 'U':
                        head.Y++;
                        break;
                    case 'D':
                        head.Y--;
                        break;
                    case 'L':
                        head.X--;
                        break;
                    case 'R':
                        head.X++;
                        break;
                }

                (int X, int Y) previous = head;
                for (int j = 0; j < tails.Length; j++)
                {
                    int x = previous.X - tails[j].X;
                    int xAbs = Math.Abs(x);
                    int y = previous.Y - tails[j].Y;
                    int yAbs = Math.Abs(y);
                    if (xAbs > 1 || yAbs > 1 || xAbs + yAbs > 2)
                    {
                        tails[j].X += Math.Sign(x);
                        tails[j].Y += Math.Sign(y);
                    }

                    previous = tails[j];
                }
                visited.Add(tails.Last());
            }
        }

        return visited.Count.ToString(CultureInfo.InvariantCulture);
    }
}
