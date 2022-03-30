namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Private frame (ID3v2.3)
/// </summary>
public class PrivateFrame : Frame
{
    /// <summary>
    /// Owner identifier
    /// </summary>
    public string OwnerID { get; set; }
    /// <summary>
    /// Binary data
    /// </summary>
    public byte[] Data { get; set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public PrivateFrame() : base()
    {
        OwnerID = "";
        Data = Array.Empty<byte>();
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal PrivateFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        int pos = 0;
        OwnerID = data.ToID3v2_3String(Encoding.ISO_8859_1, ref pos);
        Data = data[pos..].ToArray();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {OwnerID}",
            "A" => $"{ID.String()}: (Private Frame)\n" +
                   $"  Owner ID: {OwnerID}\n" +
                   $"  Data: {Data.Length} B",
            _ => throw new FormatException()
        };
    }
}
