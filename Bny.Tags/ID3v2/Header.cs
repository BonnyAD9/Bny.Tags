namespace Bny.Tags.ID3v2;

/// <summary>
/// Contains essential information about ID3v2 tag
/// </summary>
internal struct Header
{
    /// <summary>
    /// Size of the header in bytes
    /// </summary>
    public static int size = 10;

    /// <summary>
    /// Version of the standard (ID3v2.2, ID3v2.3, ID3v2.4)
    /// </summary>
    public Version Version { get; set; }
    /// <summary>
    /// These have different meaning in different versions
    /// </summary>
    public HeaderFlags Flags { get; set; }
    /// <summary>
    /// Size of the whole tag
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// Creates empty header with unknown version
    /// </summary>
    public Header()
    {
        Version = Version.Unknown;
        Flags = HeaderFlags.None;
        Size = 0;
    }

    /// <summary>
    /// Creates header from binary data
    /// </summary>
    /// <param name="data"></param>
    public Header(ReadOnlySpan<byte> data)
    {
        Version = (Version)data[3..5].ToUInt16();
        Flags = (HeaderFlags)data[5];

        // Size uses only last 7 bits in every byte
        Size = data[6];
        Size = (Size << 7) | data[7];
        Size = (Size << 7) | data[8];
        Size = (Size << 7) | data[9];
    }
}

/// <summary>
/// Header flags, bits have different meaning in different versions
/// Version number is appended to the name
/// </summary>
[Flags]
internal enum HeaderFlags : byte
{
    None = 0,
    Unsynchronisation_3 = 0b1000_0000,
    ExtendedHeader_3 = 0b0100_0000,
    Experimental_3 = 0b0010_0000,
}

/// <summary>
/// Version of the tag
/// Versions aren't cross compatible
/// </summary>
internal enum Version : ushort
{
    Unknown = 0,
    ID3v2_2 = 0x0200,
    ID3v2_3 = 0x0300,
    ID3v2_4 = 0x0400,
}
