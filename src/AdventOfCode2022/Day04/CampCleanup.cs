using System.Globalization;

namespace AdventOfCode2022.Day04;

public class CampCleanup : IDay
{
    public int DayNumber => 4;

    public string PartOne(TextReader input)
    {
        int result = 0;
        while (input.ReadLine() is { } line)
        {
            string[] sections = line.Split('-', ',');

            int firstStart = int.Parse(sections[0], CultureInfo.InvariantCulture);
            int firstEnd = int.Parse(sections[1], CultureInfo.InvariantCulture);
            int secondStart = int.Parse(sections[2], CultureInfo.InvariantCulture);
            int secondEnd = int.Parse(sections[3], CultureInfo.InvariantCulture);
            if ((secondStart >= firstStart && secondEnd <= firstEnd) ||
                (firstStart >= secondStart && firstEnd <= secondEnd))
            {
                result++;
            }
        }

        return result.ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        int result = 0;
        while (input.ReadLine() is { } line)
        {
            string[] sections = line.Split('-', ',');

            int firstStart = int.Parse(sections[0], CultureInfo.InvariantCulture);
            int firstEnd = int.Parse(sections[1], CultureInfo.InvariantCulture);
            int secondStart = int.Parse(sections[2], CultureInfo.InvariantCulture);
            int secondEnd = int.Parse(sections[3], CultureInfo.InvariantCulture);
            if ((secondStart >= firstStart && secondStart <= firstEnd) ||
                (firstStart >= secondStart && firstStart <= secondEnd))
            {
                result++;
            }
        }

        return result.ToString(CultureInfo.InvariantCulture);
    }
}
