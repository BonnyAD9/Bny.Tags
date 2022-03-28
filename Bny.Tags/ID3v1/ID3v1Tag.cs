namespace Bny.Tags.ID3v1;

/// <summary>
/// Manages ID3v1/ID3v1.1 tag
/// </summary>
public class ID3v1Tag
{
    /// <summary>
    /// Size of the whole tag in bytes
    /// </summary>
    public const int size = 128;
    /// <summary>
    /// Value that indicates start of the tag
    /// </summary>
    public const string id = "TAG";

    /// <summary>
    /// Title of the track; Song name
    /// </summary>
    public string Title { get; set; } = "";
    /// <summary>
    /// Artist
    /// </summary>
    public string Artist { get; set; } = "";
    /// <summary>
    /// Album name; Record name
    /// </summary>
    public string Album { get; set; } = "";
    /// <summary>
    /// Release year
    /// </summary>
    public string Year { get; set; } = "";
    /// <summary>
    /// Comment for the track
    /// </summary>
    public string Comment { get; set; } = "";
    /// <summary>
    /// Track number
    /// </summary>
    public byte Track { get; set; } = 0;
    /// <summary>
    /// Genre
    /// </summary>
    public ID3v1Genre Genre { get; set; } = ID3v1Genre.Unset;

    /// <summary>
    /// Reads the tag from existing file, the file must contain the tag at end
    /// </summary>
    /// <param name="file">Path to the file</param>
    /// <returns>Error code (FileNotFound, UnexpectedEnd, NoTag); TagError.None on success</returns>
    public TagError Read(string file)
    {
        if (!File.Exists(file))
            return TagError.FileNotFound;

        using FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read);
        return ReadStream(fs);
    }

    /// <summary>
    /// Reads the tag from the given stream
    /// </summary>
    /// <param name="stream">Stream to read the tag from</param>
    /// <param name="shouldSeek">Specifies whether to seek at the end of the stream (where this tag is usually located)</param>
    /// <returns>Error code (CannotRead, CannotSeek, UnexpectedEnd, NoTag); TagError.None on success</returns>
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

        ReadBytes(spanBuf);

        return TagError.None;
    }

    /// <summary>
    /// Reads the tag from the given binary data
    /// </summary>
    /// <param name="data">Binary data with the tag; must be exactly 128 bytes long; must include the tag identifier</param>
    /// <returns>Error code (InvalidSize, NoTag); ErrorCode.None on success</returns>
    public TagError Read(ReadOnlySpan<byte> data)
    {
        if (data.Length != size)
            return TagError.InvalidSize;
        if (data[..3].ToAscii() != id)
            return TagError.NoTag;

        ReadBytes(data);

        return TagError.None;
    }

    /// <summary>
    /// Reads tag from the end of a stream
    /// </summary>
    /// <param name="stream">Stream to read tag from; must be readable and seekable</param>
    /// <returns>Error code (UnexpectedEnd, NoTag); ErrorCode.None on success</returns>
    private TagError ReadStream(Stream stream)
    {
        stream.Seek(-size, SeekOrigin.End);
        byte[] buffer = new byte[size];
        
        if (stream.Read(buffer) != size)
            return TagError.UnexpectedEnd;
        
        ReadOnlySpan<byte> spanBuf = buffer.AsSpan();
        
        if (spanBuf[..3].ToAscii() != id)
            return TagError.NoTag;
        
        ReadBytes(buffer.AsSpan());
        
        return TagError.None;
    }

    
    /// <summary>
    /// Reads tag from byte span
    /// </summary>
    /// <param name="data">Binary data with the tag; Expects that the span is 128 bytes long</param>
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
