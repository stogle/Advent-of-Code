using System.Globalization;

namespace AdventOfCode2022.Day06;

public class TuningTrouble : IDay
{
    public int DayNumber => 6;

    public string PartOne(TextReader input)
    {
        const int length = 4;
        return GetFirstIndexOfDistinctCharacters(input, length);
    }

    public string PartTwo(TextReader input)
    {
        const int length = 14;
        return GetFirstIndexOfDistinctCharacters(input, length);
    }

    private static string GetFirstIndexOfDistinctCharacters(TextReader input, int length)
    {
        char?[] buffer = new char?[length];
        int index = 0;

        int result = 0;
        int c;
        while ((c = input.Read()) != -1)
        {
            buffer[index] = (char)c;
            index = (index + 1) % length;
            result++;

            if (result >= length && buffer.Distinct().Count() == length)
            {
                return result.ToString(CultureInfo.InvariantCulture);
            }
        }

        throw new ArgumentException(null, nameof(input));
    }
}
