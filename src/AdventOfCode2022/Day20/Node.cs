using System.Globalization;

namespace AdventOfCode2022.Day20;

internal sealed class Node
{
    public long? Value { get; init; }
    
    public Node? Previous { get; set; }

    public Node? Next { get; set; }

    public override string ToString() => Value?.ToString(CultureInfo.InvariantCulture) ?? "start";
}
