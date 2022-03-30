namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Event timing codes (ID3v2.3)
/// Allows synchronization with key events in a song or sound
/// </summary>
public class EventTimingFrame : Frame
{
    /// <summary>
    /// Format of the time stamps
    /// </summary>
    public TimeStampFormat Format { get; set; }
    /// <summary>
    /// Key events in order
    /// </summary>
    public List<EventTime> Events { get; set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public EventTimingFrame() : base()
    {
        Format = TimeStampFormat.MPEGFrames;
        Events = new();
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal EventTimingFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        Events = new(data.Length / EventTime.size + 1);
        for (int i = 0; i < data.Length; i += EventTime.size)
            Events.Add(new(data[i..]));
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {Events.Count} Events",
            "A" => $"{ID.String()}: (Event Timing)\n" +
                   $"  Format: {Format}\n" +
                   $"  Events: {string.Join(", ", Events.Select(p => p.ToString()))}",
            _ => throw new FormatException()
        };
    }
}
