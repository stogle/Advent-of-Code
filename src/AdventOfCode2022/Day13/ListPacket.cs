using System.Collections;

namespace AdventOfCode2022.Day13;

internal sealed class ListPacket : Packet, IEnumerable<Packet>
{
    public new static ListPacket Parse(TextReader line)
    {
        var result = new ListPacket();
        line.Read(); // '['
        int c;
        while ((c = line.Peek()) != -1 && c != ']')
        {
            result.Add(Packet.Parse(line));
            if (line.Peek() == ']')
            {
                break;
            }

            line.Read(); // ','
        }

        line.Read(); // ']'
        return result;
    }

    public IList<Packet> Packets { get; } = new List<Packet>();

    public void Add(Packet packet) => Packets.Add(packet);

    public override int CompareTo(Packet? other) =>
        other switch
        {
            null => 1,
            IntegerPacket otherInteger => CompareTo(new ListPacket {otherInteger}),
            ListPacket otherList => Packets.Zip(otherList.Packets)
                .Select(pair => (int?) pair.First.CompareTo(pair.Second))
                .FirstOrDefault(r => r != 0) ?? Packets.Count.CompareTo(otherList.Packets.Count),
            _ => throw new ArgumentOutOfRangeException(nameof(other))
        };

    public IEnumerator<Packet> GetEnumerator() =>
        Packets.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    public override string ToString() =>
        string.Concat('[', string.Join(',', Packets), ']');
}