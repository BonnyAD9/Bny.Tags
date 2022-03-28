namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class LinkedInformationFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public uint FrameID { get; set; }
    public string URL { get; set; }
    public FrameID LinkedID { get; set; }
    public byte[] AdditionalData { get; set; }

    public LinkedInformationFrame()
    {
        Header = default;
        FrameID = 0;
        URL = "";
        LinkedID = ID3v2_3.FrameID.None;
        AdditionalData = Array.Empty<byte>();
    }

    internal LinkedInformationFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
        FrameID = data.ToUInt24();
        int pos = 3;
        URL = data[3..].ToID3v2_3String(Encoding.ISO_8859_1, ref pos);
        LinkedID = (FrameID)data[pos..].ToUInt24();
        pos += 4;
        AdditionalData = data[pos..].ToArray();
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
            "C" => $"{ID}: {LinkedID}",
            "A" => $"{ID}: (Linked Information)\n" +
                   $"  Frame ID: {FrameID}\n" +
                   $"  URL: {URL}\n" +
                   $"  Linked ID: {LinkedID}\n" +
                   $"  Additional Data: {AdditionalData.Length} B",
            _ => throw new FormatException()
        };
    }
}
