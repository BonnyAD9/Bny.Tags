namespace Bny.Tags.ID3v2Tags;
internal static class ID3v2Helpers
{
    public static byte[] UTF_8Null { get; } = new byte[] { 0, 0 }; 
    public static string ToID3v2_3VariableEncoding(this ReadOnlySpan<byte> span) => (ID3v2_3Encoding)span[0] == ID3v2_3Encoding.ISO_8859_1 ? span[1..].ToISO_8859_1() : span[1..].ToUTF_16BOM();
    public static string ToID3v2_3String(this ReadOnlySpan<byte> span) => span[1..].ToID3v2_3String((ID3v2_3Encoding)span[0], out int pos);
    public static string ToID3v2_3String(this ReadOnlySpan<byte> span, ID3v2_3Encoding encoding) => span.ToID3v2_3String(encoding, out int pos);
    public static string ToID3v2_3String(this ReadOnlySpan<byte> span, ID3v2_3Encoding encoding, out int pos)
    {
        if (encoding == ID3v2_3Encoding.ISO_8859_1)
        {
            pos = span.IndexOf((byte)0);
            if (pos == -1)
                return span.ToISO_8859_1();
            return span[..pos].ToISO_8859_1();
        }

        pos = span.IndexOf(UTF_8Null);
        if (pos == -1)
            return span.ToUTF_16BOM();
        return span[..pos].ToUTF_16BOM();
    }
}
