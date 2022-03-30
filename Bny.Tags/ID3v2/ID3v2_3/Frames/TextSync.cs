namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Text with time stamp (ID3v2.3)
/// </summary>
public struct TextSync
{
    /// <summary>
    /// Text
    /// </summary>
    public string Text { get; set; }
    /// <summary>
    /// Time stamp
    /// May be in milliseconds or MPEG frames
    /// </summary>
    public uint TimeStamp { get; set; }

    /// <summary>
    /// Creates empty sync
    /// </summary>
    public TextSync()
    {
        Text = "";
        TimeStamp = 0;
    }

    /// <summary>
    /// Initializes sync from binary data
    /// </summary>
    /// <param name="data">Binary data to read from</param>
    /// <param name="encoding">encoding of the text</param>
    /// <param name="pos">Number of byte read</param>
    internal TextSync(ReadOnlySpan<byte> data, Encoding encoding, out int pos)
    {
        pos = 0;
        Text = data.ToID3v2_3String(encoding, ref pos);
        TimeStamp = data[pos..].ToUInt16();
        pos += 2;
    }

    public override string ToString()
    {
        return $"{TimeStamp}: {Text}";
    }
}
