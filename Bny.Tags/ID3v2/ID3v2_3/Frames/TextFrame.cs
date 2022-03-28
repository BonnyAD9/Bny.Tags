namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class TextFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public string Information { get; set; }

    public TextFrame()
    {
        Header = default;
        Information = "";
    }

    internal TextFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
        Information = data.ToID3v2_3VariableEncoding();
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
            "C" => $"{ID}: {Information}",
            "A" => $"{ID}: (Text Frame)\n" +
                   $"  Information: {Information}",
            _ => throw new FormatException()
        };
    }
}
