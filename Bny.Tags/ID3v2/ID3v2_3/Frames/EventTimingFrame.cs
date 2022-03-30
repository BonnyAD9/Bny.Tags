namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class EventTimingFrame : Frame
{
    public TimeStampFormat Format { get; set; }
    public List<EventTime> Events { get; set; }

    public EventTimingFrame() : base()
    {
        Format = TimeStampFormat.MPEGFrames;
        Events = new();
    }

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
