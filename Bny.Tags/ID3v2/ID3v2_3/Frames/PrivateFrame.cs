namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class PrivateFrame : Frame
{
    public string OwnerID { get; set; }
    public byte[] Data { get; set; }

    public PrivateFrame() : base()
    {
        OwnerID = "";
        Data = Array.Empty<byte>();
    }

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
