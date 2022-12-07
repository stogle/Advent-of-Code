using System.Globalization;

namespace AdventOfCode2022.Day21;

internal sealed record ValueExpression(long Value) : Expression
{
    public override bool TryGetValue(out long value)
    {
        value = Value;
        return true;
    }

    public override long SolveEquality(long value) =>
        throw new InvalidOperationException("No unknown in equality");

    public override string ToString() =>
        Value.ToString(CultureInfo.InvariantCulture);
}
