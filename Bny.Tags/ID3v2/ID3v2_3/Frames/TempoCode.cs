namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public struct TempoCode
{
    public ushort Tempo { get; set; }
    public uint TimeStamp { get; set; }

    public TempoCode()
    {
        Tempo = 0;
        TimeStamp = 0;
    }

    internal TempoCode(ReadOnlySpan<byte> data, out int pos)
    {
        pos = 1;
        Tempo = data[0];
        if (Tempo == 0xFF)
        {
            Tempo += data[1];
            pos++;
        }
        TimeStamp = data[pos..].ToUInt32();
        pos += 4;
    }

    public override string ToString()
    {
        return $"{TimeStamp}: {Tempo}";
    }
}
