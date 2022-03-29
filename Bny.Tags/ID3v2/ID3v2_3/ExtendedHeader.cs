namespace Bny.Tags.ID3v2.ID3v2_3;

/// <summary>
/// Contains extra information about the tag
/// </summary>
internal struct ExtendedHeader
{
    /// <summary>
    /// Size of the extended header
    /// </summary>
    public uint Size { get; set; } = 0;
    /// <summary>
    /// Additional info, contains only CRCData bit
    /// </summary>
    public ExtendedFlags Flags { get; set; } = ExtendedFlags.None;
    /// <summary>
    /// Size of padding at the end of the tag
    /// </summary>
    public uint PaddingSize { get; set; } = 0;
    /// <summary>
    /// Cyclic redundancy check
    /// </summary>
    public uint CRCData { get; set; } = 0;

    /// <summary>
    /// Checks whether Flags has CRCData flag
    /// </summary>
    public bool HasCRCData => Flags.HasFlag(ExtendedFlags.CRCData);

    /// <summary>
    /// Reads the extended header data from stream
    /// It is recomended to use constructor to initialize from ReadOnlySpan<byte> instead of stream when possible
    /// </summary>
    /// <param name="stream">Stream to read data from</param>
    /// <returns>Newly created ExtendedHeader</returns>
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

    /// <summary>
    /// Initializes ExtendedHeader from binary data
    /// </summary>
    /// <param name="data">Binary data (usually from file just after header if the header has extended header flag)</param>
    public ExtendedHeader(ReadOnlySpan<byte> data)
    {
        Size = data.ToUInt32();
        Flags = (ExtendedFlags)data[..2].ToUInt16();
        PaddingSize = (uint)data[2..6].ToUInt32();

        if (HasCRCData)
            CRCData = data[6..10].ToUInt32();
    }
}

/// <summary>
/// Flags for extended header
/// </summary>
[Flags]
internal enum ExtendedFlags : ushort
{
    /// <summary>
    /// No flag is set
    /// </summary>
    None = 0,
    /// <summary>
    /// Header contains CRC (Cyclic Redundancy Check)
    /// </summary>
    CRCData = 0b1000_0000__0000_0000,
}
