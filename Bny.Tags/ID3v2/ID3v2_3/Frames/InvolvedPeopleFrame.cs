namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class InvolvedPeopleFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public List<InvolvedPerson> People { get; set; }

    public InvolvedPeopleFrame()
    {
        Header = default;
        People = new();
    }

    internal InvolvedPeopleFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
        People = new();
        var enc = (Encoding)data[0];
        int p;
        for (int pos = 1; pos < data.Length; pos += p)
        {
            People.Add(new(data[pos..], enc, out p));
        }
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
            "C" => $"{ID}: {People.Count} People",
            "A" => $"{ID}: (Involved People)\n" +
                   $"  People: {string.Join(", ", People.Select(p => p.ToString()))}",
            _ => throw new FormatException()
        };
    }
}
