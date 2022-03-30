namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class GeneralObjectFrame : Frame
{
    public string MIMEType { get; set; }
    public string FileName { get; set; }
    public string ContentDescription { get; set; }
    public byte[] Data { get; set; }

    public GeneralObjectFrame() : base()
    {
        MIMEType = "";
        FileName = "";
        ContentDescription = "";
        Data = Array.Empty<byte>();
    }

    internal GeneralObjectFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        var enc = (Encoding)data[0];
        int pos = 1;
        MIMEType = data[1..].ToID3v2_3String(Encoding.ISO_8859_1, ref pos);
        FileName = data[pos..].ToID3v2_3String(enc, ref pos);
        ContentDescription = data[pos..].ToID3v2_3String(enc, ref pos);
        Data = data[pos..].ToArray();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {ContentDescription}",
            "A" => $"{ID.String()}: (General Object)\n" +
                   $"  MIME Type: {MIMEType}\n" +
                   $"  File Name: {FileName}\n" +
                   $"  Content Description: {ContentDescription}\n" +
                   $"  Data: {Data.Length} B",
            _ => throw new FormatException()
        };
    }
}
