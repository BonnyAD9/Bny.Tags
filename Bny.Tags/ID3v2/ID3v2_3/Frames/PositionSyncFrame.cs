namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Position synchronization frame (ID3v2.3)
/// Information about how far the listener picked up
/// </summary>
public class PositionSyncFrame : Frame
{
    /// <summary>
    /// Format of time stamps
    /// </summary>
    public TimeStampFormat Format { get; set; }
    /// <summary>
    /// Position in the audio
    /// May be in Milliseconds or MPEG frames
    /// </summary>
    public uint Position { get; set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public PositionSyncFrame() : base()
    {
        Format = TimeStampFormat.MPEGFrames;
        Position = 0;
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal PositionSyncFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        Format = (TimeStampFormat)data[0];
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
                   $"  Time Stamp Format: {Format}\n" +
                   $"  Position: {Position}",
            _ => throw new FormatException()
        };
    }
}
