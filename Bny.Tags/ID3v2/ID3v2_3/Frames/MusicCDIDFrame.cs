namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Music CD ifentifier (ID3v2.3)
/// Contains binary dump of the Table Of Contents, TOC, from the CD
/// </summary>
public class MusicCDIDFrame : Frame
{
    /// <summary>
    /// Binary dump of the Table Of Contents, TOC, from the CD
    /// </summary>
    public byte[] TOC { get; set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public MusicCDIDFrame() : base()
    {
        TOC = Array.Empty<byte>();
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal MusicCDIDFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        TOC = data.ToArray();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {TOC.Length} B",
            "A" => $"{ID.String()}: (Music CD ID)\n" +
                   $"  TOC: {TOC.Length} B",
            _ => throw new FormatException()
        };
    }
}
