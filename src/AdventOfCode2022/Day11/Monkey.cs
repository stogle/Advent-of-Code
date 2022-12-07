namespace AdventOfCode2022.Day11;

internal sealed record Monkey(List<long> Items, Operation Operation, long? Operand, long Test, int IfTrue, int IfFalse)
{
    public long Inspected { get; set; }
}
