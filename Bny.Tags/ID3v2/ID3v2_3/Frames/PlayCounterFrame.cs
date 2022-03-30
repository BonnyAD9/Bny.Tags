using System.Numerics;

namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Play counter (ID3v2.3)
/// </summary>
public class PlayCounterFrame : Frame
{
    /// <summary>
    /// Counter
    /// </summary>
    public BigInteger Counter { get; set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public PlayCounterFrame() : base()
    {
        Counter = BigInteger.Zero;
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
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
