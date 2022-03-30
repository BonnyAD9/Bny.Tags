namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Involved people list (ID3v2.3)
/// </summary>
public class InvolvedPeopleFrame : Frame
{
    /// <summary>
    /// List of involved people and their involvement
    /// </summary>
    public List<InvolvedPerson> People { get; set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public InvolvedPeopleFrame() : base()
    {
        People = new();
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
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
