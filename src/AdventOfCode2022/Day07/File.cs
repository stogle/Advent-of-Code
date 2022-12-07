namespace AdventOfCode2022.Day07;

internal sealed class File : Node
{
    public required long Size { get; init; }

    public override IEnumerable<Node> Children { get; } = Enumerable.Empty<Node>();

    public override long GetSize() => Size;
}
