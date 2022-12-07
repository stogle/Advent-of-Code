using System.Globalization;
using Priority_Queue;

namespace AdventOfCode2022.Day24;

public class BlizzardBasin : IDay
{
    public int DayNumber => 24;

    public string PartOne(TextReader input)
    {
        Basin basin = ReadInput(input);

        var state = new State(basin, -1, 0, 0);
        state = GetShortestPath(state, s => s.Row == basin.Height - 1 && s.Column == basin.Width - 1)!;
        state = state with { Row = state.Row + 1, Minute = state.Minute + 1, Previous = state, Direction = "down" };
        return state.Minute.ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        Basin basin = ReadInput(input);

        var state = new State(basin, -1, 0, 0);
        state = GetShortestPath(state, s => s.Row == basin.Height - 1 && s.Column == basin.Width - 1)!;
        state = state with { Row = state.Row + 1, Minute = state.Minute + 1, Previous = state, Direction = "down" };
        state = GetShortestPath(state, s => s.Row == 0 && s.Column == 0)!;
        state = state with { Row = state.Row - 1, Minute = state.Minute + 1, Previous = state, Direction = "up" };
        state = GetShortestPath(state, s => s.Row == basin.Height - 1 && s.Column == basin.Width - 1)!;
        state = state with { Row = state.Row + 1, Minute = state.Minute + 1, Previous = state, Direction = "down" };
        return state.Minute.ToString(CultureInfo.InvariantCulture);
    }

    private static Basin ReadInput(TextReader input)
    {
        var downBlizzards = new List<bool[]>();
        var rightBlizzards = new List<bool[]>();
        var upBlizzards = new List<bool[]>();
        var leftBlizzards = new List<bool[]>();

        input.ReadLine();
        while (input.ReadLine() is { } line && line[1] != '#')
        {
            downBlizzards.Add(line[1..^1].Select(c => c == 'v').ToArray());
            rightBlizzards.Add(line[1..^1].Select(c => c == '>').ToArray());
            upBlizzards.Add(line[1..^1].Select(c => c == '^').ToArray());
            leftBlizzards.Add(line[1..^1].Select(c => c == '<').ToArray());
        }

        return new Basin(downBlizzards, rightBlizzards, upBlizzards, leftBlizzards);
    }

    private static State? GetShortestPath(State state, Predicate<State> isTarget)
    {
        var queue = new SimplePriorityQueue<State, int>(State.EqualityComparer);
        foreach (var nextState in state.GetNextStates())
        {
            queue.Enqueue(nextState, nextState.Minute);
        }

        while (queue.Count > 0)
        {
            state = queue.Dequeue();

            // Exit
            if (isTarget(state))
            {
                return state;
            }

            foreach (var nextState in state.GetNextStates())
            {
                if (!queue.TryGetPriority(nextState, out int minute))
                {
                    queue.Enqueue(nextState, nextState.Minute);
                }
                else if (minute > nextState.Minute)
                {
                    queue.UpdatePriority(nextState, nextState.Minute);
                }
            }
        }

        return null;
    }
}
