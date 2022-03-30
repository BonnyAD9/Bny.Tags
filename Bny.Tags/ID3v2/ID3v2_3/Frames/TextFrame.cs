namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Text information frame (ID3v2.3)
/// </summary>
public class TextFrame : Frame
{
    /// <summary>
    /// Text in the frame
    /// </summary>
    public string Information { get; set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public TextFrame() : base()
    {
        Information = "";
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal TextFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        Information = data.ToID3v2_3VariableEncoding();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {Information}",
            "A" => $"{ID.String()}: (Text Frame)\n" +
                   $"  Information: {Information}",
            _ => throw new FormatException()
        };
    }
}
