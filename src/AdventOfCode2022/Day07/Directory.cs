namespace AdventOfCode2022.Day07;

internal sealed class Directory : Node
{
    private readonly List<Node> _children = new();

    public override IEnumerable<Node> Children => _children;

    public override long GetSize() => _children.Sum(c => c.GetSize());

    internal void Add(Node node)
    {
        _children.Add(node);
    }

    internal void Remove(Node node)
    {
        _children.Remove(node);
    }
}
