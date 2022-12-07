namespace AdventOfCode2022.Day19;

internal sealed record Blueprint(int Id, int OreBotOreCost, int ClayBotOreCost, int ObsidianBotOreCost,
    int ObsidianBotClayCost, int GeodeBotOreCost, int GeodeBotObsidianCost)
{
    public int GetMaximumGeodes(State state)
    {
        int geodes = 0;
        var queue = new PriorityQueue<State, int>();
        queue.Enqueue(state, state.GetGeodesUpperBound(this));
        while (queue.Count > 0)
        {
            state = queue.Dequeue();

            if (state.Geodes > geodes)
            {
                geodes = state.Geodes;
            }


            foreach (State nextState in state.GetNextStates(this))
            {
                int geodesUpperBound = nextState.GetGeodesUpperBound(this);
                if (geodesUpperBound <= geodes)
                {
                    continue;
                }

                queue.Enqueue(nextState, geodesUpperBound);
            }
        }

        return geodes;
    }
}
