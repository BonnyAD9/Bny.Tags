namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// MPEG location lookup table (ID3v2.3)
/// Allows increase of performance and accuracy of jumps within a MPEG file
/// </summary>
public class MPEGLocationLookupTableFrame : Frame
{
    /// <summary>
    /// MPEG frames between reference
    /// </summary>
    public ushort MPEGFramesBetweenReference { get; set; }
    /// <summary>
    /// Bytes between reference
    /// </summary>
    public uint BytesBetweenReference { get; set; }
    /// <summary>
    /// Milliseconds between reference
    /// </summary>
    public uint MillisecondsBetweenreference { get; set; }
    /// <summary>
    /// Bits for bytes deviation
    /// </summary>
    public byte BitsForBytesDeviation { get; set; }
    /// <summary>
    /// Bits for milliseconds deviation
    /// </summary>
    public byte BitsForMillisecondsDeviation { get; set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public MPEGLocationLookupTableFrame() : base()
    {
        MPEGFramesBetweenReference = 0;
        BytesBetweenReference = 0;
        MillisecondsBetweenreference = 0;
        BitsForBytesDeviation = 0;
        BitsForMillisecondsDeviation = 0;
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal MPEGLocationLookupTableFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        MPEGFramesBetweenReference = data.ToUInt16();
        BytesBetweenReference = data[2..].ToUInt24();
        MillisecondsBetweenreference = data[5..].ToUInt24();
        BitsForBytesDeviation = data[8];
        BitsForMillisecondsDeviation = data[9];
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {MPEGFramesBetweenReference} Frames / Reference",
            "A" => $"{ID.String()}: (MPEG Location Lookup table)\n" +
                   $"  MPEG Frames Between Reference: {MPEGFramesBetweenReference}\n" +
                   $"  Bytes Between Reference: {BytesBetweenReference}\n" +
                   $"  Milliseconds Between Reference: {MillisecondsBetweenreference}\n" +
                   $"  Bits For Bytes Deviation: {BitsForBytesDeviation}\n" +
                   $"  Bits For Milliseconds Deviation: {BitsForMillisecondsDeviation}",
            _ => throw new FormatException()
        };
    }
}
