using System.Text;

namespace Bny.Tags;
internal static class Helpers
{
    /// <summary>
    /// Converts span to string using ascii encoding
    /// </summary>
    /// <param name="span">Span to convert</param>
    /// <returns>String with the converted span</returns>
    public static string ToAscii(this ReadOnlySpan<byte> span) => Encoding.ASCII.GetString(span);
    
    /// <summary>
    /// Converts span to string using ISO-8859-1 encoding
    /// </summary>
    /// <param name="span">Span to convert</param>
    /// <returns>Converted string</returns>
    public static string ToISO_8859_1(this ReadOnlySpan<byte> span) => Encoding.GetEncoding("ISO-8859-1").GetString(span);

    /// <summary>
    /// Converts given span to string using UTF-16 encoding
    /// </summary>
    /// <param name="span">Span to convert</param>
    /// <param name="bigEndian">Specifies whether the characters are stored in BigEndian or LittleEndian byte order</param>
    /// <returns>Converted string</returns>
    public static string ToUTF_16(this ReadOnlySpan<byte> span, bool bigEndian)
    {
        if (bigEndian)
            return Encoding.BigEndianUnicode.GetString(span);
        return Encoding.Unicode.GetString(span);
    }

    /// <summary>
    /// Converts span to string using UTF-16 encoding with BOM (Byte Order Mask) at the begining
    /// </summary>
    /// <param name="span">Span to convert</param>
    /// <returns>Converted string</returns>
    public static string ToUTF_16BOM(this ReadOnlySpan<byte> span)
    {
        if (span.Length == 0 || span[0] == 0)
            return "";
        return span[2..].ToUTF_16(span[0] == 0xFE && span[1] == 0xFF);
    }

    // Always converts in big-endian byte order
    /// <summary>
    /// Converts first 2 bytes of span to ushort in big-endian byte order
    /// </summary>
    /// <param name="span">Span to convert</param>
    /// <returns>Converted ushort</returns>
    /// <exception cref="ArgumentException">Thrown when the span is too short</exception>
    public static ushort ToUInt16(this ReadOnlySpan<byte> span) => span.Length < 2 ? throw new ArgumentException("Given span is too short") : (ushort)(span[0] << 8 | span[1]);

    /// <summary>
    /// Converts first 2 bytes of span to short in big-endian byte order
    /// </summary>
    /// <param name="span">Span to convert</param>
    /// <returns>Converted short</returns>
    /// <exception cref="ArgumentException">Thrown when the span is too short</exception>
    public static short ToInt16(this ReadOnlySpan<byte> span) => span.Length < 2 ? throw new ArgumentException("Given span is too short") : (short)(span[0] << 8 | span[1]);

    /// <summary>
    /// Converts first 3 bytes of span to uint in big-endian byte order
    /// </summary>
    /// <param name="span">Span to convert</param>
    /// <returns>Converted uint</returns>
    /// <exception cref="ArgumentException">Thrown when the given span is too short</exception>
    public static uint ToUInt24(this ReadOnlySpan<byte> span) =>
        span.Length < 3 ? throw new ArgumentException("Given span is too short") : (uint)(span[0] << 16 | span[1] << 8 | span[2]);

    /// <summary>
    /// Converts first 4 bytes of span to uint in big-endian byte order
    /// </summary>
    /// <param name="span">Span to convert</param>
    /// <returns>Converted uint</returns>
    /// <exception cref="ArgumentException">Thrown when the given span is too short</exception>
    public static uint ToUInt32(this ReadOnlySpan<byte> span) =>
        span.Length < 4 ? throw new ArgumentException("Given span is too short") : (uint)(span[0] << 24 | span[1] << 16 | span[2] << 8 | span[3]);

    /// <summary>
    /// Converts the int into array of bytes in big-endian byte order
    /// </summary>
    /// <param name="value">value to convert</param>
    /// <returns>new byte array of length 4</returns>
    public static byte[] ToBytes(this uint value) => unchecked(new byte[] { (byte)(value >> 24), (byte)(value >> 16), (byte)(value >> 8), (byte)value });
}
