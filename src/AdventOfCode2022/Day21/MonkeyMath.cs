using System.Globalization;

namespace AdventOfCode2022.Day21;

public class MonkeyMath : IDay
{
    public int DayNumber => 21;

    public string PartOne(TextReader input)
    {
        IDictionary<string, Monkey> monkeys = ReadInput(input);
        return monkeys["root"].GetValue(monkeys).ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        IDictionary<string, Monkey> monkeys = ReadInput(input);
        var equality = new Equality(monkeys);
        return equality.Solve().ToString(CultureInfo.InvariantCulture);
    }

    private static IDictionary<string, Monkey> ReadInput(TextReader input)
    {
        var monkeys = new Dictionary<string, Monkey>();
        while (input.ReadLine() is { } line)
        {
            string[] split = line.Split(": ");
            string name = split[0];
            string[] split2 = split[1].Split(' ');
            if (split2.Length == 1)
            {
                long value = long.Parse(split2[0], CultureInfo.InvariantCulture);
                monkeys[name] = new Monkey(name) { Value = value };
            }
            else
            {
                Operation operation = split2[1] switch
                {
                    "+" => Operation.Add,
                    "-" => Operation.Subtract,
                    "*" => Operation.Multiply,
                    "/" => Operation.Divide,
                    _ => Operation.None
                };
                monkeys[name] = new Monkey(name) { Operation = operation, Operand1 = split2[0], Operand2 = split2[2] };
            }
        }

        return monkeys;
    }
}
