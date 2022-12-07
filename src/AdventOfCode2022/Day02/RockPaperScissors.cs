using System.Globalization;

namespace AdventOfCode2022.Day02;

public class RockPaperScissors : IDay
{
    public int DayNumber => 2;

    private static readonly int[][] End =
    {
        // You  R  P  S
        new[] { 3, 6, 0 }, // Opponent Rock
        new[] { 0, 3, 6 }, // Opponent Paper
        new[] { 6, 0, 3 }  // Opponent Scissors
    };

    private static readonly int[][] You =
    {
        // End  L  D  W
        new[] { 2, 0, 1 }, // Opponent Rock
        new[] { 0, 1, 2 }, // Opponent Paper
        new[] { 1, 2, 0 }  // Opponent Scissors
    };

    public string PartOne(TextReader input)
    {
        int result = 0;
        while (input.ReadLine() is { } line)
        {
            int opponent = line[0] - 'A'; // 0 = Rock, 1 = Paper, 2 = Scissors
            int you = line[2] - 'X'; // 0 = Rock, 1 = Paper, 2 = Scissors

            result += 1 + you + End[opponent][you];
        }

        return result.ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        int result = 0;
        while (input.ReadLine() is { } line)
        {
            int opponent = line[0] - 'A'; // 0 = Rock, 1 = Paper, 2 = Scissors
            int end = line[2] - 'X'; // 0 = Lose, 1 = Draw, 2 = Win
            int you = You[opponent][end];

            result += 1 + you + 3*end;
        }

        return result.ToString(CultureInfo.InvariantCulture);
    }
}
