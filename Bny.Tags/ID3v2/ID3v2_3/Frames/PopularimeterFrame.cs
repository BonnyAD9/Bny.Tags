using System.Numerics;

namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class PopularimeterFrame : Frame
{
    public string EmailToUser { get; set; }
    public byte Rating { get; set; }
    public BigInteger Counter { get; set; }

    public PopularimeterFrame() : base()
    {
        EmailToUser = "";
        Rating = 0;
        Counter = BigInteger.Zero;
    }

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
