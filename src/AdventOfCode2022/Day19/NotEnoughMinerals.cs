using System.Globalization;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day19;

public class NotEnoughMinerals : IDay
{
    public int DayNumber => 19;

    public string PartOne(TextReader input)
    {
        List<Blueprint> blueprints = ReadInput(input);

        var initialState = new State(0, 24, 0, 0, 0, 0, 1, 0, 0, 0, null);
        int result = blueprints.Sum(b => b.Id * b.GetMaximumGeodes(initialState));
        return result.ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        List<Blueprint> blueprints = ReadInput(input);

        var initialState = new State(0, 32, 0, 0, 0, 0, 1, 0, 0, 0, null);
        int result = blueprints.Take(3).Aggregate(1, (current, b) => current * b.GetMaximumGeodes(initialState));
        return result.ToString(CultureInfo.InvariantCulture);
    }

    private static List<Blueprint> ReadInput(TextReader input)
    {
        var result = new List<Blueprint>();
        while (input.ReadLine() is { } line)
        {
            string[] split1 = line.Split(": ");
            int id = int.Parse(split1[0][10..], CultureInfo.InvariantCulture);
            string[] split2 = split1[1].Split(". ");
            int oreRobotOreCost = int.Parse(Regex.Match(split2[0], @"Each ore robot costs (\d+) ore").Groups[1].Captures[0].Value, CultureInfo.InvariantCulture);
            int clayRobotOreCost = int.Parse(Regex.Match(split2[1], @"Each clay robot costs (\d+) ore").Groups[1].Captures[0].Value, CultureInfo.InvariantCulture);
            Match match1 = Regex.Match(split2[2], @"Each obsidian robot costs (\d+) ore and (\d+) clay");
            int obsidianRobotOreCost = int.Parse(match1.Groups[1].Captures[0].Value, CultureInfo.InvariantCulture);
            int obsidianRobotClayCost = int.Parse(match1.Groups[2].Captures[0].Value, CultureInfo.InvariantCulture);
            Match match2 = Regex.Match(split2[3], @"Each geode robot costs (\d+) ore and (\d+) obsidian");
            int geodeRobotOreCost = int.Parse(match2.Groups[1].Captures[0].Value, CultureInfo.InvariantCulture);
            int geodeRobotObsidianCost = int.Parse(match2.Groups[2].Captures[0].Value, CultureInfo.InvariantCulture);
            result.Add(new Blueprint(id, oreRobotOreCost, clayRobotOreCost, obsidianRobotOreCost, obsidianRobotClayCost, geodeRobotOreCost, geodeRobotObsidianCost));
        }

        return result;
    }
}
