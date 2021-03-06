namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// URL link frame (ID3v2.3)
/// </summary>
public class URLFrame : Frame
{
    /// <summary>
    /// URL
    /// </summary>
    public string URL { get; set; }

    /// <summary>
    /// Next frame of this type, otherwise null
    /// </summary>
    public URLFrame? Next { get; private set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public URLFrame() : base()
    {
        URL = "";
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal URLFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        URL = data.ToISO_8859_1();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {URL}",
            "A" => $"{ID.String()}: (URL)\n" +
                   $"  URL: {URL}",
            _ => throw new FormatException()
        };
    }

    public override bool TryAdd(Frame frame)
    {
        if (frame is URLFrame w)
        {
            Add(w);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Adds another frame of this type
    /// </summary>
    /// <param name="frame">frame to add</param>
    public bool Add(URLFrame frame)
    {
        if (frame.ID != ID || ID is not FrameID.WCOM or FrameID.WOAR)
            return false;

        if (frame.Next is null)
        {
            frame.Next = Next;
            Next = frame;
            return true;
        }

        frame.Next.AddNoCheck(frame);
        return true;
    }

    /// <summary>
    /// Adds frame without checking for the frame ID
    /// </summary>
    /// <param name="frame"></param>
    private void AddNoCheck(URLFrame frame)
    {
        if (frame.Next is null)
        {
            frame.Next = Next;
            Next = frame;
            return;
        }

        frame.Next.AddNoCheck(frame);
    }
}
