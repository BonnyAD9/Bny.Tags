namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Recommended buffer size (ID3v2.3)
/// Helps when streaming
/// </summary>
public class RecommendedBufferSizeFrame : Frame
{
    /// <summary>
    /// Buffer size
    /// </summary>
    public uint BufferSize { get; set; }
    /// <summary>
    /// Indicates that an ID3 tag with the maximum size described in 'BufferSize' may occur en the audiostream
    /// </summary>
    public bool EmbededInfo { get; set; }
    /// <summary>
    /// Offset to the next tag
    /// </summary>
    public uint OffsetToNextTag { get; set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public RecommendedBufferSizeFrame() : base()
    {
        BufferSize = 0;
        EmbededInfo = false;
        OffsetToNextTag = 0;
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal RecommendedBufferSizeFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        BufferSize = data.ToUInt24();
        EmbededInfo = (data[3] & 1) == 1;
        OffsetToNextTag = data[4..].ToUInt32();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {BufferSize}",
            "A" => $"{ID.String()}: (Recomended Buffer Size)\n" +
                   $"  Buffer Size: {BufferSize}\n" +
                   $"  Embeded Info: {EmbededInfo}\n" +
                   $"  Offset To Next Tag: {OffsetToNextTag}",
            _ => throw new FormatException()
        };
    }
}
