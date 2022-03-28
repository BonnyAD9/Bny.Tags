namespace Bny.Tags.ID3v2.ID3v2_3;

internal struct FrameHeader
{
    public const int size = 10;

    public FrameID ID { get; set; } = FrameID.Invalid;
    public uint Size { get; private set; } = 0;
    public FrameHeaderFlags Flags { get; set; } = FrameHeaderFlags.None;
    public uint DecompressedSize { get; private set; } = 0;
    public byte EncriptionMethod { get; private set; } = 0;
    public byte GroupIdentifier { get; private set; } = 0;

    public bool IsExperimental =>
        (ID | FrameID.Mask1) == FrameID.X ||
        (ID | FrameID.Mask1) == FrameID.Y ||
        (ID | FrameID.Mask1) == FrameID.Z;
    public bool IsText => (ID & FrameID.Mask1) == FrameID.T;
    public bool IsURL => (ID & FrameID.Mask1) == FrameID.W;
    public bool IsValid => ID != FrameID.Invalid && ID != FrameID.None;
    public bool IsEmpty => ID == FrameID.None;
    public bool IsCompressed => Flags.HasFlag(FrameHeaderFlags.Compression);
    public bool IsEncrypted => Flags.HasFlag(FrameHeaderFlags.Encryption);
    public bool HasGroup => Flags.HasFlag(FrameHeaderFlags.GroupingIdentity);
    public bool TagAlterPreserve => Flags.HasFlag(FrameHeaderFlags.TagAlterPreservation);
    public bool FileAlterPreserve => Flags.HasFlag(FrameHeaderFlags.FileAlterPreservation);
    public bool ReadOnly => Flags.HasFlag(FrameHeaderFlags.ReadOnly);

    public FrameHeader() { }

    internal static FrameHeader FromBytes(ReadOnlySpan<byte> data, out int headerSize)
    {
        headerSize = size;
        FrameHeader header = new();

        header.ID = (FrameID)data[0..4].ToUInt32();
        header.Size = data[4..8].ToUInt32();
        header.Flags = (FrameHeaderFlags)data[8..10].ToUInt16();

        if (header.Flags.HasFlag(FrameHeaderFlags.Compression))
        {
            header.DecompressedSize = data[10..14].ToUInt32();
            headerSize += 4;
        }
        if (header.Flags.HasFlag(FrameHeaderFlags.Encryption))
        {
            header.EncriptionMethod = data[14];
            headerSize += 1;
        }
        if (header.Flags.HasFlag(FrameHeaderFlags.GroupingIdentity))
        {
            header.GroupIdentifier = data[15];
            headerSize += 1;
        }

        return header;
    }
}

[Flags]
internal enum FrameHeaderFlags : ushort
{
    None = 0,
    TagAlterPreservation = 0b_1000_0000__0000_0000,
    FileAlterPreservation = 0b_0100_0000__0000_0000,
    ReadOnly = 0b_0010_0000__0000_0000,
    Compression = 0b_0000_0000__1000_0000,
    Encryption = 0b_0000_0000__0100_0000,
    GroupingIdentity = 0b_0000_0000__0010_0000,
}