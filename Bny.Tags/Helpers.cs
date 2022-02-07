using System.Text;

namespace Bny.Tags;
internal static class Helpers
{
    public static string FromAscii(this ReadOnlySpan<byte> span, Range range) => Encoding.ASCII.GetString(span[range]);
    public static string FromAsciiTrimmed(this ReadOnlySpan<byte> span, Range range) => Encoding.ASCII.GetString(span[range].Trim(new ReadOnlySpan<byte>(new byte[] { 0, 32 }))); // trim '\0' and ' '
}
