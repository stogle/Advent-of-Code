using System.Globalization;

namespace AdventOfCode2022.Day18;

public class BoilingBoulders : IDay
{
    public int DayNumber => 18;

    public string PartOne(TextReader input)
    {
        const int length = 20;
        var cubes = new List<int[]>();
        var space = new bool[length, length, length];
        while (input.ReadLine() is { } line)
        {
            int[] cube = line.Split(",").Select(int.Parse).ToArray();
            cubes.Add(cube);
            space[cube[0], cube[1], cube[2]] = true;
        }

        int count = 0;
        foreach (int[] cube in cubes)
        {
            if (cube[0] == 0 || !space[cube[0] - 1, cube[1], cube[2]])
            {
                count++;
            }
            if (cube[1] == 0 || !space[cube[0], cube[1] - 1, cube[2]])
            {
                count++;
            }
            if (cube[2] == 0 || !space[cube[0], cube[1], cube[2] - 1])
            {
                count++;
            }
            if (cube[0] == length - 1 || !space[cube[0] + 1, cube[1], cube[2]])
            {
                count++;
            }
            if (cube[1] == length - 1 || !space[cube[0], cube[1] + 1, cube[2]])
            {
                count++;
            }
            if (cube[2] == length - 1 || !space[cube[0], cube[1], cube[2] + 1])
            {
                count++;
            }
        }

        return count.ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        const int length = 20;
        var space = new bool[length, length, length];
        while (input.ReadLine() is { } line)
        {
            int[] cube = line.Split(",").Select(int.Parse).ToArray();
            space[cube[0], cube[1], cube[2]] = true;
        }

        // Flood fill from the outside
        int count = 0;
        var queued = new bool[length + 2, length + 2, length + 2]; // Make queued is one unit longer in each direction
        var next = new Queue<(int X, int Y, int Z)>();
        queued[0, 0, 0] = true;
        next.Enqueue((0, 0, 0));
        while (next.Any())
        {
            (int x, int y, int z) = next.Dequeue();

            if (x < length + 1)
            {
                Check(x + 1, y, z);
            }
            if (y < length + 1)
            {
                Check(x, y + 1, z);
            }
            if (z < length + 1)
            {
                Check(x, y, z + 1);
            }
            if (x > 0)
            {
                Check(x - 1, y, z);
            }
            if (y > 0)
            {
                Check(x, y - 1, z);
            }
            if (z > 0)
            {
                Check(x, y, z - 1);
            }
        }

        return count.ToString(CultureInfo.InvariantCulture);

        void Check(int x, int y, int z)
        {
            (int a, int b, int c) = (x - 1, y - 1, z - 1); // Space coordinates
            if (a is >= 0 and < length && b is >= 0 and < length && c is >= 0 and < length && space[a, b, c])
            {
                count++;
            }
            else if (!queued[x, y, z])
            {
                queued[x, y, z] = true;
                next.Enqueue((x, y, z));
            }
        }
    }
}
