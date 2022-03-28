using System.Numerics;

namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class PlayCounterFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public BigInteger Counter { get; set; }

    public PlayCounterFrame()
    {
        Header = default;
        Counter = BigInteger.Zero;
    }

    internal PlayCounterFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
        Counter = new(data, true, true);
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
            "C" => $"{ID}: {Counter}",
            "A" => $"{ID}: (Play Counter)\n" +
                   $"  Counter: {Counter}",
            _ => throw new FormatException()
        };
    }
}
