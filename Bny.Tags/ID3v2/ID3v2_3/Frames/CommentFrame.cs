namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Comments (ID3v2.3)
/// Full text information that doesn't fit in any other frame
/// </summary>
public class CommentFrame : Frame
{
    /// <summary>
    /// Language
    /// </summary>
    public string Language { get; set; }
    /// <summary>
    /// Short content description
    /// </summary>
    public string ContentDescription { get; set; }
    /// <summary>
    /// The actual text
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Next frame of this type, otherwise null
    /// </summary>
    public CommentFrame? Next { get; private set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public CommentFrame() : base()
    {
        Language = "lng";
        ContentDescription = "";
        Text = "";
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal CommentFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        var enc = (Encoding)data[0];
        Language = data[1..4].ToISO_8859_1();
        int pos = 4;
        ContentDescription = data[4..].ToID3v2_3String(enc, ref pos);
        Text = data[pos..].ToID3v2_3String(enc);
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {ContentDescription}",
            "A" => $"{ID.String()} (Comment):\n" + 
                   $"  Language: {Language}\n" +
                   $"  Description: {ContentDescription}\n" +
                   $"  Text: {Text}",
            _ => throw new FormatException()
        };
    }

    public override bool TryAdd(Frame frame)
    {
        if (frame is CommentFrame comm)
        {
            Add(comm);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Adds another frame of this type
    /// </summary>
    /// <param name="frame">frame to add</param>
    public void Add(CommentFrame frame)
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
