namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class MusicCDIDFrame : Frame
{
    public byte[] TOC { get; set; }

    public MusicCDIDFrame() : base()
    {
        TOC = Array.Empty<byte>();
    }

    internal MusicCDIDFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        TOC = data.ToArray();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {TOC.Length} B",
            "A" => $"{ID.String()}: (Music CD ID)\n" +
                   $"  TOC: {TOC.Length} B",
            _ => throw new FormatException()
        };
    }
}
