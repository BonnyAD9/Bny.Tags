namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// General encapsulated object (ID3v2.3)
/// Allows to encapsulate any type of file
/// </summary>
public class GeneralObjectFrame : Frame
{
    /// <summary>
    /// MIME type
    /// </summary>
    public string MIMEType { get; set; }
    /// <summary>
    /// Filename
    /// </summary>
    public string Filename { get; set; }
    /// <summary>
    /// Content description
    /// </summary>
    public string ContentDescription { get; set; }
    /// <summary>
    /// Encapsulated object, binary data
    /// </summary>
    public byte[] Data { get; set; }

    /// <summary>
    /// Next frame of this type, otherwise null
    /// </summary>
    public GeneralObjectFrame? Next { get; private set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public GeneralObjectFrame() : base()
    {
        MIMEType = "";
        Filename = "";
        ContentDescription = "";
        Data = Array.Empty<byte>();
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal GeneralObjectFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        var enc = (Encoding)data[0];
        int pos = 1;
        MIMEType = data[1..].ToID3v2_3String(Encoding.ISO_8859_1, ref pos);
        Filename = data[pos..].ToID3v2_3String(enc, ref pos);
        ContentDescription = data[pos..].ToID3v2_3String(enc, ref pos);
        Data = data[pos..].ToArray();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {ContentDescription}",
            "A" => $"{ID.String()}: (General Object)\n" +
                   $"  MIME Type: {MIMEType}\n" +
                   $"  File Name: {Filename}\n" +
                   $"  Content Description: {ContentDescription}\n" +
                   $"  Data: {Data.Length} B",
            _ => throw new FormatException()
        };
    }

    public override bool TryAdd(Frame frame)
    {
        if (frame is GeneralObjectFrame geob)
        {
            Add(geob);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Adds another frame of this type
    /// </summary>
    /// <param name="frame">frame to add</param>
    public void Add(GeneralObjectFrame frame)
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
