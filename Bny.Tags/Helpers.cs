using System.Text;

namespace Bny.Tags;
internal static class Helpers
{
    public static string FromAscii(this ReadOnlySpan<byte> span, Range range) => Encoding.ASCII.GetString(span[range]);
    public static string FromAsciiTrimmed(this ReadOnlySpan<byte> span, Range range) => Encoding.ASCII.GetString(span[range].Trim(new ReadOnlySpan<byte>(new byte[] { 0, 32 }))); // trim '\0' and ' '

    public static string FromISO_8859_1(this ReadOnlySpan<byte> span, Range range) => Encoding.GetEncoding("ISO-8859-1").GetString(span[range]);
    public static string FromUTF_16(this ReadOnlySpan<byte> span, Range range, bool bigEndian)
    {
        if (bigEndian)
            return Encoding.BigEndianUnicode.GetString(span[range]);
        return Encoding.Unicode.GetString(span[range]);
    }

    public static string FromUTF_16BOM(this ReadOnlySpan<byte> span, Range range)
    {
        span = span[range];
        return span.FromUTF_16(2.., span[0] == 0xFE && span[1] == 0xFF);
    }

    public static string FromID3v2_3VariableEncoding(this ReadOnlySpan<byte> span, Range range)
    {
        span = span[range];
        if (span[0] == 0)
            return span.FromISO_8859_1(1..);
        return span.FromUTF_16BOM(1..);
    }

    // Always converts in BigEndian
    public static ushort ToUInt16(this ReadOnlySpan<byte> span, Range range)
    {
        span = span[range];
        if (span.Length < 2)
            return 0;
        return (ushort)(span[0] << 8 | span[1]);
    }

    public static uint ToUInt32(this ReadOnlySpan<byte> span, Range range)
    {
        span = span[range];
        if (span.Length < 4)
            return 0;
        return (uint)(span[0] << 24 | span[1] << 16 | span[2] << 8 | span[3]);
    }
}
