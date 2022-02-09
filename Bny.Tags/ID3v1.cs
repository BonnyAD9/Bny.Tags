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
        
        if (spanBuf[..3].ToAscii() != id)
            return false;
        
        FromBytes(tag, buffer.AsSpan());
        
        return true;
    }

    private static void FromBytes(ITag tag, ReadOnlySpan<byte> data)
    {
        tag.SetTag(data[3..33].ToAsciiTrimmed(), "Title");
        tag.SetTag(data[33..63].ToAsciiTrimmed(), "Artist");
        tag.SetTag(data[63..93].ToAsciiTrimmed(), "Album");
        tag.SetTag(data[93..97].ToAsciiTrimmed(), "Year");

        if (data[125] == 0 && data[126] != 0)
        {
            tag.SetTag(data[97..125].ToAsciiTrimmed(), "Comment");
            tag.SetTag(data[126], "Track");
        }
        else
        {
            tag.SetTag(data[97..127].ToAsciiTrimmed(), "Comment");
        }

        tag.SetTag((ID3v1Genre)data[127], "Genre");
    }
}
