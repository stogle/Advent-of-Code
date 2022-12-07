using System.Globalization;

namespace AdventOfCode2022.Day12;

public class HillClimbingAlgorithm : IDay
{
    public int DayNumber => 12;

    public string PartOne(TextReader input)
    {
        IList<IList<int>> heightMap = ReadInput(input, out (int Row, int Column) start, out (int Row, int Column) end);
        int distance = GetShortestDistance(heightMap, start, (from, to) => to <= from + 1, (row, column) => row == end.Row && column == end.Column);

        return distance.ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        IList<IList<int>> heightMap = ReadInput(input, out _, out (int Row, int Column) end);
        int distance = GetShortestDistance(heightMap, end, (from, to) => from <= to + 1, (row, column) => heightMap[row][column] == 0);

        return distance.ToString(CultureInfo.InvariantCulture);
    }

    private static IList<IList<int>> ReadInput(TextReader input, out (int Row, int Column) start, out (int Row, int Column) end)
    {
        start = (0, 0);
        end = (0, 0);
        var heightMap = new List<IList<int>>();
        while (input.ReadLine() is { } line)
        {
            int positionIndex = line.IndexOf('S');
            if (positionIndex != -1)
            {
                start = (heightMap.Count, positionIndex);
            }

            int endIndex = line.IndexOf('E');
            if (endIndex != -1)
            {
                end = (heightMap.Count, endIndex);
            }

            heightMap.Add(line.Select(c => c switch
            {
                'S' => 0,
                'E' => 25,
                _ => c - 'a'
            }).ToList());
        }

        return heightMap;
    }

    private static int GetShortestDistance(IList<IList<int>> heightMap, (int Row, int Column) start, Func<int, int, bool> canMove, Func<int, int, bool> isEnd)
    {
        int[][] distances = heightMap.Select(r => r.Select(_ => int.MaxValue).ToArray()).ToArray();
        distances[start.Row][start.Column] = 0;

        // Breadth-first search
        var queue = new Queue<(int Row, int Column)>();
        queue.Enqueue(start);
        while (queue.Count != 0)
        {
            (int row, int column) = queue.Dequeue();
            if (isEnd(row, column))
            {
                return distances[row][column];
            }

            int distance = distances[row][column] + 1;
            int height = heightMap[row][column];
            foreach ((int r, int c) in GetAdjacent(row, column, distance, height))
            {
                distances[r][c] = distance;
                queue.Enqueue((r, c));
            }
        }

        return -1;

        IEnumerable<(int Row, int Column)> GetAdjacent(int row, int column, int distance, int height)
        {
            if (column > 0 && distance < distances[row][column - 1] && canMove(height, heightMap[row][column - 1]))
            {
                yield return (row, column - 1);
            }
            if (column < heightMap[row].Count - 1 && distance < distances[row][column + 1] && canMove(height, heightMap[row][column + 1]))
            {
                yield return (row, column + 1);
            }
            if (row > 0 && distance < distances[row - 1][column] && canMove(height, heightMap[row - 1][column]))
            {
                yield return (row - 1, column);
            }
            if (row < heightMap.Count - 1 && distance < distances[row + 1][column] && canMove(height, heightMap[row + 1][column]))
            {
                yield return (row + 1, column);
            }
        }
    }
}
