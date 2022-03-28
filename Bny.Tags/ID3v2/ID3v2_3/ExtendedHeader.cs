namespace Bny.Tags.ID3v2.ID3v2_3;

internal struct ExtendedHeader
{
    public uint Size { get; set; } = 0;
    public ExtendedFlags Flags { get; set; } = ExtendedFlags.None;
    public uint PaddingSize { get; set; } = 0;
    public uint CRCData { get; set; } = 0;

    public bool HasCRCData => Flags.HasFlag(ExtendedFlags.CRCData);

    public static ExtendedHeader FromStream(Stream stream)
    {
        ExtendedHeader header = new();
        
        byte[] sizeBuffer = new byte[4];
        
        if (stream.Read(sizeBuffer) != 4)
            return header;

        ReadOnlySpan<byte> sizeBufferSpan = sizeBuffer.AsSpan();
        header.Size = sizeBufferSpan.ToUInt32();

        byte[] buffer = new byte[header.Size];
        if (stream.Read(buffer) != header.Size)
            return header;

        ReadOnlySpan<byte> data = buffer.AsSpan();

        header.Flags = (ExtendedFlags)data[..2].ToUInt16();
        header.PaddingSize = data[2..6].ToUInt32();

        if (header.HasCRCData)
            header.CRCData = data[6..10].ToUInt32();

        return header;
    }

    public ExtendedHeader(ReadOnlySpan<byte> data)
    {
        Size = data.ToUInt32();
        Flags = (ExtendedFlags)data[..2].ToUInt16();
        PaddingSize = (uint)data[2..6].ToUInt32();

        if (HasCRCData)
            CRCData = data[6..10].ToUInt32();
    }
}

[Flags]
internal enum ExtendedFlags : ushort
{
    None = 0,
    CRCData = 0b1000_0000__0000_0000,
}
