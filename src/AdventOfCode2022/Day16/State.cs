using System.Globalization;
using System.Text;

namespace AdventOfCode2022.Day16;

internal sealed record State(IList<Valve> Valves, Valve? Valve, int CurrentMinute = 0, int CurrentPressure = 0,
    int PressurePerMinute = 0, State? PreviousState = null)
{
    public State GetMaxPressureState(int minutes) =>
        GetNextStates(minutes)
            .Select(s => s.GetMaxPressureState(minutes))
            .MaxBy(s => s.CurrentPressure) ?? this;

    private IEnumerable<State> GetNextStates(int minutes)
    {
        if (Valve == null)
        {
            yield break;
        }

        bool canOpenMoreValves = false;
        foreach (Valve valve in Valves)
        {
            int minutesToOpenValve = Valve.GetMinutesToValve(valve)!.Value + 1;
            if (CurrentMinute + minutesToOpenValve >= minutes)
            {
                // Can't get to valve in time
                continue;
            }

            canOpenMoreValves = true;
            yield return this with
            {
                Valves = Valves.Where(v => v != valve).ToList(),
                Valve = valve,
                CurrentMinute = CurrentMinute + minutesToOpenValve,
                CurrentPressure = CurrentPressure + minutesToOpenValve * PressurePerMinute,
                PressurePerMinute = PressurePerMinute + valve.FlowRate,
                PreviousState = this
            };
        }

        if (!canOpenMoreValves)
        {
            // Do nothing
            yield return this with
            {
                Valve = null,
                CurrentMinute = minutes,
                CurrentPressure = CurrentPressure + (minutes - CurrentMinute) * PressurePerMinute,
                PreviousState = this
            };
        }
    }

    public (State Human, State? Elephant, int TotalPressure) GetMaxPressureWithElephant(int minutes)
    {
        var maxPressureByOpenValves = new Dictionary<ulong, (State State, int FinalPressure)>();
        GetMaxPressureByOpenValves(minutes, maxPressureByOpenValves);

        ulong mask = (1UL << Valves.Count) - 1;
        (State Human, State? Elephant, int TotalPressure) result = (this, null, 0);

        foreach ((ulong key, (State human, int humanFinalPressure)) in maxPressureByOpenValves)
        {
            foreach ((ulong elephantKey, (State elephant, int elephantFinalPressure)) in maxPressureByOpenValves)
            {
                if ((~key & ~elephantKey & mask) == 0)
                {
                    int totalPressure = humanFinalPressure + elephantFinalPressure;
                    if (totalPressure > result.TotalPressure)
                    {
                        result = (human, elephant, totalPressure);
                    }
                }
            }
        }

        return result;
    }

    private void GetMaxPressureByOpenValves(int minutes, IDictionary<ulong, (State State, int FinalPressure)> maxPressureByOpenValves)
    {
        foreach (State nextState in GetNextStates(minutes))
        {
            ulong key = nextState.Valves.Aggregate(0UL, (result, v) => result + (1UL << v.Id));
            int finalPressure = nextState.GetFinalPressure(minutes);
            if (!maxPressureByOpenValves.TryGetValue(key, out var max) || finalPressure > max.FinalPressure)
            {
                maxPressureByOpenValves[key] = (nextState, finalPressure);
            }

            nextState.GetMaxPressureByOpenValves(minutes, maxPressureByOpenValves);
        }
    }

    private int GetFinalPressure(int minutes) =>
        CurrentPressure + (minutes - CurrentMinute) * PressurePerMinute;

    public override string ToString()
    {
        var result = new StringBuilder();
        if (PreviousState != null)
        {
            result.AppendLine(PreviousState.ToString());
            result.AppendLine(CultureInfo.InvariantCulture, $"== Minute {CurrentMinute} ==");
            if (PressurePerMinute == 0)
            {
                result.AppendLine("No valves are open.");
            }
            else
            {
                result.AppendLine(CultureInfo.InvariantCulture, $"Valves are open, releasing {PressurePerMinute} pressure.");
            }

            if (Valve?.FlowRate > 0)
            {
                result.AppendLine(CultureInfo.InvariantCulture, $"You open valve {Valve.Name}.");
            }
        }

        return result.ToString();
    }
}
