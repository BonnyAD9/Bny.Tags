namespace Bny.Tags.ID3v2Tags;
public static class ID3v2
{
    public const string id = "ID3";

    public static ID3v2Error Read(ITag tag, string file)
    {
        if (!File.Exists(file))
            return ID3v2Error.FileNotFound;

        using FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read);
        return Read(tag, fs);
    }

    private static ID3v2Error Read(ITag tag, Stream stream)
    {
        byte[] headerBuffer = new byte[ID3v2Header.size];
        if (stream.Read(headerBuffer) != ID3v2Header.size)
            return ID3v2Error.UnexpecterEnd;

        ReadOnlySpan<byte> headerBufSpan = headerBuffer.AsSpan();

        if (headerBufSpan[..3].ToAscii() != id)
            return ID3v2Error.NotID3;

        ID3v2Header header = ID3v2Header.FromBytes(headerBufSpan);

        return header.Version switch
        {
            ID3v2Version.ID3v2_2 => ID3v2Error.Unsupported,
            ID3v2Version.ID3v2_3 => ID3v2_3.Read(tag, stream, header),
            ID3v2Version.ID3v2_4 => ID3v2Error.Unsupported,
            _ => ID3v2Error.InvalidData,
        };
    }
}

public enum ID3v2Error
{
    None = 0,
    Unsupported = 1,
    InvalidData = 2,
    UnexpecterEnd = 3,
    FileNotFound = 4,
    NotID3 = 5,
    InvalidSize = 6,
}
