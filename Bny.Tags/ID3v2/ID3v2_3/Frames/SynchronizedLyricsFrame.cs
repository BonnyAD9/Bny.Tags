namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Synchronized lyrics/text (ID3v2.3)
/// </summary>
public class SynchronizedLyricsFrame : Frame
{
    /// <summary>
    /// Language
    /// </summary>
    public string Language { get; set; }
    /// <summary>
    /// Time stamp format
    /// </summary>
    public TimeStampFormat TimeStampFormat { get; set; }
    /// <summary>
    /// Content type
    /// </summary>
    public ContentType ContentType { get; set; }
    /// <summary>
    /// Constent descriptor
    /// </summary>
    public string ContentDescriptor { get; set; }
    /// <summary>
    /// Synchronized text (text with time stamps)
    /// </summary>
    public List<TextSync> TextSyncs { get; set; }

    public SynchronizedLyricsFrame? Next { get; private set; } = null;

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public SynchronizedLyricsFrame() : base()
    {
        Language = "lng";
        TimeStampFormat = TimeStampFormat.MPEGFrames;
        ContentType = ContentType.Other;
        ContentDescriptor = "";
        TextSyncs = new();
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal SynchronizedLyricsFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        var enc = (Encoding)data[0];
        Language = data[1..4].ToISO_8859_1();
        TimeStampFormat = (TimeStampFormat)data[4];
        ContentType = (ContentType)data[5];
        int pos = 6;
        ContentDescriptor = data[6..].ToID3v2_3String(enc, ref pos);
        TextSyncs = new();
        for (int p; pos < data.Length; pos += p)
            TextSyncs.Add(new TextSync(data[pos..], enc, out p));
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {ContentDescriptor}",
            "A" => $"{ID.String()}: (Synchronized Lyrics)\n" +
                   $"  Language: {Language}\n" +
                   $"  Time Stamp Format: {TimeStampFormat}\n" +
                   $"  Content Type: {ContentType}\n" +
                   $"  Content Descriptor: {ContentDescriptor}\n" +
                   $"  Text Syncs: {string.Join(", ", TextSyncs.Select(p => p.ToString()))}",
            _ => throw new FormatException()
        };
    }

    public override bool TryAdd(Frame frame)
    {
        if (frame is SynchronizedLyricsFrame sylt)
        {
            Add(sylt);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Adds another frame of this type
    /// </summary>
    /// <param name="frame">frame to add</param>
    public void Add(SynchronizedLyricsFrame frame)
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
/// Describes the type of content
/// </summary>
public enum ContentType : byte
{
    Other = 0x00,
    Lyrics = 0x01,
    TextTranscription = 0x02,
    /// <summary>
    /// Movement/part name (e.g. "Adagio")
    /// </summary>
    PartName = 0x03,
    /// <summary>
    /// Events (e.g. "Don Quijote enteres the stage")
    /// </summary>
    Events = 0x04,
    /// <summary>
    /// Chord (e.g. "Bb F Fsus")
    /// </summary>
    Chord = 0x05,
    /// <summary>
    /// Trivia/'pop up' information
    /// </summary>
    Trivia = 0x06,
}
