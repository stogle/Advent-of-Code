namespace AdventOfCode2022;

public interface IDay
{
    int DayNumber { get; }
    string PartOne(TextReader input);
    string PartTwo(TextReader input);
}
