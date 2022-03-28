namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class MusicCDIDFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public byte[] TOC { get; set; }

    public MusicCDIDFrame()
    {
        Header = default;
        TOC = Array.Empty<byte>();
    }

    internal MusicCDIDFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
        TOC = data.ToArray();
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
            "C" => $"{ID}: {TOC.Length} B",
            "A" => $"{ID}: (Music CD ID)\n" +
                   $"  TOC: {TOC.Length} B",
            _ => throw new FormatException()
        };
    }
}
