namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class UniqueFileIDFrame : Frame
{
    public string OwnerID { get; set; }
    public byte[] Identifier { get; set; }

    public UniqueFileIDFrame() : base()
    {
        OwnerID = "";
        Identifier = Array.Empty<byte>();
    }

    internal UniqueFileIDFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        int pos = 0;
        OwnerID = data.ToID3v2_3String(Encoding.ISO_8859_1, ref pos);
        Identifier = data[pos..].ToArray();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {OwnerID}",
            "A" => $"{ID.String()}: (Unique File ID)\n" +
                   $"  Owner ID: {OwnerID}\n" +
                   $"  Identifier: {BitConverter.ToString(Identifier)}",
            _ => throw new FormatException()
        };
    }
}
