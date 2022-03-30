namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class CommentFrame : Frame
{
    public string Language { get; set; }
    public string ContentDescription { get; set; }
    public string Text { get; set; }

    public CommentFrame() : base()
    {
        Language = "lng";
        ContentDescription = "";
        Text = "";
    }

    internal CommentFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        var enc = (Encoding)data[0];
        Language = data[1..4].ToISO_8859_1();
        int pos = 4;
        ContentDescription = data[4..].ToID3v2_3String(enc, ref pos);
        Text = data[pos..].ToID3v2_3String(enc);
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {ContentDescription}",
            "A" => $"{ID.String()} (Comment):\n" + 
                   $"  Language: {Language}\n" +
                   $"  Description: {ContentDescription}\n" +
                   $"  Text: {Text}",
            _ => throw new FormatException()
        };
    }
}
