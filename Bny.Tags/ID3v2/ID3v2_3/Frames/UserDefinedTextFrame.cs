namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class UserDefinedTextFrame : Frame
{
    public string Description { get; set; }
    public string Value { get; set; }

    public UserDefinedTextFrame() : base()
    {
        Description = "";
        Value = "";
    }

    internal UserDefinedTextFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        var enc = (Encoding)data[0];
        int pos = 1;
        Description = data[1..].ToID3v2_3String(enc, ref pos);
        Value = data[pos..].ToID3v2_3String(enc);
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {Description}",
            "A" => $"{ID.String()}: (User Defined Text)\n" +
                   $"  Description: {Description}\n" +
                   $"  Value: {Value}",
            _ => throw new FormatException()
        };
    }
}
