namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class UserDefinedURLFrame : Frame
{
    public string Description { get; set; }
    public string URL { get; set; }

    public UserDefinedURLFrame() : base()
    {
        Description = "";
        URL = "";
    }

    internal UserDefinedURLFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        int pos = 0;
        Description = data.ToID3v2_3String(ref pos);
        URL = data[pos..].ToISO_8859_1();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {Description}",
            "A" => $"{ID.String()}: (User Defined URL)\n" +
                   $"  Description: {Description}\n" +
                   $"  URL: {URL}",
            _ => throw new FormatException()
        };
    }
}
