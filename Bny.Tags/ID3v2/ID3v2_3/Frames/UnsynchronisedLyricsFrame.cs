namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Unsynchronized lyrics/text transcription (ID3v2.3)
/// </summary>
public class UnsynchronisedLyricsFrame : Frame
{
    /// <summary>
    /// Language
    /// </summary>
    public string Language { get; set; }
    /// <summary>
    /// Content descriptor
    /// </summary>
    public string ContentDescriptor { get; set; }
    /// <summary>
    /// Lyrics/text
    /// </summary>
    public string Lyrics { get; set; }

    /// <summary>
    /// Next frame of this type
    /// </summary>
    public UnsynchronisedLyricsFrame? Next { get; private set; } = null;

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public UnsynchronisedLyricsFrame() : base()
    {
        Language = "lng";
        ContentDescriptor = "";
        Lyrics = "";
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal UnsynchronisedLyricsFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        var enc = (Encoding)data[0];
        Language = data[1..4].ToISO_8859_1();
        int pos = 4;
        ContentDescriptor = data[4..].ToID3v2_3String(enc, ref pos);
        Lyrics = data[pos..].ToID3v2_3String(enc);
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {ContentDescriptor}",
            "A" => $"{ID.String()}: (Unsynchronized Lyrics)\n" +
                   $"  Language: {Language}\n" +
                   $"  Content Descriptor: {ContentDescriptor}\n" +
                   $"  Lyrics: {Lyrics}",
            _ => throw new FormatException()
        };
    }

    public override bool TryAdd(Frame frame)
    {
        if (frame is UnsynchronisedLyricsFrame uslt)
        {
            Add(uslt);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Adds another frame of this type
    /// </summary>
    /// <param name="frame">frame to add</param>
    public void Add(UnsynchronisedLyricsFrame frame)
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
