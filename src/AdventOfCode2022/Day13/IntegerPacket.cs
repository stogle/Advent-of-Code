using System.Globalization;
using System.Text;

namespace AdventOfCode2022.Day13;

internal sealed class IntegerPacket : Packet
{
    public new static IntegerPacket Parse(TextReader line)
    {
        var builder = new StringBuilder();
        int c;
        while ((c = line.Peek()) != -1 && char.IsAsciiDigit((char)c))
        {
            builder.Append((char)line.Read());
        }

        return new IntegerPacket { Value = int.Parse(builder.ToString(), CultureInfo.InvariantCulture) };
    }

    public required int Value { get; init; }

    public override int CompareTo(Packet? other) =>
        other switch
        {
            null => 1,
            IntegerPacket otherInteger => Value.CompareTo(otherInteger.Value),
            ListPacket otherList => -otherList.CompareTo(this),
            _ => throw new ArgumentOutOfRangeException(nameof(other))
        };

    public override string ToString() =>
        Value.ToString(CultureInfo.InvariantCulture);
}
