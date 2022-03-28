namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

internal class UserDefinedURLFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public string Description { get; set; }
    public string URL { get; set; }

    public UserDefinedURLFrame()
    {
        Header = default;
        Description = "";
        URL = "";
    }

    public UserDefinedURLFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
        int pos = 0;
        Description = data.ToID3v2_3String(ref pos);
        URL = data[pos..].ToISO_8859_1();
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
            "C" => $"{ID}: {Description}",
            "A" => $"{ID}: (User Defined URL)\n" +
                   $"  Description: {Description}\n" +
                   $"  URL: {URL}",
            _ => throw new FormatException()
        };
    }
}
