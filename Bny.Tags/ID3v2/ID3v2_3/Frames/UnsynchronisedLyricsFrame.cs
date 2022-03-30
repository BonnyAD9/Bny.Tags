namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class UnsynchronisedLyricsFrame : Frame
{
    public string Language { get; set; }
    public string ContentDescriptor { get; set; }
    public string Lyrics { get; set; }

    public UnsynchronisedLyricsFrame() : base()
    {
        Language = "lng";
        ContentDescriptor = "";
        Lyrics = "";
    }

    internal UnsynchronisedLyricsFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        var enc = (Encoding)data[0];
        Language = data[1..4].ToISO_8859_1();
        int pos = 4;
        ContentDescriptor = data[4..].ToID3v2_3String(enc, ref pos);
        Lyrics = data[pos..].ToID3v2_3String(enc);
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {ContentDescriptor}",
            "A" => $"{ID.String()}: (Unsynchronized Lyrics)\n" +
                   $"  Language: {Language}\n" +
                   $"  Content Descriptor: {ContentDescriptor}\n" +
                   $"  Lyrics: {Lyrics}",
            _ => throw new FormatException()
        };
    }
}
