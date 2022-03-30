using System.Numerics;

namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Relative volume adjustmet (ID3v2.3)
/// Allows to adjust how much to increase/decrease the volume on each channel
/// </summary>
public class RelativeVolumeAdjustmentFrame : Frame
{
    /// <summary>
    /// Increment/decrement
    /// </summary>
    public IncrementDecrement IncrementDecrement { get; set; }
    /// <summary>
    /// Bits used for volume description
    /// 0x10 (16) is recommended
    /// </summary>
    public byte BitsPerVolDesc { get; set; }
    /// <summary>
    /// Relative volume change, right
    /// </summary>
    public BigInteger RelativeRight { get; set; }
    /// <summary>
    /// Relative volume change, left
    /// </summary>
    public BigInteger RelativeLeft { get; set; }
    /// <summary>
    /// Peak folume right
    /// </summary>
    public BigInteger PeakRight { get; set; }
    /// <summary>
    /// Peak volume left
    /// </summary>
    public BigInteger PeakLeft { get; set; }
    /// <summary>
    /// Relative volume change, right back
    /// </summary>
    public BigInteger RelativeRightBack { get; set; }
    /// <summary>
    /// Relative volume change, left back
    /// </summary>
    public BigInteger RelativeLeftBack { get; set; }
    /// <summary>
    /// Peak volume right back
    /// </summary>
    public BigInteger PeakRightBack { get; set; }
    /// <summary>
    /// Peak volume left back
    /// </summary>
    public BigInteger PeakLeftBack { get; set; }
    /// <summary>
    /// Relative volume change, center
    /// </summary>
    public BigInteger RelativeCenter { get; set; }
    /// <summary>
    /// Peak volume center
    /// </summary>
    public BigInteger PeakCenter { get; set; }
    /// <summary>
    /// Relative volume change, bass
    /// </summary>
    public BigInteger RelativeBass { get; set; }
    /// <summary>
    /// Peak volume bass
    /// </summary>
    public BigInteger PeakBass { get; set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public RelativeVolumeAdjustmentFrame() : base()
    {
        IncrementDecrement = IncrementDecrement.None;
        BitsPerVolDesc = 16;
        RelativeRight = BigInteger.Zero;
        RelativeLeft = BigInteger.Zero;
        PeakRight = BigInteger.Zero;
        PeakLeft = BigInteger.Zero;
        RelativeRightBack = BigInteger.Zero;
        RelativeLeftBack = BigInteger.Zero;
        PeakRightBack = BigInteger.Zero;
        PeakLeftBack = BigInteger.Zero;
        RelativeCenter = BigInteger.Zero;
        PeakCenter = BigInteger.Zero;
        RelativeBass = BigInteger.Zero;
        PeakBass = BigInteger.Zero;
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal RelativeVolumeAdjustmentFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        IncrementDecrement = (IncrementDecrement)data[0];
        BitsPerVolDesc = data[1];
        int bpvd = BitsPerVolDesc > (255 - 7) ? 32 : (BitsPerVolDesc + 7) / 8;
        int pos = 2;

        RelativeRight = new(data[pos..(pos + bpvd)], true, true);
        pos += bpvd;
        RelativeLeft = new(data[pos..(pos + bpvd)], true, true);
        pos += bpvd;
        PeakRight = new(data[pos..(pos + bpvd)], true, true);
        pos += bpvd;
        PeakLeft = new(data[pos..(pos + bpvd)], true, true);
        pos += bpvd;

        if (data.Length <= pos)
            return;

        RelativeRightBack = new(data[pos..(pos + bpvd)], true, true);
        pos += bpvd;
        RelativeLeftBack = new(data[pos..(pos + bpvd)], true, true);
        pos += bpvd;
        PeakRightBack = new(data[pos..(pos + bpvd)], true, true);
        pos += bpvd;
        PeakLeftBack = new(data[pos..(pos + bpvd)], true, true);
        pos += bpvd;

        if (data.Length <= pos)
            return;

        RelativeCenter = new(data[pos..(pos + bpvd)], true, true);
        pos += bpvd;
        PeakCenter = new(data[pos..(pos + bpvd)], true, true);
        pos += bpvd;

        if (data.Length <= pos)
            return;

        RelativeBass = new(data[pos..(pos + bpvd)], true, true);
        pos += bpvd;
        PeakBass = new(data[pos..], true, true);
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {IncrementDecrement}",
            "A" => $"{ID.String()}: (Relative Volume Adjustment)\n" +
                   $"  Increment: {IncrementDecrement}\n" +
                   $"  Bits Per Volume Description: {BitsPerVolDesc}\n" +
                   $"  Relative Right: {RelativeRight}\n" +
                   $"  Relative Left: {RelativeLeft}\n" +
                   $"  Peak Right: {PeakRight}\n" +
                   $"  Peak Left: {PeakLeft}\n" +
                   $"  Relative Right Back: {RelativeRightBack}\n" +
                   $"  Relative Left Back: {RelativeLeftBack}\n" +
                   $"  Peak Right Back: {PeakRightBack}\n" +
                   $"  Peak Left Back: {PeakLeftBack}\n" +
                   $"  Relative Center: {RelativeCenter}\n" +
                   $"  Peak Center: {PeakCenter}\n" +
                   $"  Relative Bass: {RelativeBass}\n" +
                   $"  Peak Bass: {PeakBass}",
            _ => throw new FormatException()
        };
    }
}

/// <summary>
/// Indicates whether the relative volume change is decrease or increase at the given channel (ID3v2.3)
/// </summary>
[Flags]
public enum IncrementDecrement : byte
{
    None = 0,
    Right     = 0b_0000_0001,
    Left      = 0b_0000_0010,
    RightBack = 0b_0000_0100,
    LeftBack  = 0b_0000_1000,
    Center    = 0b_0001_0000,
    Bass      = 0b_0010_0000,
}
