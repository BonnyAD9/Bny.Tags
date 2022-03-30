namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// User defined text information frame (ID3v2.3)
/// </summary>
public class UserDefinedTextFrame : Frame
{
    /// <summary>
    /// Description of what the value is
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// Text value of the frame
    /// </summary>
    public string Value { get; set; }


    /// <summary>
    /// Creates empty frame
    /// </summary>
    public UserDefinedTextFrame() : base()
    {
        Description = "";
        Value = "";
    }


    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal UserDefinedTextFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        var enc = (Encoding)data[0];
        int pos = 1;
        Description = data[1..].ToID3v2_3String(enc, ref pos);
        Value = data[pos..].ToID3v2_3String(enc);
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {Description}",
            "A" => $"{ID.String()}: (User Defined Text)\n" +
                   $"  Description: {Description}\n" +
                   $"  Value: {Value}",
            _ => throw new FormatException()
        };
    }
}
