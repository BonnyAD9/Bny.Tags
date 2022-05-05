namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Group identification registration
/// </summary>
public class GroupRegistrationFrame : Frame
{
    /// <summary>
    /// Email adress/link to email adress to organization responsible for the grouping
    /// </summary>
    public string OwnerID { get; set; }
    /// <summary>
    /// Group symbol
    /// </summary>
    public byte GroupSymbol { get; set; }
    /// <summary>
    /// Group data
    /// </summary>
    public byte[] Data { get; set; }

    /// <summary>
    /// Next frame of the same type, otherwise null
    /// </summary>
    public GroupRegistrationFrame? Next { get; private set; } = null;

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public GroupRegistrationFrame() : base()
    {
        OwnerID = "";
        GroupSymbol = 0;
        Data = Array.Empty<byte>();
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal GroupRegistrationFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        int pos = 0;
        OwnerID = data.ToID3v2_3String(Encoding.ISO_8859_1, ref pos);
        GroupSymbol = data[pos];
        pos++;
        Data = data[pos..].ToArray();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {OwnerID}",
            "A" => $"{ID.String()}: (Group Registration)\n" +
                   $"  Owner ID: {OwnerID}\n" +
                   $"  Group Symbol: {GroupSymbol}\n" +
                   $"  Data: {Data.Length} B",
            _ => throw new FormatException()
        };
    }

    public override bool TryAdd(Frame frame)
    {
        if (frame is GroupRegistrationFrame grid)
        {
            Add(grid);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Adds another frame of this type
    /// </summary>
    /// <param name="frame">frame to add</param>
    public void Add(GroupRegistrationFrame frame)
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
