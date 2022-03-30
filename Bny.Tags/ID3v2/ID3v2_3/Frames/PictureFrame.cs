namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class PictureFrame : Frame
{
    public string MIMEType { get; set; }
    public PictureType PictureType { get; set; }
    public string Description { get; set; }
    public byte[] PictureData { get; set; }

    public PictureFrame() : base()
    {
        MIMEType = "image/";
        PictureType = PictureType.Other;
        Description = "";
        PictureData = Array.Empty<byte>();
    }

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
}

public enum PictureType : byte
{
    Other = 0x00,
    FileIcon32 = 0x01,
    OtherFleIcon = 0x02,
    CoverFrom = 0x03,
    CoverBack = 0x04,
    LeafletPage = 0x05,
    Media = 0x06,
    LeadPerformer = 0x07,
    Artist = 0x08,
    Conductor = 0x09,
    Band = 0x0a,
    Composer = 0x0b,
    Lyricist = 0x0c,
    RecordingLocatoin = 0x0d,
    DuringRecording = 0x0e,
    DuringPerformance = 0x0f,
    MovieScreenCapture = 0x10,
    BrightColouredFish = 0x11,
    Illustration = 0x12,
    ArtistLogotype = 0x13,
    PublisherLogotype = 0x14,
}
