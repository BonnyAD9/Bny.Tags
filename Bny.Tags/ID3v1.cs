namespace Bny.Tags;

public static class ID3v1
{
    public const int size = 128;
    public const string id = "TAG";

    public static bool Read(ITag tag, string file)
    {
        if (!File.Exists(file))
            return false;

        using FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read);
        return Read(tag, fs);
    }

    private static bool Read(ITag tag, Stream stream)
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

    private static void FromBytes(ITag tag, ReadOnlySpan<byte> data)
    {
        tag.SetTag(data.FromAsciiTrimmed(3..33), "Title");
        tag.SetTag(data.FromAsciiTrimmed(33..63), "Artist");
        tag.SetTag(data.FromAsciiTrimmed(63..93), "Album");
        tag.SetTag(data.FromAsciiTrimmed(93..97), "Year");

        if (data[125] == 0 && data[126] != 0)
        {
            tag.SetTag(data.FromAsciiTrimmed(97..125), "Comment");
            tag.SetTag(data[126], "Track");
        }
        else
        {
            tag.SetTag(data.FromAsciiTrimmed(97..127), "Comment");
        }

        tag.SetTag((ID3v1Genre)data[127], "Genre");
    }
}
