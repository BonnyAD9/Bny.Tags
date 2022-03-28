namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class URLFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public string URL { get; set; }

    public URLFrame()
    {
        Header = default;
        URL = "";
    }

    internal URLFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
        URL = data.ToISO_8859_1();
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
            "C" => $"{ID}: {URL}",
            "A" => $"{ID}: (URL)\n" +
                   $"  URL: {URL}",
            _ => throw new FormatException()
        };
    }
}
