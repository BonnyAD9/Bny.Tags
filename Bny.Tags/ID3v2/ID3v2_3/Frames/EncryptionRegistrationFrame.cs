namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class EncryptionRegistrationFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public string OwnerID { get; set; }
    public byte MethodSymbol { get; set; }
    public byte[] EncryptionData { get; set; }

    public EncryptionRegistrationFrame()
    {
        Header = default;
        OwnerID = "";
        MethodSymbol = 0;
        EncryptionData = Array.Empty<byte>();
    }

    internal EncryptionRegistrationFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
        int pos = 0;
        OwnerID = data.ToID3v2_3String(Encoding.ISO_8859_1, ref pos);
        MethodSymbol = data[pos];
        pos++;
        EncryptionData = data[pos..].ToArray();
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
            "C" => $"{ID}: {OwnerID}",
            "A" => $"{ID}: (Encryption Registration)\n" +
                   $"  Owner ID: {OwnerID}\n" +
                   $"  MethodSymbol: {MethodSymbol}\n" +
                   $"  Encryption Data: {EncryptionData.Length} B",
            _ => throw new FormatException()
        };
    }
}
