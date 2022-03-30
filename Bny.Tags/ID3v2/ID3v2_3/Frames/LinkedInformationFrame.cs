namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Linked information (ID3v2.3)
/// Link to other file with ID3v2.3 tag with additional information
/// </summary>
public class LinkedInformationFrame : Frame
{
    /// <summary>
    /// Frame identifier
    /// </summary>
    public uint FrameID { get; set; }
    /// <summary>
    /// Link to the file
    /// </summary>
    public string URL { get; set; }
    /// <summary>
    /// ID of the linked frame
    /// </summary>
    public FrameID LinkedID { get; set; }
    /// <summary>
    /// Additional data (e.g. content descriptor)
    /// </summary>
    public byte[] AdditionalData { get; set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public LinkedInformationFrame() : base()
    {
        FrameID = 0;
        URL = "";
        LinkedID = ID3v2_3.FrameID.None;
        AdditionalData = Array.Empty<byte>();
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
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
