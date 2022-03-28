namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class UnsynchronisedLyricsFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public string Language { get; set; }
    public string ContentDescriptor { get; set; }
    public string Lyrics { get; set; }

    public UnsynchronisedLyricsFrame()
    {
        Header = default;
        Language = "lng";
        ContentDescriptor = "";
        Lyrics = "";
    }

    internal UnsynchronisedLyricsFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
        var enc = (Encoding)data[0];
        Language = data[1..4].ToISO_8859_1();
        int pos = 4;
        ContentDescriptor = data[4..].ToID3v2_3String(enc, ref pos);
        Lyrics = data[pos..].ToID3v2_3String(enc);
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
            "C" => $"{ID}: {ContentDescriptor}",
            "A" => $"{ID}: (Unsynchronized Lyrics)\n" +
                   $"  Language: {Language}\n" +
                   $"  Content Descriptor: {ContentDescriptor}\n" +
                   $"  Lyrics: {Lyrics}",
            _ => throw new FormatException()
        };
    }
}
