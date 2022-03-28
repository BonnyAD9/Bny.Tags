namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class EventTimingFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public TimeStampFormat Format { get; set; }
    public List<EventTime> Events { get; set; }

    public EventTimingFrame()
    {
        Header = default;
        Format = TimeStampFormat.MPEGFrames;
        Events = new();
    }

    internal EventTimingFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
        Events = new(data.Length / EventTime.size + 1);
        for (int i = 0; i < data.Length; i += EventTime.size)
            Events.Add(new(data[i..]));
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
            "C" => $"{ID}: {Events.Count} Events",
            "A" => $"{ID}: (Event Timing)\n" +
                   $"  Format: {Format}\n" +
                   $"  Events: {string.Join(", ", Events.Select(p => p.ToString()))}",
            _ => throw new FormatException()
        };
    }
}
