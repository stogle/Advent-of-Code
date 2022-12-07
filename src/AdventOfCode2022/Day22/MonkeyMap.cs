using System.Globalization;

namespace AdventOfCode2022.Day22;

public class MonkeyMap : IDay
{
    public int DayNumber => 22;

    public string PartOne(TextReader input)
    {
        Map map = ReadMap(input);
        ReadPath(input, map);

        int password = 1000 * map.Row + 4 * map.Column + (int)map.Facing;
        return password.ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        Map map = ReadMap(input);
        map.WrapStrategy = map.Height == 200 ? WrapRealInput : WrapSampleInput;
        ReadPath(input, map);

        int password = 1000 * map.Row + 4 * map.Column + (int)map.Facing;
        return password.ToString(CultureInfo.InvariantCulture);
    }

    private static Map ReadMap(TextReader input)
    {
        var map = new List<string>();
        while (input.ReadLine() is { } line && line.Length != 0)
        {
            map.Add(line);
        }

        return new Map(map);
    }

    private static void ReadPath(TextReader input, Map map)
    {
        string path = input.ReadLine()!;
        int count = 0;
        foreach (char c in path)
        {
            if (char.IsAsciiDigit(c))
            {
                count = count * 10 + (c - '0');
            }
            else
            {
                map.Forward(count);
                switch (c)
                {
                    case 'L':
                        map.Left();
                        break;
                    case 'R':
                        map.Right();
                        break;
                }
                count = 0;
            }
        }
        map.Forward(count);
    }

    private static (int Row, int Column, Facing Facing) WrapSampleInput(Map map, int row, int column, Facing facing)
    {
        return (row, column, facing) switch
        {
            (>= 1 and <= 4, 13, Facing.Right) => (13 - row, 16, Facing.Left), // face 1, rows 1-4 to face 6, rows 12-9
            (>= 5 and <= 8, 13, Facing.Right) => (9, 21 - row, Facing.Down), // face 4, rows 5-8 to face 6, rows 16-13
            (>= 9 and <= 12, 17, Facing.Right) => (13 - row, 12, Facing.Left), // face 6, rows 9-12 to face 1, rows 4-1
            (9, >= 1 and <= 4, Facing.Down) => (12, 13 - column, Facing.Up), // face 2, columns 1-4 to face 5, columns 12-9
            (9, >= 5 and <= 8, Facing.Down) => (17 - column, 9, Facing.Right), // face 3, columns 5-8 to face 5, rows 12-9
            (13, >= 9 and <= 12, Facing.Down) => (8, 13 - column, Facing.Up), // face 5, columns 9-12 to face 2, columns 4-1
            (13, >= 13 and <= 16, Facing.Down) => (21 - column, 1, Facing.Right), // face 6, columns 13-16 to face 2, rows 8-5
            (>= 1 and <= 4, 8, Facing.Left) => (5, 4 + row, Facing.Down), // face 1, rows 1-4 to face 3, columns 5-8
            (>= 5 and <= 8, 0, Facing.Left) => (12, 21 - row, Facing.Up), // face 2, rows 5-8 to face 6, columns 16-13
            (>= 9 and <= 12, 8, Facing.Left) => (8, 17 - row, Facing.Up), // face 5, rows 9-12 to face 3, columns 8-5
            (4, >= 1 and <= 4, Facing.Up) => (1, 13 - column, Facing.Down), // face 2, columns 1-4 to face 1, columns 12-9
            (4, >= 5 and <= 8, Facing.Up) => (column - 4, 9, Facing.Right), // face 3, columns 5-8 to face 1, rows 1-4
            (0, >= 9 and <= 12, Facing.Up) => (1, column, Facing.Down), // face 5, columns 9-12 to face 1, columns 9-12
            (8, >= 13 and <= 16, Facing.Up) => (21 - column, 12, Facing.Left), // face 6, columns 13-16 to face 4, rows 8-5
            _ => (row, column, facing)
        };
    }

    private static (int Row, int Column, Facing Facing) WrapRealInput(Map map, int row, int column, Facing facing)
    {
        return (row, column, facing) switch
        {
            (>= 1 and <= 50, 151, Facing.Right) => (151 - row, 100, Facing.Left), // face 2, rows 1-50 to face 5, rows 150-101
            (>= 51 and <= 100, 101, Facing.Right) => (50, 50 + row, Facing.Up), // face 3, rows 51-100 to face 2, columns 101-150
            (>= 101 and <= 150, 101, Facing.Right) => (151 - row, 150, Facing.Left), // face 5, rows 101-150 to face 2, rows 50-1
            (>= 151 and <= 200, 51, Facing.Right) => (150, row - 100, Facing.Up), // face 6, rows 151-200 to face 5, columns 51-100
            (201, >= 1 and <= 50, Facing.Down) => (1, 100 + column, Facing.Down), // face 6, columns 1-50 to face 2, columns 101-150
            (151, >= 51 and <= 100, Facing.Down) => (100 + column, 50, Facing.Left), // face 5, columns 51-100 to face 6, rows 151-200
            (51, >= 101 and <= 150, Facing.Down) => (column - 50, 100, Facing.Left), // face 2, columns 101-150 to face 3, rows 51-100
            (>= 1 and <= 50, 50, Facing.Left) => (151 - row, 1, Facing.Right), // face 1, rows 1-50 to face 4, rows 150-101
            (>= 51 and <= 100, 50, Facing.Left) => (101, row - 50, Facing.Down), // face 3, rows 51-100 to face 4, columns 1-50
            (>= 101 and <= 150, 0, Facing.Left) => (151 - row, 51, Facing.Right), // face 4, rows 101-150 to face 1, rows 50-1
            (>= 151 and <= 200, 0, Facing.Left) => (1, row - 100, Facing.Down), // face 6, rows 151-200 to face 1, columns 51-100
            (100, >= 1 and <= 50, Facing.Up) => (50 + column, 51, Facing.Right), // face 4, columns 1-50 to face 3, rows 51-100
            (0, >= 51 and <= 100, Facing.Up) => (100 + column, 1, Facing.Right), // face 1, columns 51-100 to face 6, rows 151-200
            (0, >= 101 and <= 150, Facing.Up) => (200, column - 100, Facing.Up), // face 2, columns 101-150 to face 6, columns 1-50
            _ => (row, column, facing)
        };
    }
}
