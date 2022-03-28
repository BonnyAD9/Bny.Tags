namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class SynchronizedLyricsFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public string Language { get; set; }
    public TimeStampFormat TimeStampFormat { get; set; }
    public ContentType ContentType { get; set; }
    public string ContentDescriptor { get; set; }
    public List<TextSync> TextSyncs { get; set; }

    public SynchronizedLyricsFrame()
    {
        Header = default;
        Language = "lng";
        TimeStampFormat = TimeStampFormat.MPEGFrames;
        ContentType = ContentType.Other;
        ContentDescriptor = "";
        TextSyncs = new();
    }

    internal SynchronizedLyricsFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
        var enc = (Encoding)data[0];
        Language = data[1..4].ToISO_8859_1();
        TimeStampFormat = (TimeStampFormat)data[4];
        ContentType = (ContentType)data[5];
        int pos = 6;
        ContentDescriptor = data[6..].ToID3v2_3String(enc, ref pos);
        TextSyncs = new();
        for (int p; pos < data.Length; pos += p)
            TextSyncs.Add(new TextSync(data[pos..], enc, out p));
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
            "A" => $"{ID}: (Synchronized Lyrics)\n" +
                   $"  Language: {Language}\n" +
                   $"  Time Stamp Format: {TimeStampFormat}\n" +
                   $"  Content Type: {ContentType}\n" +
                   $"  Content Descriptor: {ContentDescriptor}\n" +
                   $"  Text Syncs: {string.Join(", ", TextSyncs.Select(p => p.ToString()))}",
            _ => throw new FormatException()
        };
    }
}

public enum ContentType : byte
{
    Other = 0x00,
    Lyrics = 0x01,
    TextTranscription = 0x02,
    PartName = 0x03,
    Events = 0x04,
    Chord = 0x05,
    Trivia = 0x06,
}
