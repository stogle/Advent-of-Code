using System.Globalization;

namespace AdventOfCode2022.Day14;

public class RegolithReservoir : IDay
{
    public int DayNumber => 14;

    public string PartOne(TextReader input)
    {
        List<(int X1, int Y1, int X2, int Y2)> scan = ReadInput(input, out int minX, out int maxX, out int maxY);

        // Create map
        bool[,] map = new bool[maxX - minX + 1, maxY + 1]; // map[0, 0] represents point (minX, 0)
        foreach ((int x1, int y1, int x2, int y2) in scan)
        {
            for (int x = Math.Min(x1, x2); x <= Math.Max(x1, x2); x++)
            {
                for (int y = Math.Min(y1, y2); y <= Math.Max(y1, y2); y++)
                {
                    map[x - minX, y] = true;
                }
            }
        }

        // Pour sand
        int units = 0;
        bool sandCameToRest;
        do
        {
            sandCameToRest = false;
            int sandX = 500;
            int sandY = 0;

            while (sandY <= maxY)
            {
                if (!map[sandX - minX, sandY + 1])
                {
                    // Fall down one step
                    sandY++;
                }
                else if (sandX == minX)
                {
                    // Fall off to the left
                    break;
                }
                else if (!map[sandX - minX - 1, sandY + 1])
                {
                    // Fall one step down and to the left
                    sandX--;
                    sandY++;
                }
                else if (sandX == maxX)
                {
                    // Fall off to the right
                    break;
                }
                else if (!map[sandX - minX + 1, sandY + 1])
                {
                    // Fall one step down and to the right
                    sandX++;
                    sandY++;
                }
                else
                {
                    map[sandX - minX, sandY] = true;
                    units++;
                    sandCameToRest = true;
                    break;
                }
            }
        } while (sandCameToRest);

        return units.ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        List<(int X1, int Y1, int X2, int Y2)> scan = ReadInput(input, out _, out _, out int maxY);

        // Create map
        maxY++; // Air above floor
        var map = new HashSet<(int X, int Y)>();
        foreach ((int x1, int y1, int x2, int y2) in scan)
        {
            for (int x = Math.Min(x1, x2); x <= Math.Max(x1, x2); x++)
            {
                for (int y = Math.Min(y1, y2); y <= Math.Max(y1, y2); y++)
                {
                    map.Add((x, y));
                }
            }
        }

        // Pour sand
        int units = 0;
        int sandX = 500;
        int sandY = 0;
        while (!map.Contains((sandX, sandY)))
        {
            while (sandY < maxY)
            {
                if (!map.Contains((sandX, sandY + 1)))
                {
                    // Fall down one step
                    sandY++;
                }
                else if (!map.Contains((sandX - 1, sandY + 1)))
                {
                    // Fall one step down and to the left
                    sandX--;
                    sandY++;
                }
                else if (!map.Contains((sandX + 1, sandY + 1)))
                {
                    // Fall one step down and to the right
                    sandX++;
                    sandY++;
                }
                else
                {
                    break;
                }
            }

            map.Add((sandX, sandY));
            units++;
            sandX = 500;
            sandY = 0;
        }

        return units.ToString(CultureInfo.InvariantCulture);
    }

    private static List<(int X1, int Y1, int X2, int Y2)> ReadInput(TextReader input, out int minX, out int maxX, out int maxY)
    {
        // Read input
        minX = int.MaxValue;
        maxX = int.MinValue;
        maxY = int.MinValue;
        var scan = new List<(int X1, int Y1, int X2, int Y2)>();
        while (input.ReadLine() is { } line)
        {
            List<int[]> points = line.Split(" -> ")
                .Select(p => p.Split(',').Select(int.Parse).ToArray())
                .ToList();

            int[] start = points[0];
            foreach (int[] end in points.Skip(1))
            {
                minX = Math.Min(minX, Math.Min(start[0], end[0]));
                maxX = Math.Max(maxX, Math.Max(start[0], end[0]));
                maxY = Math.Max(maxY, Math.Max(start[1], end[1]));
                scan.Add((start[0], start[1], end[0], end[1]));
                start = end;
            }
        }

        return scan;
    }
}
