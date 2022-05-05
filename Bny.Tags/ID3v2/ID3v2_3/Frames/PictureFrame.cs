namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Attached picture (ID3v2.3)
/// </summary>
public class PictureFrame : Frame
{
    /// <summary>
    /// MIME type
    /// if omited "image/" is implied
    /// </summary>
    public string MIMEType { get; set; }
    /// <summary>
    /// Picture type
    /// </summary>
    public PictureType PictureType { get; set; }
    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// Binary data of the picture
    /// </summary>
    public byte[] PictureData { get; set; }

    /// <summary>
    /// Next frame of this type, otherwise false
    /// </summary>
    public PictureFrame? Next { get; private set; } = null;

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public PictureFrame() : base()
    {
        MIMEType = "image/";
        PictureType = PictureType.Other;
        Description = "";
        PictureData = Array.Empty<byte>();
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal PictureFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        var enc = (Encoding)data[0];
        int pos = 1;
        MIMEType = data[1..].ToID3v2_3String(Encoding.ISO_8859_1, ref pos);
        PictureType = (PictureType)data[pos];
        pos++;
        Description = data[pos..].ToID3v2_3String(enc, ref pos);
        PictureData = data[pos..].ToArray();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {Description}",
            "A" => $"{ID.String()}: (Picture)\n" +
                   $"  MIME Type: {MIMEType}\n" +
                   $"  Picture Type: {PictureType}\n" +
                   $"  Description: {Description}\n" +
                   $"  Picture Data: {PictureData.Length} B",
            _ => throw new FormatException()
        };
    }

    public override bool TryAdd(Frame frame)
    {
        if (frame is PictureFrame apic)
        {
            Add(apic);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Adds another frame of this type
    /// </summary>
    /// <param name="frame">frame to add</param>
    public void Add(PictureFrame frame)
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

/// <summary>
/// Describes type of picture (ID3v2.3)
/// </summary>
public enum PictureType : byte
{
    Other = 0x00,
    /// <summary>
    /// 32x32 pixels 'file icon' (PNG only)
    /// </summary>
    FileIcon32 = 0x01,
    OtherFleIcon = 0x02,
    /// <summary>
    /// Cover (front)
    /// </summary>
    CoverFront = 0x03,
    /// <summary>
    /// Cover (back)
    /// </summary>
    CoverBack = 0x04,
    LeafletPage = 0x05,
    /// <summary>
    /// Media (e.g. label side of CD)
    /// </summary>
    Media = 0x06,
    /// <summary>
    /// Lead artist/lead performer/soloist
    /// </summary>
    LeadPerformer = 0x07,
    /// <summary>
    /// Artist/performer
    /// </summary>
    Artist = 0x08,
    Conductor = 0x09,
    /// <summary>
    /// Band/orchestra
    /// </summary>
    Band = 0x0a,
    Composer = 0x0b,
    /// <summary>
    /// Lyricist/text writer
    /// </summary>
    Lyricist = 0x0c,
    RecordingLocatoin = 0x0d,
    DuringRecording = 0x0e,
    DuringPerformance = 0x0f,
    /// <summary>
    /// Movie/video screen capture
    /// </summary>
    MovieScreenCapture = 0x10,
    /// <summary>
    /// A bright coloured fish
    /// </summary>
    BrightColouredFish = 0x11,
    Illustration = 0x12,
    /// <summary>
    /// Band/artist logotype
    /// </summary>
    ArtistLogotype = 0x13,
    /// <summary>
    /// Publisher/studio logotype
    /// </summary>
    PublisherLogotype = 0x14,
}
