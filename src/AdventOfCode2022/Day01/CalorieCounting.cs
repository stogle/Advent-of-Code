using System.Globalization;

namespace AdventOfCode2022.Day01;

public class CalorieCounting : IDay
{
    public int DayNumber => 1;

    public string PartOne(TextReader input)
    {
        return ReadInput(input)
            .Max(foods => foods.Sum())
            .ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        return ReadInput(input)
            .Select(foods => foods.Sum())
            .OrderByDescending(foods => foods)
            .Take(3)
            .Sum()
            .ToString(CultureInfo.InvariantCulture);
    }

    private static IEnumerable<List<int>> ReadInput(TextReader input)
    {
        while (input.ReadLine() is { } line)
        {
            var foods = new List<int>();
            do
            {
                int calories = int.Parse(line, CultureInfo.InvariantCulture);
                foods.Add(calories);
            } while ((line = input.ReadLine()) != null && line.Length != 0);
            yield return foods;
        }
    }
}
