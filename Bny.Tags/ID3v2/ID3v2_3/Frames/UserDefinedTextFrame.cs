namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

internal class UserDefinedTextFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public string Description { get; set; }
    public string Value { get; set; }

    public UserDefinedTextFrame()
    {
        Header = default;
        Description = "";
        Value = "";
    }

    public UserDefinedTextFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
        var enc = (Encoding)data[0];
        int pos = 1;
        Description = data[1..].ToID3v2_3String(enc, ref pos);
        Value = data[pos..].ToID3v2_3String(enc);
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
            "A" => $"{ID}: (User Defined Text)\n" +
                   $"  Description: {Description}\n" +
                   $"  Value: {Value}",
            _ => throw new FormatException()
        };
    }
}
