namespace Bny.Tags;

public static class ID3v1
{
    public const int size = 128;
    public const string id = "TAG";

    public static bool Read(IID3v1Tag tag, string file)
    {
        if (!File.Exists(file))
            return false;

        using FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read);
        return Read(tag, fs);
    }

    private static bool Read(IID3v1Tag tag, Stream stream)
    {
        stream.Seek(-size, SeekOrigin.End);
        byte[] buffer = new byte[size];
        
        if (stream.Read(buffer) != size)
            return false;
        
        ReadOnlySpan<byte> spanBuf = buffer.AsSpan();
        
        if (spanBuf.FromAscii(..3) != id)
            return false;
        
        FromBytes(tag, buffer.AsSpan());
        
        return true;
    }

    private static void FromBytes(IID3v1Tag tag, ReadOnlySpan<byte> data)
    {
        tag.Title = data.FromAsciiTrimmed(3..33);
        tag.Artist = data.FromAsciiTrimmed(33..63);
        tag.Album = data.FromAsciiTrimmed(63..93);
        tag.Year = data.FromAsciiTrimmed(93..97);

        if (data[125] == 0 && data[126] != 0)
        {
            tag.Comment = data.FromAsciiTrimmed(97..125);
            tag.TrackNumber = data[126];
        }
        else
        {
            tag.Comment = data.FromAsciiTrimmed(97..127);
        }

        tag.GenreEnum = (ID3v1Genre)data[127];
    }
}
