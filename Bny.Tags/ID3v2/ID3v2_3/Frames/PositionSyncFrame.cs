namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class PositionSyncFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public TimeStampFormat TimeStampFormat { get; set; }
    public uint Position { get; set; }

    public PositionSyncFrame()
    {
        Header = default;
        TimeStampFormat = TimeStampFormat.MPEGFrames;
        Position = 0;
    }

    internal PositionSyncFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
        TimeStampFormat = (TimeStampFormat)data[0];
        Position = data[1..].ToUInt32();
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
            "C" => $"{ID}: {Position}",
            "A" => $"{ID}: (Position Synchronization)\n" +
                   $"  Time Stamp Format: {TimeStampFormat}\n" +
                   $"  Position: {Position}",
            _ => throw new FormatException()
        };
    }
}
