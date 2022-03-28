namespace Bny.Tags.ID3v2.ID3v2_3;

internal static class ID3v2_3Helpers
{
    public static byte[] UTF_16Null { get; } = new byte[] { 0, 0 };
    public static string ToID3v2_3VariableEncoding(this ReadOnlySpan<byte> span) => (Encoding)span[0] switch
    {
        Encoding.ISO_8859_1 => span[1..].ToISO_8859_1(),
        Encoding.UTF_16 => span[1..].ToUTF_16BOM(),
        _ => span.ToISO_8859_1()
    };
    public static string ToID3v2_3String(this ReadOnlySpan<byte> span) => span[1..].ToID3v2_3String((Encoding)span[0]);
    public static string ToID3v2_3String(this ReadOnlySpan<byte> span, ref int pos) => span[1..].ToID3v2_3String((Encoding)span[0], ref pos);
    public static string ToID3v2_3String(this ReadOnlySpan<byte> span, Encoding encoding)
    {
        int pos;
        if (encoding == Encoding.ISO_8859_1)
        {
            pos = span.IndexOf((byte)0);
            if (pos == -1)
                return span.ToISO_8859_1();
            return span[..pos].ToISO_8859_1();
        }

        pos = span.IndexOf(UTF_16Null);
        if (pos == -1)
            return span.ToUTF_16BOM();
        return span[..pos].ToUTF_16BOM();
    }

    public static string ToID3v2_3String(this ReadOnlySpan<byte> span, Encoding encoding, ref int pos)
    {
        int p;
        if (encoding == Encoding.ISO_8859_1)
        {
            p = span.IndexOf((byte)0);
            if (p == -1)
                return span.ToISO_8859_1();
            pos += p + 1;
            return span[..p].ToISO_8859_1();
        }

        p = span.IndexOf(UTF_16Null);
        if (p == -1)
            return span.ToUTF_16BOM();
        pos += p + 2;
        return span[..p].ToUTF_16BOM();
    }
}
internal enum Encoding : byte
{
    ISO_8859_1 = 0x00,
    UTF_16 = 0x01,
}