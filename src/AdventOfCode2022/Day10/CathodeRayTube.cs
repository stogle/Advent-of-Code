using System.Globalization;
using System.Text;

namespace AdventOfCode2022.Day10;

public class CathodeRayTube : IDay
{
    public int DayNumber => 10;

    public string PartOne(TextReader input)
    {
        int signalStrength = 0;

        int cycle = 0;
        int x = 1;
        while (input.ReadLine() is { } line)
        {
            // Decode
            int cycles = 0;
            Action? action = null;
            if (line == "noop")
            {
                cycles = 1;
                action = () => { };
            }
            // ReSharper disable once StringLiteralTypo
            else if (line.StartsWith("addx", StringComparison.InvariantCulture))
            {
                cycles = 2;
                action = () => x += int.Parse(line[5..], CultureInfo.InvariantCulture);
            }

            // Update signal strength
            for (int i = 0; i < cycles; i++)
            {
                cycle++;
                if ((cycle - 20) % 40 == 0)
                {
                    signalStrength += cycle * x;
                }
            }

            // Execute
            action?.Invoke();
        }

        return signalStrength.ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        StringBuilder result = new ();

        int cycle = 0;
        int x = 1;
        while (input.ReadLine() is { } line)
        {
            // Decode
            int cycles = 0;
            Action? action = null;
            if (line == "noop")
            {
                cycles = 1;
                action = () => { };
            }
            // ReSharper disable once StringLiteralTypo
            else if (line.StartsWith("addx", StringComparison.InvariantCulture))
            {
                cycles = 2;
                action = () => x += int.Parse(line[5..], CultureInfo.InvariantCulture);
            }

            // Update result
            for (int i = 0; i < cycles; i++)
            {
                bool lit = Math.Abs(x - cycle % 40) <= 1;
                result.Append(lit ? '#' : '.');
                cycle++;
                if (cycle % 40 == 0)
                {
                    result.AppendLine();
                }
            }

            // Execute
            action?.Invoke();
        }

        return result.ToString();
    }
}
