namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Terms of use frame (ID3v2.3)
/// </summary>
public class TermsOfUseFrame : Frame
{
    /// <summary>
    /// Language
    /// </summary>
    public string Language { get; set; }
    /// <summary>
    /// The actual text
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public TermsOfUseFrame() : base()
    {
        Language = "lng";
        Text = "";
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal TermsOfUseFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        var enc = (Encoding)data[0];
        Language = data[1..4].ToISO_8859_1();
        Text = data[4..].ToID3v2_3String(enc);
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {Text}",
            "A" => $"{ID.String()}: (Terms Of Use)\n" +
                   $"  Language: {Language}\n" +
                   $"  Text: {Text}",
            _ => throw new FormatException()
        };
    }
}
