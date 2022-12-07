using System.Text;

namespace AdventOfCode2022.Day19;

internal sealed record State(int Minute, int MaxMinutes, int Ore, int Clay, int Obsidian, int Geodes,
    int OreBots, int ClayBots, int ObsidianBots, int GeodeBots, State? PreviousState)
{
    public int GetGeodesUpperBound(Blueprint blueprint)
    {
        int clay = Clay;
        int obsidian = Obsidian;
        int geodes = Geodes;
        int clayBots = ClayBots;
        int obsidianBots = ObsidianBots;
        int geodeBots = GeodeBots;
        for (int i = Minute; i <= MaxMinutes; i++)
        {
            bool buildObsidianBot = clay > blueprint.ObsidianBotClayCost;
            bool buildGeodeBot = obsidian > blueprint.GeodeBotObsidianCost;
            clay += clayBots;
            obsidian += obsidianBots;
            geodes += geodeBots;
            clayBots++;
            if (buildObsidianBot)
            {
                obsidianBots++;
                clay -= blueprint.ObsidianBotClayCost;
            }
            if (buildGeodeBot)
            {
                geodeBots++;
                obsidian -= blueprint.GeodeBotObsidianCost;
            }
        }

        return geodes;
    }

    public IEnumerable<State> GetNextStates(Blueprint blueprint)
    {
        if (Minute == MaxMinutes)
        {
            yield break;
        }

        if (Ore >= blueprint.GeodeBotOreCost && Obsidian >= blueprint.GeodeBotObsidianCost)
        {
            yield return this with
            {
                Minute = Minute + 1,
                Ore = Ore + OreBots - blueprint.GeodeBotOreCost,
                Clay = Clay + ClayBots,
                Obsidian = Obsidian + ObsidianBots - blueprint.GeodeBotObsidianCost,
                Geodes = Geodes + GeodeBots,
                GeodeBots = GeodeBots + 1,
                PreviousState = this
            };
        }

        if (ObsidianBots < blueprint.GeodeBotObsidianCost && Ore >= blueprint.ObsidianBotOreCost && Clay >= blueprint.ObsidianBotClayCost)
        {
            yield return this with
            {
                Minute = Minute + 1,
                Ore = Ore + OreBots - blueprint.ObsidianBotOreCost,
                Clay = Clay + ClayBots - blueprint.ObsidianBotClayCost,
                Obsidian = Obsidian + ObsidianBots,
                Geodes = Geodes + GeodeBots,
                ObsidianBots = ObsidianBots + 1,
                PreviousState = this
            };
        }

        if (ClayBots < blueprint.ObsidianBotClayCost && Ore >= blueprint.ClayBotOreCost)
        {
            yield return this with
            {
                Minute = Minute + 1,
                Ore = Ore + OreBots - blueprint.ClayBotOreCost,
                Clay = Clay + ClayBots,
                Obsidian = Obsidian + ObsidianBots,
                Geodes = Geodes + GeodeBots,
                ClayBots = ClayBots + 1,
                PreviousState = this
            };
        }

        int maxOreBots = Math.Max(Math.Max(blueprint.OreBotOreCost, blueprint.ClayBotOreCost),
            Math.Max(blueprint.ObsidianBotOreCost, blueprint.GeodeBotOreCost));
        if (OreBots < maxOreBots && Ore >= blueprint.OreBotOreCost)
        {
            yield return this with
            {
                Minute = Minute + 1,
                Ore = Ore + OreBots - blueprint.OreBotOreCost,
                Clay = Clay + ClayBots,
                Obsidian = Obsidian + ObsidianBots,
                Geodes = Geodes + GeodeBots,
                OreBots = OreBots + 1,
                PreviousState = this
            };
        }

        yield return this with
        {
            Minute = Minute + 1,
            Ore = Ore + OreBots,
            Clay = Clay + ClayBots,
            Obsidian = Obsidian + ObsidianBots,
            Geodes = Geodes + GeodeBots,
            PreviousState = this
        };
    }

    public override string ToString()
    {
        if (PreviousState == null)
        {
            return string.Empty;
        }

        var result = new StringBuilder(PreviousState.ToString());
        result.AppendLine((string)$"== Minute {Minute} ==");
        if (OreBots > PreviousState.OreBots)
        {
            result.AppendLine((string)$"Spend {PreviousState.Ore + PreviousState.OreBots - Ore} ore to start building an ore-collecting robot.");
        }
        if (ClayBots > PreviousState.ClayBots)
        {
            result.AppendLine((string)$"Spend {PreviousState.Ore + PreviousState.OreBots - Ore} ore to start building a clay-collecting robot.");
        }
        if (ObsidianBots > PreviousState.ObsidianBots)
        {
            result.AppendLine((string)$"Spend {PreviousState.Ore + PreviousState.OreBots - Ore} ore and {PreviousState.Clay + PreviousState.ClayBots - Clay} clay to start building an obsidian-collecting robot.");
        }
        if (GeodeBots > PreviousState.GeodeBots)
        {
            result.AppendLine((string)$"Spend {PreviousState.Ore + PreviousState.OreBots - Ore} ore and {PreviousState.Obsidian + PreviousState.ObsidianBots - Obsidian} obsidian to start building a geode-cracking robot.");
        }
        if (PreviousState.OreBots > 0)
        {
            result.AppendLine((string)$"{PreviousState.OreBots} ore-collecting robots collect {PreviousState.OreBots} ore; you now have {Ore} ore.");
        }
        if (PreviousState.ClayBots > 0)
        {
            result.AppendLine((string)$"{PreviousState.ClayBots} clay-collecting robots collect {PreviousState.ClayBots} clay; you now have {Clay} clay.");
        }
        if (PreviousState.ObsidianBots > 0)
        {
            result.AppendLine((string)$"{PreviousState.ObsidianBots} obsidian-collecting robots collect {PreviousState.ObsidianBots} obsidian; you now have {Obsidian} obsidian.");
        }
        if (PreviousState.GeodeBots > 0)
        {
            result.AppendLine((string)$"{PreviousState.GeodeBots} geode-cracking robots crack {PreviousState.GeodeBots} geodes; you now have {Geodes} open geodes.");
        }
        if (OreBots > PreviousState?.OreBots)
        {
            result.AppendLine((string)$"The new ore-collecting robot is ready; you now have {OreBots} of them.");
        }
        if (ClayBots > PreviousState?.ClayBots)
        {
            result.AppendLine((string)$"The new clay-collecting robot is ready; you now have {ClayBots} of them.");
        }
        if (ObsidianBots > PreviousState?.ObsidianBots)
        {
            result.AppendLine((string)$"The new obsidian-collecting robot is ready; you now have {ObsidianBots} of them.");
        }
        if (GeodeBots > PreviousState?.GeodeBots)
        {
            result.AppendLine((string)$"The new geode-cracking robot is ready; you now have {GeodeBots} of them.");
        }

        result.AppendLine();

        return result.ToString();
    }
}
