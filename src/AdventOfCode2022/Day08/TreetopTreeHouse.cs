using System.Globalization;

namespace AdventOfCode2022.Day08;

public class TreetopTreeHouse : IDay
{
    public int DayNumber => 8;

    public string PartOne(TextReader input)
    {
        List<List<int>> grid = ReadInput(input);

        int visibleCount = 2 * grid.Count + 2 * grid[0].Count - 4;

        List<int> topMaximums = grid[0].ToList();
        for (int i = 1; i < grid.Count - 1; i++)
        {
            List<int> gridRow = grid[i];
            int leftMaximum = gridRow[0];
            for (int j = 1; j < gridRow.Count - 1; j++)
            {
                int topMaximum = topMaximums[j];
                int rightMaximum = gridRow.Skip(j + 1).Max();
                int bottomMaximum = grid.Skip(i + 1).Max(r => r[j]);

                int height = gridRow[j];
                if (height > topMaximum || height > bottomMaximum ||
                    height > leftMaximum || height > rightMaximum)
                {
                    visibleCount++;
                }

                topMaximums[j] = Math.Max(topMaximum, height);
                leftMaximum = Math.Max(leftMaximum, height);
            }
        }

        return visibleCount.ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        List<List<int>> grid = ReadInput(input);

        int[][] scenicScores = grid.Select(row => new int[row.Count]).ToArray();

        for (int i = 1; i < grid.Count - 1; i++)
        {
            List<int> gridRow = grid[i];
            for (int j = 1; j < gridRow.Count - 1; j++)
            {
                int height = gridRow[j];
                int viewingDistanceUp = grid.Take(i).Reverse().Select(row => row[j]).TakeWhileInclusive(h => h < height).Count();
                int viewingDistanceLeft = gridRow.Take(j).Reverse().TakeWhileInclusive(h => h < height).Count();
                int viewingDistanceRight = gridRow.Skip(j + 1).TakeWhileInclusive(h => h < height).Count();
                int viewingDistanceDown = grid.Skip(i + 1).Select(row => row[j]).TakeWhileInclusive(h => h < height).Count();
                scenicScores[i][j] = viewingDistanceUp * viewingDistanceLeft * viewingDistanceRight * viewingDistanceDown;
            }
        }

        return scenicScores.Max(row => row.Max()).ToString(CultureInfo.InvariantCulture);
    }

    private static List<List<int>> ReadInput(TextReader input)
    {
        List<List<int>> grid = new();
        while (input.ReadLine() is { } line)
        {
            grid.Add(line.Select(c => c - '0').ToList());
        }

        return grid;
    }
}
