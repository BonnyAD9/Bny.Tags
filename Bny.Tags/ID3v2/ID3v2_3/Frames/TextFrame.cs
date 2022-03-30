namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class TextFrame : Frame
{
    public string Information { get; set; }

    public TextFrame() : base()
    {
        Information = "";
    }

    internal TextFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        Information = data.ToID3v2_3VariableEncoding();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {Information}",
            "A" => $"{ID.String()}: (Text Frame)\n" +
                   $"  Information: {Information}",
            _ => throw new FormatException()
        };
    }
}
