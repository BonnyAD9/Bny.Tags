namespace Bny.Tags.ID3v1;

public class ID3v1Tag
{
    public const int size = 128;
    public const string id = "TAG";

    public string Title { get; set; } = "";
    public string Artist { get; set; } = "";
    public string Album { get; set; } = "";
    public string Year { get; set; } = "";
    public string Comment { get; set; } = "";
    public byte Track { get; set; } = 0;
    public ID3v1Genre Genre { get; set; } = ID3v1Genre.Unset;

    public TagError Read(string file)
    {
        if (!File.Exists(file))
            return TagError.FileNotFound;

        using FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read);
        return ReadStream(fs);
    }

    public TagError Read(Stream stream, bool shouldSeek = true)
    {
        if (!stream.CanRead)
            return TagError.CannotRead;

        if (shouldSeek)
        {
            if (!stream.CanSeek)
                return TagError.CannotSeak;
            return ReadStream(stream);
        }

        byte[] buffer = new byte[size];

        if (stream.Read(buffer) != size)
            return TagError.UnexpectedEnd;

        ReadOnlySpan<byte> spanBuf = buffer.AsSpan();

        if (spanBuf[..3].ToAscii() != id)
            return TagError.NoTag;

        ReadBytes(buffer.AsSpan());

        return TagError.None;
    }

    public TagError Read(ReadOnlySpan<byte> data)
    {
        if (data.Length != size)
            return TagError.InvalidSize;
        if (data[..3].ToAscii() != id)
            return TagError.NoTag;
        ReadBytes(data);
        return TagError.None;
    }

    private TagError ReadStream(Stream stream)
    {
        stream.Seek(-size, SeekOrigin.End);
        byte[] buffer = new byte[size];
        
        if (stream.Read(buffer) != size)
            return TagError.InvalidSize;
        
        ReadOnlySpan<byte> spanBuf = buffer.AsSpan();
        
        if (spanBuf[..3].ToAscii() != id)
            return TagError.NoTag;
        
        ReadBytes(buffer.AsSpan());
        
        return TagError.None;
    }

    private void ReadBytes(ReadOnlySpan<byte> data)
    {
        Title = data[3..33].ToAsciiTrimmed();
        Artist = data[33..63].ToAsciiTrimmed();
        Album = data[63..93].ToAsciiTrimmed();
        Year = data[93..97].ToAsciiTrimmed();

        if (data[125] == 0 && data[126] != 0)
        {
            Comment = data[97..125].ToAsciiTrimmed();
            Track = data[126];
        }
        else
            Comment = data[97..127].ToAsciiTrimmed();

        Genre = (ID3v1Genre)data[127];
    }
}
