// Workaround for https://github.com/dotnet/roslyn-analyzers/issues/6141
#pragma warning disable CA1852

using AdventOfCode2022;
using System.Diagnostics;
using System.Reflection;

Type[] dayTypes = Assembly.GetExecutingAssembly().GetTypes()
    .Where(t => !t.IsAbstract && typeof(IDay).IsAssignableFrom(t))
    .OrderBy(t => t.Namespace)
    .ToArray();

int dayStart;
int dayEnd;
int partStart;
int partEnd;

switch (args.Length)
{
    case 0:
        dayStart = 1;
        dayEnd = dayTypes.Length;
        partStart = 1;
        partEnd = 2;
        break;
    case 1 when int.TryParse(args[0], out dayStart) && dayStart is >= 1 and <= 25:
        dayEnd = dayStart;
        partStart = 1;
        partEnd = 2;
        break;
    case 2 when int.TryParse(args[0], out dayStart) && dayStart is >= 1 and <= 25 &&
                int.TryParse(args[1], out partStart) && partStart is >= 1 and <= 2:
        dayEnd = dayStart;
        partEnd = partStart;
        break;
    default:
        Console.WriteLine($"Usage: {Environment.GetCommandLineArgs()[0]} [day] [part]");
        Console.WriteLine("  day: [1..25] The day number");
        Console.WriteLine("  part: [1..2] The part number");
        return 1;
}

var stopwatch = Stopwatch.StartNew();
TimeSpan last = TimeSpan.Zero;
for (int dayNumber = dayStart; dayNumber <= dayEnd; dayNumber++)
{
    var day = (IDay)Activator.CreateInstance(dayTypes[dayNumber - 1])!;
    for (int partNumber = partStart; partNumber <= partEnd; partNumber++)
    {
        using var input = new StreamReader(File.OpenRead($"Day{dayNumber:00}\\input.txt"));
        string result = partNumber == 1 ? day.PartOne(input) : day.PartTwo(input);
        TimeSpan elapsed = stopwatch.Elapsed;
        Console.WriteLine($"Day {dayNumber:00}, Part {partNumber}, Duration: {elapsed - last}: {result}");
        last = elapsed;
    }
}
Console.WriteLine($"Total Duration: {stopwatch.Elapsed}");
stopwatch.Stop();

return 0;
