namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class UnknownFrame : Frame
{
    public byte[] Data { get; set; }

    public UnknownFrame() : base()
    {
        Data = Array.Empty<byte>();
    }

    internal UnknownFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        Data = data.ToArray();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {Data.Length} B",
            "A" => $"{ID.String()} (Unknown):\n" +
                   $"  Data: {Data.Length} B",
            _ => throw new FormatException()
        };
    }
}