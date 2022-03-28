namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class SynchronizedTempoCodesFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public TimeStampFormat Format { get; set; }
    public List<TempoCode> TempoData { get; set; }

    public SynchronizedTempoCodesFrame()
    {
        Header = default;
        Format = TimeStampFormat.MPEGFrames;
        TempoData = new();
    }

    internal SynchronizedTempoCodesFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
        TempoData = new();
        Format = (TimeStampFormat)data[0];
        int step;
        for (int i = 1; i < data.Length; i += step)
            TempoData.Add(new(data, out step));
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
            "C" => $"{ID}: {TempoData.Count} codes",
            "A" => $"{ID}: (Synchronized Tempo Codes)\n" +
                   $"  Format: {Format}\n" +
                   $"  Tempo Data: {string.Join(", ", TempoData.Select(p => p.ToString()))}",
            _ => throw new FormatException()
        };
    }
}
