namespace AdventOfCode2022.Day21;

internal sealed record VariableExpression(string Name) : Expression
{
    public override bool TryGetValue(out long value)
    {
        value = 0;
        return false;
    }

    public override long SolveEquality(long value) =>
        value;

    public override string ToString() =>
        Name;
}
