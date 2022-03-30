namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class LinkedInformationFrame : Frame
{
    public uint FrameID { get; set; }
    public string URL { get; set; }
    public FrameID LinkedID { get; set; }
    public byte[] AdditionalData { get; set; }

    public LinkedInformationFrame() : base()
    {
        FrameID = 0;
        URL = "";
        LinkedID = ID3v2_3.FrameID.None;
        AdditionalData = Array.Empty<byte>();
    }

    internal LinkedInformationFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        FrameID = data.ToUInt24();
        int pos = 3;
        URL = data[3..].ToID3v2_3String(Encoding.ISO_8859_1, ref pos);
        LinkedID = (FrameID)data[pos..].ToUInt24();
        pos += 4;
        AdditionalData = data[pos..].ToArray();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {LinkedID}",
            "A" => $"{ID.String()}: (Linked Information)\n" +
                   $"  Frame ID: {FrameID}\n" +
                   $"  URL: {URL}\n" +
                   $"  Linked ID: {LinkedID}\n" +
                   $"  Additional Data: {AdditionalData.Length} B",
            _ => throw new FormatException()
        };
    }
}
