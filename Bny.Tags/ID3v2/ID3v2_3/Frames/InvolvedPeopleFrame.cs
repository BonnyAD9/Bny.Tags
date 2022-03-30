namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class InvolvedPeopleFrame : Frame
{
    public List<InvolvedPerson> People { get; set; }

    public InvolvedPeopleFrame() : base()
    {
        People = new();
    }

    internal InvolvedPeopleFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        People = new();
        var enc = (Encoding)data[0];
        int p;
        for (int pos = 1; pos < data.Length; pos += p)
        {
            People.Add(new(data[pos..], enc, out p));
        }
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {People.Count} People",
            "A" => $"{ID.String()}: (Involved People)\n" +
                   $"  People: {string.Join(", ", People.Select(p => p.ToString()))}",
            _ => throw new FormatException()
        };
    }
}
