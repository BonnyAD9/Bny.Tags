namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class GroupRegistrationFrame : Frame
{
    public string OwnerID { get; set; }
    public byte GroupSymbol { get; set; }
    public byte[] Data { get; set; }

    public GroupRegistrationFrame() : base()
    {
        OwnerID = "";
        GroupSymbol = 0;
        Data = Array.Empty<byte>();
    }

    internal GroupRegistrationFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        int pos = 0;
        OwnerID = data.ToID3v2_3String(Encoding.ISO_8859_1, ref pos);
        GroupSymbol = data[pos];
        pos++;
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
            "A" => $"{ID.String()}: (Group Registration)\n" +
                   $"  Owner ID: {OwnerID}\n" +
                   $"  Group Symbol: {GroupSymbol}\n" +
                   $"  Data: {Data.Length} B",
            _ => throw new FormatException()
        };
    }
}
