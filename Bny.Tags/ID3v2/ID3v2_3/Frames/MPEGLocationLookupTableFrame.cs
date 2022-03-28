namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class MPEGLocationLookupTableFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public ushort MPEGFramesBetweenReference { get; set; }
    public uint BytesBetweenReference { get; set; }
    public uint MillisecondsBetweenreference { get; set; }
    public byte BitsForBytesDeviation { get; set; }
    public byte BitsForMillisecondsDeviation { get; set; }

    public MPEGLocationLookupTableFrame()
    {
        Header = default;
        MPEGFramesBetweenReference = 0;
        BytesBetweenReference = 0;
        MillisecondsBetweenreference = 0;
        BitsForBytesDeviation = 0;
        BitsForMillisecondsDeviation = 0;
    }

    internal MPEGLocationLookupTableFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
        MPEGFramesBetweenReference = data.ToUInt16();
        BytesBetweenReference = data[2..].ToUInt24();
        MillisecondsBetweenreference = data[5..].ToUInt24();
        BitsForBytesDeviation = data[8];
        BitsForMillisecondsDeviation = data[9];
    }

    public override string ToString()
    {
        return ToString("G");
    }

    public string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.ToString(),
            "C" => $"{ID}: {MPEGFramesBetweenReference} Frames / Reference",
            "A" => $"{ID}: (MPEG Location Lookup table)\n" +
                   $"  MPEG Frames Between Reference: {MPEGFramesBetweenReference}\n" +
                   $"  Bytes Between Reference: {BytesBetweenReference}\n" +
                   $"  Milliseconds Between Reference: {MillisecondsBetweenreference}\n" +
                   $"  Bits For Bytes Deviation: {BitsForBytesDeviation}\n" +
                   $"  Bits For Milliseconds Deviation: {BitsForMillisecondsDeviation}",
            _ => throw new FormatException()
        };
    }
}
