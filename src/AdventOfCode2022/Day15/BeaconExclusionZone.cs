using System.Globalization;

namespace AdventOfCode2022.Day15;

public class BeaconExclusionZone : IDay
{
    private readonly int _partOneRow;
    private readonly int _partTwoLimit;

    public BeaconExclusionZone() :
        this(2_000_000, 4_000_000)
    {
    }

    public BeaconExclusionZone(int partOneRow, int partTwoLimit)
    {
        _partOneRow = partOneRow;
        _partTwoLimit = partTwoLimit;
    }

    public int DayNumber => 15;

    public string PartOne(TextReader input)
    {
        List<(int SensorX, int SensorY, int BeaconX, int BeaconY, int Distance)> sensors = ReadInput(input);

        var beaconInRow = new HashSet<int>();
        var noBeaconInRow = new HashSet<int>();
        foreach ((int sensorX, int sensorY, int beaconX, int beaconY, int distance) in sensors)
        {
            if (beaconY == _partOneRow)
            {
                beaconInRow.Add(beaconX);
            }

            int distanceAtRow = distance - Math.Abs(_partOneRow - sensorY);
            for (int i = sensorX - distanceAtRow; i <= sensorX + distanceAtRow; i++)
            {
                noBeaconInRow.Add(i);
            }
        }

        return noBeaconInRow.Except(beaconInRow).Count().ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        List<(int SensorX, int SensorY, int BeaconX, int BeaconY, int Distance)> sensors = ReadInput(input);

        long? tuningFrequency = null;
        Parallel.ForEach(
            Enumerable.Range(0, _partTwoLimit + 1),
            () => new List<(int Start, int End)>(),
            (y, parallelLoopState, noBeaconIntervals) =>
            {
                foreach ((int sensorX, int sensorY, int _, int _, int distance) in sensors)
                {
                    int distanceAtRow = distance - Math.Abs(y - sensorY);
                    if (distanceAtRow >= 0)
                    {
                        int start = Math.Max(0, sensorX - distanceAtRow);
                        int end = Math.Min(sensorX + distanceAtRow, _partTwoLimit);
                        int index = noBeaconIntervals.FindIndex(i => start < i.Start);
                        if (index == -1)
                        {
                            index = noBeaconIntervals.Count;
                        }
                        noBeaconIntervals.Insert(index, (start, end));
                    }
                }

                // Find beacon in this row
                int x = 0;
                foreach ((int s, int e) in noBeaconIntervals)
                {
                    if (x < s)
                    {
                        tuningFrequency = x * 4_000_000L + y;
                        parallelLoopState.Break();
                    }
                    x = Math.Max(e + 1, x);
                }

                noBeaconIntervals.Clear();
                return noBeaconIntervals;
            },
            _ => { });

        return tuningFrequency?.ToString(CultureInfo.InvariantCulture) ?? "";
    }

    private static List<(int SensorX, int SensorY, int BeaconX, int BeaconY, int Distance)> ReadInput(TextReader input)
    {
        var sensors = new List<(int SensorX, int SensorY, int BeaconX, int BeaconY, int Distance)>();
        while (input.ReadLine() is { } line)
        {
            string[] split = line.Split(": ").SelectMany(s => s.Split(", ")).ToArray();
            int sensorX = int.Parse(split[0][12..], CultureInfo.InvariantCulture);
            int sensorY = int.Parse(split[1][2..], CultureInfo.InvariantCulture);
            int beaconX = int.Parse(split[2][23..], CultureInfo.InvariantCulture);
            int beaconY = int.Parse(split[3][2..], CultureInfo.InvariantCulture);
            int distance = Math.Abs(beaconX - sensorX) + Math.Abs(beaconY - sensorY);
            sensors.Add((sensorX, sensorY, beaconX, beaconY, distance));
        }

        return sensors;
    }
}
