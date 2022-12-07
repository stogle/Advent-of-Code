using System.Text;

namespace AdventOfCode2022.Day25;

public class FullOfHotAir : IDay
{
    public int DayNumber => 25;

    public string PartOne(TextReader input)
    {
        long sum = 0;
        while (input.ReadLine() is { } line)
        {
            sum += FromSnafu(line);
        }

        return ToSnafu(sum);
    }

    public string PartTwo(TextReader input)
    {
        return "";
    }

    private static long FromSnafu(string line)
    {
        long result = 0;
        foreach (char c in line)
        {
            long value = c switch
            {
                '2' => 2,
                '1' => 1,
                '0' => 0,
                '-' => -1,
                '=' => -2,
                _ => throw new InvalidOperationException()
            };

            result = 5 * result + value;
        }

        return result;
    }

    private static string ToSnafu(long value)
    {
        var result = new StringBuilder();
        do
        {
            switch (value % 5)
            {
                case 4:
                    result.Insert(0, '-');
                    value = value / 5 + 1;
                    break;
                case 3:
                    result.Insert(0, '=');
                    value = value / 5 + 1;
                    break;
                case 2:
                    result.Insert(0, '2');
                    value /= 5;
                    break;
                case 1:
                    result.Insert(0, '1');
                    value /= 5;
                    break;
                case 0:
                    result.Insert(0, '0');
                    value /= 5;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        } while (value != 0);

        return result.ToString();
    }
}
