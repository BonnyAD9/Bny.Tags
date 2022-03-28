namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class UnknownFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public byte[] Data { get; set; }

    public UnknownFrame()
    {
        Header = default;
        Data = Array.Empty<byte>();
    }

    internal UnknownFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
        Data = data.ToArray();
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
            "C" => $"{ID}: {Data.Length} B",
            "A" => $"{ID} (Unknown):\n" +
                   $"  Data: {Data.Length} B",
            _ => throw new FormatException()
        };
    }
}