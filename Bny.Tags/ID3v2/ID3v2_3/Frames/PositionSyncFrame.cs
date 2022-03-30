namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class PositionSyncFrame : Frame
{
    public TimeStampFormat TimeStampFormat { get; set; }
    public uint Position { get; set; }

    public PositionSyncFrame() : base()
    {
        TimeStampFormat = TimeStampFormat.MPEGFrames;
        Position = 0;
    }

    internal PositionSyncFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        TimeStampFormat = (TimeStampFormat)data[0];
        Position = data[1..].ToUInt32();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {Position}",
            "A" => $"{ID.String()}: (Position Synchronization)\n" +
                   $"  Time Stamp Format: {TimeStampFormat}\n" +
                   $"  Position: {Position}",
            _ => throw new FormatException()
        };
    }
}
