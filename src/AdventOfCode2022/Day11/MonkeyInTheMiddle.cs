using System.Globalization;

namespace AdventOfCode2022.Day11;

public class MonkeyInTheMiddle : IDay
{
    public int DayNumber => 11;

    public string PartOne(TextReader input)
    {
        List<Monkey> monkeys = ReadInput(input).ToList();
        long monkeyBusiness = Execute(monkeys, 20, w => w / 3);

        return monkeyBusiness.ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        List<Monkey> monkeys = ReadInput(input).ToList();
        long testLcm = monkeys.Select(m => m.Test).Aggregate(Lcm);
        long monkeyBusiness = Execute(monkeys, 10_000, w => w % testLcm);

        return monkeyBusiness.ToString(CultureInfo.InvariantCulture);
    }

    private static IEnumerable<Monkey> ReadInput(TextReader input)
    {
        while (input.ReadLine() is { })
        {
            string line = input.ReadLine()!;
            List<long> items = line[18..].Split(", ").Select(long.Parse).ToList();
            line = input.ReadLine()!;
            var operation = Operation.None;
            long? operand = null;
            if (line[23..] == "* old")
            {
                operation = Operation.Squared;
            }
            else if (line[23..].StartsWith("+", StringComparison.InvariantCulture))
            {
                operation = Operation.Plus;
                operand = long.Parse(line[25..], CultureInfo.InvariantCulture);
            }
            else if (line[23..].StartsWith("*", StringComparison.InvariantCulture))
            {
                operation = Operation.Times;
                operand = long.Parse(line[25..], CultureInfo.InvariantCulture);
            }
            line = input.ReadLine()!;
            long test = long.Parse(line[21..], CultureInfo.InvariantCulture);
            line = input.ReadLine()!;
            int ifTrue = int.Parse(line[29..], CultureInfo.InvariantCulture);
            line = input.ReadLine()!;
            int ifFalse = int.Parse(line[30..], CultureInfo.InvariantCulture);
            input.ReadLine();

            yield return new Monkey(items, operation, operand, test, ifTrue, ifFalse);
        }
    }

    private static long Execute(List<Monkey> monkeys, int rounds, Func<long, long> worryLevelAdjustment)
    {
        for (int round = 1; round <= rounds; round++)
        {
            foreach (Monkey monkey in monkeys)
            {
                List<long> items = monkey.Items.ToList();
                foreach (long item in items)
                {
                    long worryLevel = item;
                    switch (monkey.Operation)
                    {
                        case Operation.None:
                            break;
                        case Operation.Plus:
                            worryLevel += monkey.Operand!.Value;
                            break;
                        case Operation.Times:
                            worryLevel *= monkey.Operand!.Value;
                            break;
                        case Operation.Squared:
                            worryLevel *= worryLevel;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(monkeys));
                    }
                    worryLevel = worryLevelAdjustment(worryLevel);

                    if (worryLevel % monkey.Test == 0)
                    {
                        monkeys[monkey.IfTrue].Items.Add(worryLevel);
                    }
                    else
                    {
                        monkeys[monkey.IfFalse].Items.Add(worryLevel);
                    }

                    monkey.Items.Remove(item);
                    monkey.Inspected++;
                }
            }
        }

        return monkeys
            .Select(m => m.Inspected)
            .OrderDescending()
            .Take(2)
            .Aggregate((a, b) => a * b);
    }

    private static long Lcm(long a, long b) => a / Gcd(a, b) * b;

    private static long Gcd(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
}
