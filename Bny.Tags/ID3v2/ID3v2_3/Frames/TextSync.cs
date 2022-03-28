namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public struct TextSync
{
    public string Text { get; set; }
    public uint TimeStamp { get; set; }

    public TextSync()
    {
        Text = "";
        TimeStamp = 0;
    }

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
