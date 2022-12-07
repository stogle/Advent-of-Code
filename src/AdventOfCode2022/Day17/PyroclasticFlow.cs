using System.Globalization;

namespace AdventOfCode2022.Day17;

public class PyroclasticFlow : IDay
{
    private static readonly bool[][][] Rocks =
    {
        new[]
        {
            new[] { true,  true,  true,  true }
        },
        new[]
        {
            new[] { false, true,  false },
            new[] { true,  true,  true  },
            new[] { false, true,  false }
        },
        new[]
        {
            new[] { true,  true,  true },
            new[] { false, false, true },
            new[] { false, false, true }
        },
        new[]
        {
            new[] { true },
            new[] { true },
            new[] { true },
            new[] { true }
        },
        new[]
        {
            new[] { true,  true },
            new[] { true,  true }
        }
    };

    public int DayNumber => 17;

    public string PartOne(TextReader input)
    {
        Chamber chamber = ReadInput(input);

        const int rockCount = 2022;
        for (int i = 0; i < rockCount; i++)
        {
            chamber.Add();
            chamber.Drop();
        }

        return chamber.Height.ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        Chamber chamber = ReadInput(input);

        long cycleCount = 0;
        long cycleHeight = 0;
        const long rockCount = 1_000_000_000_000L;
        for (long i = 0; i < rockCount; i++)
        {
            chamber.Add();
            chamber.Drop();

            if (cycleCount == 0 && chamber.TryDetectCycle(i, out long cycleLength, out cycleHeight))
            {
                cycleCount = (rockCount - i) / cycleLength;
                i += cycleCount * cycleLength;
            }
        }

        long height = cycleCount * cycleHeight + chamber.Height;
        return height.ToString(CultureInfo.InvariantCulture);
    }

    private static Chamber ReadInput(TextReader input)
    {
        string jetPattern = input.ReadLine()!;
        return new Chamber(Rocks, jetPattern);
    }
}
