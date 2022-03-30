using System.Numerics;

namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Popularimeter (ID3v2.3)
/// Specifies how good an audio file is
/// </summary>
public class PopularimeterFrame : Frame
{
    /// <summary>
    /// Email to user
    /// </summary>
    public string EmailToUser { get; set; }
    /// <summary>
    /// Rating of the file
    /// 1 worst
    /// 255 best
    /// 0 unknown
    /// </summary>
    public byte Rating { get; set; }
    /// <summary>
    /// Play counter
    /// </summary>
    public BigInteger Counter { get; set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public PopularimeterFrame() : base()
    {
        EmailToUser = "";
        Rating = 0;
        Counter = BigInteger.Zero;
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal PopularimeterFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        int pos = 0;
        EmailToUser = data.ToID3v2_3String(Encoding.ISO_8859_1, ref pos);
        Rating = data[pos];
        Counter = new(data[pos..], true, true);
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {EmailToUser}",
            "A" => $"{ID.String()}: (Popularimeter)\n" +
                   $"  Email To User: {EmailToUser}\n" +
                   $"  Rating: {Rating}\n" +
                   $"  Counter: {Counter}",
            _ => throw new FormatException()
        };
    }
}
