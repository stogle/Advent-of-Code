using System.Globalization;

namespace AdventOfCode2022.Day23;

public class UnstableDiffusion : IDay
{
    public int DayNumber => 23;

    public string PartOne(TextReader input)
    {
        Region region = ReadInput(input);
        for (int round = 1; round <= 10; round++)
        {
            IDictionary<(int Row, int Column), (int Row, int Column)> proposals = region.ProposeMoves();
            if (!proposals.Any())
            {
                break;
            }

            region.Move(proposals);
        }

        return region.EmptyTileCount().ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        Region region = ReadInput(input);
        int round = 1;
        while (true)
        {
            IDictionary<(int Row, int Column), (int Row, int Column)> proposals = region.ProposeMoves();
            if (!proposals.Any())
            {
                break;
            }

            region.Move(proposals);
            round++;
        }

        return round.ToString(CultureInfo.InvariantCulture);
    }

    private static Region ReadInput(TextReader input)
    {
        var region = new Region();
        int row = 0;
        while (input.ReadLine() is { } line)
        {
            for (int column = 0; column < line.Length; column++)
            {
                if (line[column] == '#')
                {
                    region.AddElf((row, column));
                }
            }
            row++;
        }

        return region;
    }
}
