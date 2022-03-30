namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class SynchronizedTempoCodesFrame : Frame
{
    public TimeStampFormat Format { get; set; }
    public List<TempoCode> TempoData { get; set; }

    public SynchronizedTempoCodesFrame() : base()
    {
        Format = TimeStampFormat.MPEGFrames;
        TempoData = new();
    }

    internal SynchronizedTempoCodesFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        TempoData = new();
        Format = (TimeStampFormat)data[0];
        int step;
        for (int i = 1; i < data.Length; i += step)
            TempoData.Add(new(data, out step));
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {TempoData.Count} codes",
            "A" => $"{ID.String()}: (Synchronized Tempo Codes)\n" +
                   $"  Format: {Format}\n" +
                   $"  Tempo Data: {string.Join(", ", TempoData.Select(p => p.ToString()))}",
            _ => throw new FormatException()
        };
    }
}
