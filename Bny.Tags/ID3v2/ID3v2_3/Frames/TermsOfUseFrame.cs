namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class TermsOfUseFrame : Frame
{
    public string Language { get; set; }
    public string Text { get; set; }

    public TermsOfUseFrame() : base()
    {
        Language = "lng";
        Text = "";
    }

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
