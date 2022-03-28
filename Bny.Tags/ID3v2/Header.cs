namespace Bny.Tags.ID3v2;

internal struct Header
{
    public static int size = 10;

    public Version Version { get; set; }
    public HeaderFlags Flags { get; set; }
    public int Size { get; set; }

    public Header()
    {
        Version = Version.Unknown;
        Flags = HeaderFlags.None;
        Size = 0;
    }

    public Header(ReadOnlySpan<byte> data)
    {
        Version = (Version)data[3..5].ToUInt16();
        Flags = (HeaderFlags)data[5];
        Size = data[6];
        Size = (Size << 7) | data[7];
        Size = (Size << 7) | data[8];
        Size = (Size << 7) | data[9];
    }
}

[Flags]
internal enum HeaderFlags : byte
{
    None = 0,
    Unsynchronisation_3 = 0b1000_0000,
    ExtendedHeader_3 = 0b0100_0000,
    Experimental_3 = 0b0010_0000,
}

internal enum Version : ushort
{
    Unknown = 0,
    ID3v2_2 = 0x0200,
    ID3v2_3 = 0x0300,
    ID3v2_4 = 0x0400,
}
