using System.Globalization;

namespace AdventOfCode2022.Day16;

public class ProboscideaVolcanium : IDay
{
    public int DayNumber => 16;

    public string PartOne(TextReader input)
    {
        IList<Valve> valves = ReadInput(input, out Valve start);
        const int minutes = 30;
        var state = new State(valves, start);
        State maxPressureState = state.GetMaxPressureState(minutes);
        return maxPressureState.CurrentPressure.ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        IList<Valve> valves = ReadInput(input, out Valve start);
        const int minutes = 26;
        var state = new State(valves, start);
        (State human, State? elephant, int totalPressure) = state.GetMaxPressureWithElephant(minutes);
        return totalPressure.ToString(CultureInfo.InvariantCulture);
    }

    private static IList<Valve> ReadInput(TextReader input, out Valve start)
    {
        var valves = new List<Valve>();
        int nextId = 0;
        while (input.ReadLine() is { } line)
        {
            string[] split = line.Split("; ");
            string name = split[0][6..8];
            int flowRate = int.Parse(split[0][23..], CultureInfo.InvariantCulture);
            int id = flowRate > 0 ? nextId++ : -1;
            IList<string> tunnels = split[1].Split(", ").Select(s => s[^2..]).ToList();
            valves.Add(new Valve(id, name, flowRate, tunnels));
        }
        start = valves.First(v => v.Name == "AA");

        // Find shortest path between all pairs with Floyd-Warshall
        foreach (Valve k in valves)
        {
            foreach (Valve i in valves.Where(v => v != k))
            {
                foreach (Valve j in valves.Where(v => v != i && v != k))
                {
                    int? newDistance = i.GetMinutesToValve(k) + k.GetMinutesToValve(j);
                    if (newDistance.HasValue)
                    {
                        int? currentDistance = i.GetMinutesToValve(j);
                        if (!currentDistance.HasValue || currentDistance > newDistance)
                        {
                            i.SetMinutesToValve(j, newDistance.Value);
                        }
                    }
                }
            }
        }

        // Return only valves that can be opened
        return valves.Where(v => v.FlowRate > 0).ToList();
    }
}
