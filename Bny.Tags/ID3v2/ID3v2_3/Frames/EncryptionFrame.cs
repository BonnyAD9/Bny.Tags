namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Audio encryption (ID3v2.3)
/// Indicates whether the audio stream is encrypted
/// </summary>
public class EncryptionFrame : Frame
{
    /// <summary>
    /// Email adress/link to email adress to organization responsible for this encryption
    /// </summary>
    public string OwnerID { get; set; }
    /// <summary>
    /// Start of preview in MPEG frames
    /// </summary>
    public ushort PreviewStart { get; set; }
    /// <summary>
    /// Length of preview in MPEG frames
    /// </summary>
    public ushort PreviewLength { get; set; }
    /// <summary>
    /// Additional info about encryption (binary data)
    /// </summary>
    public byte[] EncryptionInfo { get; set; }

    /// <summary>
    /// Next frame of this type, otherwise null
    /// </summary>
    public EncryptionFrame? Next { get; private set; } = null;

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public EncryptionFrame() : base()
    {
        OwnerID = "";
        PreviewStart = 0;
        PreviewLength = 0;
        EncryptionInfo = Array.Empty<byte>();
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal EncryptionFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        int pos = 0;
        OwnerID = data.ToID3v2_3String(Encoding.ISO_8859_1, ref pos);
        PreviewStart = data[pos..].ToUInt16();
        pos += 2;
        PreviewLength = data[pos..].ToUInt16();
        pos += 2;
        EncryptionInfo = data[pos..].ToArray();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {OwnerID}",
            "A" => $"{ID.String()}: (Encription)\n" +
                   $"  Owner ID: {OwnerID}\n" +
                   $"  Preview Start: {PreviewStart}\n" +
                   $"  Preview Length: {PreviewLength}\n" +
                   $"  Encryption Info: {EncryptionInfo.Length} B",
            _ => throw new FormatException()
        };
    }

    public override bool TryAdd(Frame frame)
    {
        if (frame is EncryptionFrame aenc)
        {
            Add(aenc);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Adds another frame of this type
    /// </summary>
    /// <param name="frame">frame to add</param>
    public void Add(EncryptionFrame frame)
    {
        if (frame.Next is null)
        {
            frame.Next = Next;
            Next = frame;
            return;
        }

        frame.Next.Add(frame);
    }
}
