namespace Bny.Tags.ID3v2Tags;

public struct ID3v2_3Comment : IComment
{
    internal ID3v2_3Encoding Encoding { get; set; } = ID3v2_3Encoding.ISO_8859_1;
    private string language = "eng"; // Default language assumes english
    public string Language
    {
        get => language;
        set
        {
            if (value.Length == 3)
                language = value;
            if (value.Length > 3)
                language = value[..3];
            language = value.PadRight(3);
        }
    }
    public string ContentDescription { get; set; } = "";
    public string Text { get; set; } = "";

    internal static ID3v2_3Comment FromBytes(ReadOnlySpan<byte> data)
    {
        ID3v2_3Comment comment = new();
        
        comment.Encoding = (ID3v2_3Encoding)data[0];
        comment.Language = data[1..4].ToAscii();
        comment.ContentDescription = data[4..].ToID3v2_3String(comment.Encoding, out int pos);
        
        if (pos == -1)
            return comment;

        pos += comment.Encoding == ID3v2_3Encoding.ISO_8859_1 ? 5 : 6;
        comment.Text = data[pos..].ToID3v2_3String(comment.Encoding);

        return comment;
    }

    public override string ToString() => Text;
}

internal enum ID3v2_3Encoding : byte
{
    ISO_8859_1 = 0x00,
    UTF_16 = 0x01,
}
