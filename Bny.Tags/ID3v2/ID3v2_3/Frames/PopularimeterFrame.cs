using System.Numerics;

namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class PopularimeterFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public string EmailToUser { get; set; }
    public byte Rating { get; set; }
    public BigInteger Counter { get; set; }

    public PopularimeterFrame()
    {
        Header = default;
        EmailToUser = "";
        Rating = 0;
        Counter = BigInteger.Zero;
    }

    internal PopularimeterFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
        int pos = 0;
        EmailToUser = data.ToID3v2_3String(Encoding.ISO_8859_1, ref pos);
        Rating = data[pos];
        Counter = new(data[pos..], true, true);
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
            "C" => $"{ID}: {EmailToUser}",
            "A" => $"{ID}: (Popularimeter)\n" +
                   $"  Email To User: {EmailToUser}\n" +
                   $"  Rating: {Rating}\n" +
                   $"  Counter: {Counter}",
            _ => throw new FormatException()
        };
    }
}
