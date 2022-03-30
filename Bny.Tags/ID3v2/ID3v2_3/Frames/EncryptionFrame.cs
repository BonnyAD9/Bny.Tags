namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class EncryptionFrame : Frame
{
    public string OwnerID { get; set; }
    public ushort PreviewStart { get; set; }
    public ushort PreviewLength { get; set; }
    public byte[] EncryptionInfo { get; set; }

    public EncryptionFrame() : base()
    {
        OwnerID = "";
        PreviewStart = 0;
        PreviewLength = 0;
        EncryptionInfo = Array.Empty<byte>();
    }

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
}
