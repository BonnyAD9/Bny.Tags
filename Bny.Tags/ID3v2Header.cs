namespace Bny.Tags;

internal struct ID3v2Header
{
    public static int size = 10;

    public ID3v2Version Version { get; set; } = ID3v2Version.Unknown;
    public ID3v2HeaderFlags Flags { get; set; } = ID3v2HeaderFlags.None;
    public int Size { get; set; } = 0;

    public static ID3v2Header FromBytes(ReadOnlySpan<byte> data)
    {
        ID3v2Header header = new();

        header.Version = (ID3v2Version)data.ToUInt16(3..5);
        
        header.Flags = (ID3v2HeaderFlags)data[5];
        
        header.Size = data[9];
        header.Size = (header.Size << 7) | data[8];
        header.Size = (header.Size << 7) | data[7];
        header.Size = (header.Size << 7) | data[6];

        return header;
    }
}

[Flags]
internal enum ID3v2HeaderFlags : byte
{
    None = 0,
    Unsynchronisation_3 = 0b1000_0000,
    ExtendedHeader_3 = 0b0100_0000,
    Experimental_3 = 0b0010_0000,
}

internal enum ID3v2Version : ushort
{
    Unknown = 0,
    ID3v2_2 = 0x0200,
    ID3v2_3 = 0x0300,
    ID3v2_4 = 0x0400,
}
