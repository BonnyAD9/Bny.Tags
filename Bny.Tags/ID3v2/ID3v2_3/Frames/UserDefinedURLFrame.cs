namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// User defined URL link frame (ID3v2.3)
/// </summary>
public class UserDefinedURLFrame : Frame
{
    /// <summary>
    /// Description of what the URL is
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// URL adress
    /// </summary>
    public string URL { get; set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public UserDefinedURLFrame() : base()
    {
        Description = "";
        URL = "";
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal UserDefinedURLFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        int pos = 0;
        Description = data.ToID3v2_3String(ref pos);
        URL = data[pos..].ToISO_8859_1();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {Description}",
            "A" => $"{ID.String()}: (User Defined URL)\n" +
                   $"  Description: {Description}\n" +
                   $"  URL: {URL}",
            _ => throw new FormatException()
        };
    }
}
