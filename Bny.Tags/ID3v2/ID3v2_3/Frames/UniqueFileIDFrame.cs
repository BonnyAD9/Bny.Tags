namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Unique File Identifier (ID3v2.3)
/// Helps to identify file in a database that may contain more information
/// </summary>
public class UniqueFileIDFrame : Frame
{
    /// <summary>
    /// Email adress / link to email adress of the organization responsible for this specific database implementation
    /// </summary>
    public string OwnerID { get; set; }
    /// <summary>
    /// The identifier
    /// Up to 64 bytes of binary data
    /// </summary>
    public byte[] Identifier { get; set; }

    /// <summary>
    /// Next frame of this type, otherwise null
    /// </summary>
    public UniqueFileIDFrame? Next { get; private set; } = null;

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public UniqueFileIDFrame() : base()
    {
        OwnerID = "";
        Identifier = Array.Empty<byte>();
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal UniqueFileIDFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        int pos = 0;
        OwnerID = data.ToID3v2_3String(Encoding.ISO_8859_1, ref pos);
        Identifier = data[pos..].ToArray();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {OwnerID}",
            "A" => $"{ID.String()}: (Unique File ID)\n" +
                   $"  Owner ID: {OwnerID}\n" +
                   $"  Identifier: {BitConverter.ToString(Identifier)}",
            _ => throw new FormatException()
        };
    }

    public override bool TryAdd(Frame frame)
    {
        if (frame is UniqueFileIDFrame ufid)
        {
            Add(ufid);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Adds another frame of this type
    /// </summary>
    /// <param name="frame">frame to add</param>
    public void Add(UniqueFileIDFrame frame)
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
