namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Encryption method registration
/// </summary>
public class EncryptionRegistrationFrame : Frame
{
    /// <summary>
    /// Email adress/link to email adress of the organization responsible for this specific encryption
    /// </summary>
    public string OwnerID { get; set; }
    /// <summary>
    /// Method symbol
    /// Values below 0x80 are reserved
    /// </summary>
    public byte MethodSymbol { get; set; }
    /// <summary>
    /// Additional encryption data
    /// </summary>
    public byte[] EncryptionData { get; set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public EncryptionRegistrationFrame() : base()
    {
        OwnerID = "";
        MethodSymbol = 0;
        EncryptionData = Array.Empty<byte>();
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal EncryptionRegistrationFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        int pos = 0;
        OwnerID = data.ToID3v2_3String(Encoding.ISO_8859_1, ref pos);
        MethodSymbol = data[pos];
        pos++;
        EncryptionData = data[pos..].ToArray();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {OwnerID}",
            "A" => $"{ID.String()}: (Encryption Registration)\n" +
                   $"  Owner ID: {OwnerID}\n" +
                   $"  MethodSymbol: {MethodSymbol}\n" +
                   $"  Encryption Data: {EncryptionData.Length} B",
            _ => throw new FormatException()
        };
    }
}
