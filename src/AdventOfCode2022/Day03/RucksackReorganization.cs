using System.Globalization;

namespace AdventOfCode2022.Day03;

public class RucksackReorganization : IDay
{
    public int DayNumber => 3;

    public string PartOne(TextReader input)
    {
        int priorities = 0;
        while (input.ReadLine() is { } line)
        {
            int count = line.Length / 2;
            char c = line[..(line.Length / 2)]
                .First(c => line.IndexOf(c, count) != -1);
            priorities += char.IsLower(c) ? c - 'a' + 1 : c - 'A' + 27;
        }

        return priorities.ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        int priorities = 0;
        while (input.ReadLine() is { } line)
        {
            string line2 = input.ReadLine()!;
            string line3 = input.ReadLine()!;
            char c = line
                .First(c => line2.IndexOf(c) != -1 && line3.IndexOf(c) != -1);
            priorities += char.IsLower(c) ? c - 'a' + 1 : c - 'A' + 27;
        }

        return priorities.ToString(CultureInfo.InvariantCulture);
    }
}
