namespace Bny.Tags.ID3v2.ID3v2_3;

/// <summary>
/// Class containing useful functions for working with ID3v2.3
/// </summary>
internal static class ID3v2_3Helpers
{
    /// <summary>
    /// Two zero bytes
    /// </summary>
    public static byte[] UTF_16Null { get; } = new byte[] { 0, 0 };

    /// <summary>
    /// Converts span to string using variable encoding based on the first byte. Reads until the ind of the span.
    /// 
    /// 0x00 => ISO-8859-1
    /// 0x01 => UTF-16 (with BOM)
    /// _    => ISO-8859-1 (including the first byte)
    /// </summary>
    /// <param name="span">Span to convert</param>
    /// <returns>Converted string</returns>
    public static string ToID3v2_3VariableEncoding(this ReadOnlySpan<byte> span) => (Encoding)span[0] switch
    {
        Encoding.ISO_8859_1 => span[1..].ToISO_8859_1(),
        Encoding.UTF_16 => span[1..].ToUTF_16BOM(),
        _ => span.ToISO_8859_1()
    };

    /// <summary>
    /// Converts span to string using encoding based on the first byte. Reads until the NULL char or end of span.
    /// </summary>
    /// <param name="span">Span to convert</param>
    /// <returns></returns>
    public static string ToID3v2_3String(this ReadOnlySpan<byte> span) => span[1..].ToID3v2_3String((Encoding)span[0]);

    /// <summary>
    /// Converts span to string using encoding based on the first byte. Reads until the NULL char or end of span and adds the bytes read to the given variable.
    /// </summary>
    /// <param name="span">Span to convert</param>
    /// <param name="pos">Variable to which the byte count will be added</param>
    /// <returns>Converted string</returns>
    public static string ToID3v2_3String(this ReadOnlySpan<byte> span, ref int pos) => span[1..].ToID3v2_3String((Encoding)span[0], ref pos);

    /// <summary>
    /// Converts span to string based on the given encoding. Reads until the NULL char or end of span.
    /// </summary>
    /// <param name="span">Span to convert</param>
    /// <param name="encoding">Encoding to use</param>
    /// <returns>Converted string</returns>
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

    /// <summary>
    /// Converts span to string based on the given encoding. Reads until the NULL char or end of span and adds the bytes read to the given variable.
    /// </summary>
    /// <param name="span">Span to convert</param>
    /// <param name="encoding">Encoding to use</param>
    /// <param name="pos">Variable to which the byte count will be added</param>
    /// <returns>Converted string</returns>
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

/// <summary>
/// Encoding available in ID3v2.3 tag
/// </summary>
internal enum Encoding : byte
{
    /// <summary>
    /// ISO-8859-1 encoding; 1 char == 1 byte
    /// </summary>
    ISO_8859_1 = 0x00,
    /// <summary>
    /// UTF-16 encoding; 2 chars == 1 byte
    /// </summary>
    UTF_16 = 0x01,
}