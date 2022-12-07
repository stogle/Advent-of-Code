namespace AdventOfCode2022.Day07;

internal abstract class Node
{
    private Directory? _parent;

    public Directory? Parent
    {
        get => _parent;
        set
        {
            _parent?.Remove(this);
            _parent = value;
            _parent?.Add(this);
        }
    }

    public required string Name { get; init; }

    public abstract IEnumerable<Node> Children { get; }

    public IEnumerable<Node> GetDescendants()
    {
        foreach (Node child in Children)
        {
            yield return child;
            foreach (Node descendant in child.GetDescendants())
            {
                yield return descendant;
            }
        }
    }

    public abstract long GetSize();
}
