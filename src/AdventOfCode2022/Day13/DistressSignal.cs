using System.Globalization;

namespace AdventOfCode2022.Day13;

public class DistressSignal : IDay
{
    public int DayNumber => 13;

    public string PartOne(TextReader input)
    {
        int result = 0;
        int index = 1;
        while (input.ReadLine() is { } line)
        {
            Packet packet1 = Packet.Parse(new StringReader(line));
            line = input.ReadLine()!;
            Packet packet2 = Packet.Parse(new StringReader(line));
            if (packet1.CompareTo(packet2) < 0)
            {
                result += index;
            }
            input.ReadLine();
            index++;
        }

        return result.ToString(CultureInfo.InvariantCulture);
    }

    public string PartTwo(TextReader input)
    {
        var dividerPacket1 = new ListPacket { new ListPacket { new IntegerPacket { Value = 2 } } };
        var dividerPacket2 = new ListPacket { new ListPacket { new IntegerPacket { Value = 6 } } };
        var packets = new List<Packet> { dividerPacket1, dividerPacket2 };
        while (input.ReadLine() is { } line)
        {
            if (line.Length != 0)
            {
                packets.Add(Packet.Parse(new StringReader(line)));
            }
        }
        packets.Sort();

        int decoderKey = (1 + packets.IndexOf(dividerPacket1)) * (1 + packets.IndexOf(dividerPacket2));
        return decoderKey.ToString(CultureInfo.InvariantCulture);
    }
}
