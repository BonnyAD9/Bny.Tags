using System.Numerics;

namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class PlayCounterFrame : Frame
{
    public BigInteger Counter { get; set; }

    public PlayCounterFrame() : base()
    {
        Counter = BigInteger.Zero;
    }

    internal PlayCounterFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        Counter = new(data, true, true);
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {Counter}",
            "A" => $"{ID.String()}: (Play Counter)\n" +
                   $"  Counter: {Counter}",
            _ => throw new FormatException()
        };
    }
}
