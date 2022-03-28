namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class RecommendedBufferSizeFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public uint BufferSize { get; set; }
    public bool EmbededInfo { get; set; }
    public uint OffsetToNextTag { get; set; }

    public RecommendedBufferSizeFrame()
    {
        Header = default;
        BufferSize = 0;
        EmbededInfo = false;
        OffsetToNextTag = 0;
    }

    internal RecommendedBufferSizeFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
        BufferSize = data.ToUInt24();
        EmbededInfo = (data[3] & 1) == 1;
        OffsetToNextTag = data[4..].ToUInt32();
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
            "C" => $"{ID}: {BufferSize}",
            "A" => $"{ID}: (Recomended Buffer Size)\n" +
                   $"  Buffer Size: {BufferSize}\n" +
                   $"  Embeded Info: {EmbededInfo}\n" +
                   $"  Offset To Next Tag: {OffsetToNextTag}",
            _ => throw new FormatException()
        };
    }
}
