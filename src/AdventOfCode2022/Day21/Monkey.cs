namespace AdventOfCode2022.Day21;

internal sealed record Monkey(string Name)
{
    public long Value { get; set; }
    public Operation Operation { get; init; }
    public string? Operand1 { get; init; }
    public string? Operand2 { get; init; }

    public long GetValue(IDictionary<string, Monkey> monkeys) =>
        Operation switch
        {
            Operation.None => Value,
            Operation.Add => monkeys[Operand1!].GetValue(monkeys) + monkeys[Operand2!].GetValue(monkeys),
            Operation.Subtract => monkeys[Operand1!].GetValue(monkeys) - monkeys[Operand2!].GetValue(monkeys),
            Operation.Multiply => monkeys[Operand1!].GetValue(monkeys) * monkeys[Operand2!].GetValue(monkeys),
            Operation.Divide => monkeys[Operand1!].GetValue(monkeys) / monkeys[Operand2!].GetValue(monkeys),
            _ => throw new InvalidOperationException()
        };
}
