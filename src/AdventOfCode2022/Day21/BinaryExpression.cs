namespace AdventOfCode2022.Day21;

internal sealed record BinaryExpression(Operation Operation, Expression First, Expression Second) : Expression
{
    public override bool TryGetValue(out long value)
    {
        if (First.TryGetValue(out long first) && Second.TryGetValue(out long second))
        {
            value = Operation switch
            {
                Operation.Add => first + second,
                Operation.Subtract => first - second,
                Operation.Multiply => first * second,
                Operation.Divide => first / second,
                _ => throw new InvalidOperationException()
            };

            return true;
        }

        value = 0;
        return false;
    }

    public override long SolveEquality(long value)
    {
        if (First.TryGetValue(out long first))
        {
            value = Operation switch
            {
                Operation.Add => value - first,
                Operation.Subtract => first - value,
                Operation.Multiply => value / first,
                Operation.Divide => first / value,
                _ => throw new InvalidOperationException()
            };
            return Second.SolveEquality(value);
        }

        if (Second.TryGetValue(out long second))
        {
            value = Operation switch
            {
                Operation.Add => value - second,
                Operation.Subtract => value + second,
                Operation.Multiply => value / second,
                Operation.Divide => value * second,
                _ => throw new InvalidOperationException()
            };
            return First.SolveEquality(value);
        }

        throw new NotImplementedException("Unknowns on both sides");
    }

    public override string ToString()
    {
        char op = Operation switch
        {
            Operation.Add => '+',
            Operation.Subtract => '-',
            Operation.Multiply => '*',
            Operation.Divide => '/',
            _ => '?'
        };
        return $"({First} {op} {Second})";
    }
}
