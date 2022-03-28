using System.Numerics;

namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class RelativeVolumeAdjustmentFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public IncrementDecrement IncrementDecrement { get; set; }
    public byte BitsPerVolDesc { get; set; }
    public BigInteger RelativeRight { get; set; }
    public BigInteger RelativeLeft { get; set; }
    public BigInteger PeakRight { get; set; }
    public BigInteger PeakLeft { get; set; }
    public BigInteger RelativeRightBack { get; set; }
    public BigInteger RelativeLeftBack { get; set; }
    public BigInteger PeakRightBack { get; set; }
    public BigInteger PeakLeftBack { get; set; }
    public BigInteger RelativeCenter { get; set; }
    public BigInteger PeakCenter { get; set; }
    public BigInteger RelativeBass { get; set; }
    public BigInteger PeakBass { get; set; }

    public RelativeVolumeAdjustmentFrame()
    {
        Header = default;
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

    internal RelativeVolumeAdjustmentFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
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

    public override string ToString()
    {
        return ToString("G");
    }

    public string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.ToString(),
            "C" => $"{ID}: {IncrementDecrement}",
            "A" => $"{ID}: (Relative Volume Adjustment)\n" +
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
