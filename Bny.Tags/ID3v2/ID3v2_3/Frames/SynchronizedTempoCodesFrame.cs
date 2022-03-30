namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Synchronized tempo codes (ID3v2.3)
/// Allows more accurate description of the temo of a musical piece
/// </summary>
public class SynchronizedTempoCodesFrame : Frame
{
    /// <summary>
    /// Time stamp format
    /// </summary>
    public TimeStampFormat Format { get; set; }
    /// <summary>
    /// Tempo data
    /// </summary>
    public List<TempoCode> TempoData { get; set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public SynchronizedTempoCodesFrame() : base()
    {
        Format = TimeStampFormat.MPEGFrames;
        TempoData = new();
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
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
