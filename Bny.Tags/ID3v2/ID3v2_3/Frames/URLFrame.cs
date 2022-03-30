namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class URLFrame : Frame
{
    public string URL { get; set; }

    public URLFrame() : base()
    {
        URL = "";
    }

    internal URLFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        URL = data.ToISO_8859_1();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {URL}",
            "A" => $"{ID.String()}: (URL)\n" +
                   $"  URL: {URL}",
            _ => throw new FormatException()
        };
    }
}
