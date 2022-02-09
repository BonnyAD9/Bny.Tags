namespace Bny.Tags.ID3v2Tags;

internal struct ID3v2_3ExtendedHeader
{
    public uint Size { get; set; } = 0;
    public ID3v2_3ExtendedFlags Flags { get; set; } = ID3v2_3ExtendedFlags.None;
    public uint PaddingSize { get; set; } = 0;
    public uint CRCData { get; set; } = 0;

    public bool HasCRCData => Flags.HasFlag(ID3v2_3ExtendedFlags.CRCData);

    public static ID3v2_3ExtendedHeader FromStream(Stream stream)
    {
        ID3v2_3ExtendedHeader header = new();
        
        byte[] sizeBuffer = new byte[4];
        
        if (stream.Read(sizeBuffer) != 4)
            return header;

        ReadOnlySpan<byte> sizeBufferSpan = sizeBuffer.AsSpan();
        header.Size = sizeBufferSpan.ToUInt32();

        byte[] buffer = new byte[header.Size];
        if (stream.Read(buffer) != header.Size)
            return header;

        ReadOnlySpan<byte> data = buffer.AsSpan();

        header.Flags = (ID3v2_3ExtendedFlags)data[..2].ToUInt16();
        header.PaddingSize = data[2..6].ToUInt32();

        if (header.HasCRCData)
            header.CRCData = data[6..10].ToUInt32();

        return header;
    }
}

[Flags]
internal enum ID3v2_3ExtendedFlags : ushort
{
    None = 0,
    CRCData = 0b1000_0000__0000_0000,
}
