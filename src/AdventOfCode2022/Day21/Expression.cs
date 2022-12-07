namespace AdventOfCode2022.Day21;

internal abstract record Expression
{
    public abstract bool TryGetValue(out long value);

    public abstract long SolveEquality(long value);
}
