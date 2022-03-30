namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// This is used for unknown frame IDs
/// </summary>
public class UnknownFrame : Frame
{
    /// <summary>
    /// Data in the fra,e
    /// </summary>
    public byte[] Data { get; set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public UnknownFrame() : base()
    {
        Data = Array.Empty<byte>();
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal UnknownFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        Data = data.ToArray();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {Data.Length} B",
            "A" => $"{ID.String()} (Unknown):\n" +
                   $"  Data: {Data.Length} B",
            _ => throw new FormatException()
        };
    }
}