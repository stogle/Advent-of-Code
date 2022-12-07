namespace AdventOfCode2022.Day21;

internal sealed class Equality
{
    private readonly Expression _lhs;
    private readonly Expression _rhs;

    public Equality(IDictionary<string, Monkey> monkeys)
    {
        Monkey root = monkeys["root"];
        var rootExpression = (BinaryExpression)CreateExpression(root, monkeys);
        _lhs = rootExpression.First;
        _rhs = rootExpression.Second;
    }

    private static Expression CreateExpression(Monkey monkey, IDictionary<string, Monkey> monkeys)
    {
        if (monkey.Name == "humn")
        {
            return new VariableExpression(monkey.Name);
        }

        if (monkey.Operation == Operation.None)
        {
            return new ValueExpression(monkey.Value);
        }

        Expression lhs = CreateExpression(monkeys[monkey.Operand1!], monkeys);
        Expression rhs = CreateExpression(monkeys[monkey.Operand2!], monkeys);
        return new BinaryExpression(monkey.Operation, lhs, rhs);
    }

    public long Solve()
    {
        if (_lhs.TryGetValue(out long lhs))
        {
            return _rhs.SolveEquality(lhs);
        }
        if (_rhs.TryGetValue(out long rhs))
        {
            return _lhs.SolveEquality(rhs);
        }

        throw new NotImplementedException("Unknowns on both sides");
    }

    public override string ToString() => $"{_lhs} = {_rhs}";
}
