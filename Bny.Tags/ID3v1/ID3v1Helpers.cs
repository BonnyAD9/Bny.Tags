using System.Text;

namespace Bny.Tags.ID3v1;

/// <summary>
/// Useful functions specific to ID3v1
/// </summary>
internal static class ID3v1Helpers
{
    /// <summary>
    /// Converts given span to string using ascii encoding and trims all spaces and NULL characters
    /// </summary>
    /// <param name="span">Span to convert</param>
    /// <returns>Converted string</returns>
    public static string ToAsciiTrimmed(this ReadOnlySpan<byte> span) => Encoding.ASCII.GetString(span.Trim(new ReadOnlySpan<byte>(new byte[] { 0, 32 }))); // trim '\0' and ' '
}
