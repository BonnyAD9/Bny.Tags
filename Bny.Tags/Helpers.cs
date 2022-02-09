﻿using System.Text;

namespace Bny.Tags;
internal static class Helpers
{
    public static string ToAscii(this ReadOnlySpan<byte> span) => Encoding.ASCII.GetString(span);
    public static string ToAsciiTrimmed(this ReadOnlySpan<byte> span) => Encoding.ASCII.GetString(span.Trim(new ReadOnlySpan<byte>(new byte[] { 0, 32 }))); // trim '\0' and ' '

    public static string ToISO_8859_1(this ReadOnlySpan<byte> span) => Encoding.GetEncoding("ISO-8859-1").GetString(span);

    public static string ToUTF_16(this ReadOnlySpan<byte> span, bool bigEndian)
    {
        if (bigEndian)
            return Encoding.BigEndianUnicode.GetString(span);
        return Encoding.Unicode.GetString(span);
    }

    public static string ToUTF_16BOM(this ReadOnlySpan<byte> span)
    {
        if (span.Length == 0 || span[0] == 0)
            return "";
        return span[2..].ToUTF_16(span[0] == 0xFE && span[1] == 0xFF);
    }

    // Always converts in big-endian byte order
    public static ushort ToUInt16(this ReadOnlySpan<byte> span) => span.Length < 2 ? throw new ArgumentException("Given span is too short") : (ushort)(span[0] << 8 | span[1]);
    public static uint ToUInt32(this ReadOnlySpan<byte> span) =>
        span.Length < 4 ? throw new ArgumentException("Given span is too short") : (uint)(span[0] << 24 | span[1] << 16 | span[2] << 8 | span[3]);
}
