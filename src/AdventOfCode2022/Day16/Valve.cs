namespace AdventOfCode2022.Day16;

internal sealed record Valve(int Id, string Name, int FlowRate, IList<string> Tunnels)
{
    private readonly IDictionary<string, int> _minutesToValve = Tunnels.ToDictionary(name => name, _ => 1);

    public int? GetMinutesToValve(Valve valve) =>
        _minutesToValve.TryGetValue(valve.Name, out int value) ? value : null;

    public void SetMinutesToValve(Valve valve, int value) =>
        _minutesToValve[valve.Name] = value;
}
