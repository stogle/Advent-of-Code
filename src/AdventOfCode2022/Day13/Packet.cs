namespace AdventOfCode2022.Day13;

internal abstract class Packet : IComparable<Packet>
{
    public static Packet Parse(TextReader line)
    {
        if (line.Peek() == '[')
        {
            return ListPacket.Parse(line);
        }

        return IntegerPacket.Parse(line);
    }

    public abstract int CompareTo(Packet? other);
}
