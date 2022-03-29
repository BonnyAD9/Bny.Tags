namespace Bny.Tags.ID3v2.ID3v2_3;

/// <summary>
/// Header for ID3v2.3 Frame
/// </summary>
internal struct FrameHeader
{
    /// <summary>
    /// Size of the header in bytes
    /// </summary>
    public const int size = 10;

    /// <summary>
    /// Frame identificator
    /// </summary>
    public FrameID ID { get; set; } = FrameID.Invalid;
    /// <summary>
    /// Size of the frame (without the header)
    /// </summary>
    public uint Size { get; private set; } = 0;
    /// <summary>
    /// Additional info about the tag
    /// </summary>
    public FrameHeaderFlags Flags { get; set; } = FrameHeaderFlags.None;
    /// <summary>
    /// Size of the frame after decompression (0 if Compression flag is not set)
    /// </summary>
    public uint DecompressedSize { get; private set; } = 0;
    /// <summary>
    /// Byte indicating which method was used to encrypt this frame (0 if Encryption flag is not set)
    /// Info about the method should be included in EncryptionRegistrationFrame
    /// </summary>
    public byte EncriptionMethod { get; private set; } = 0;
    /// <summary>
    /// ID of group in which this frame belongs (0 if GroupingIdentity flag is not set)
    /// More info should be in GroupRegistrationFrame
    /// </summary>
    public byte GroupID { get; private set; } = 0;

    /// <summary>
    /// True if the ID indicates that the frame is experimental (starts with X, Y or Z)
    /// </summary>
    public bool IsExperimental =>
        (ID & FrameID.Mask1) == FrameID.X ||
        (ID & FrameID.Mask1) == FrameID.Y ||
        (ID & FrameID.Mask1) == FrameID.Z;
    /// <summary>
    /// true if the ID indicates that the frame is text field (starts with T)
    /// </summary>
    public bool IsText => (ID & FrameID.Mask1) == FrameID.T;
    /// <summary>
    /// true if the ID indicates that the frame is URL (starts with W)
    /// </summary>
    public bool IsURL => (ID & FrameID.Mask1) == FrameID.W;
    /// <summary>
    /// false if the ID is Invalid (XXXX) or None (all bytes 0)
    /// </summary>
    public bool IsValid => ID != FrameID.Invalid && ID != FrameID.None;
    /// <summary>
    /// true if the ID is None (all bytes 0)
    /// </summary>
    public bool IsEmpty => ID == FrameID.None;
    /// <summary>
    /// Indicates whether the Compression flag is set
    /// </summary>
    public bool IsCompressed => Flags.HasFlag(FrameHeaderFlags.Compression);
    /// <summary>
    /// Indicates whether the Encryption flag is set
    /// </summary>
    public bool IsEncrypted => Flags.HasFlag(FrameHeaderFlags.Encryption);
    /// <summary>
    /// Indicates whether the GroupingIdentity flag is set
    /// </summary>
    public bool HasGroup => Flags.HasFlag(FrameHeaderFlags.GroupingIdentity);
    /// <summary>
    /// Indicates whether the TagAlterPreservation flag is set
    /// </summary>
    public bool TagAlterPreserve => Flags.HasFlag(FrameHeaderFlags.TagAlterPreservation);
    /// <summary>
    /// Indicates whether the FileAlterPreservation flag is set
    /// </summary>
    public bool FileAlterPreserve => Flags.HasFlag(FrameHeaderFlags.FileAlterPreservation);
    /// <summary>
    /// Indicates whether the ReadOnly flag is set
    /// </summary>
    public bool ReadOnly => Flags.HasFlag(FrameHeaderFlags.ReadOnly);

    /// <summary>
    /// Initializes invalid empty FrameHeader
    /// </summary>
    public FrameHeader() { }

    /// <summary>
    /// Initializes frame header from binary data
    /// </summary>
    /// <param name="data">Binary data</param>
    /// <param name="headerSize">returns the bytes read</param>
    internal FrameHeader(ReadOnlySpan<byte> data, out int headerSize)
    {
        headerSize = size;

        ID = (FrameID)data[0..4].ToUInt32();
        Size = data[4..8].ToUInt32();
        Flags = (FrameHeaderFlags)data[8..10].ToUInt16();

        if (IsCompressed)
        {
            DecompressedSize = data[10..14].ToUInt32();
            headerSize += 4;
        }
        if (IsEncrypted)
        {
            EncriptionMethod = data[14];
            headerSize += 1;
        }
        if (HasGroup)
        {
            GroupID = data[15];
            headerSize += 1;
        }
    }
}

/// <summary>
/// Flags for ID3v2.3 frame header
/// </summary>
[Flags]
internal enum FrameHeaderFlags : ushort
{
    /// <summary>
    /// No flag is set
    /// </summary>
    None = 0,
    /// <summary>
    /// Indicates whether the frame should be discarted if it is unknown and it was altered in any way
    /// </summary>
    TagAlterPreservation = 0b_1000_0000__0000_0000,
    /// <summary>
    /// Indicates whether the frame should be discarted if the file (excluding tag) was altered in any way (does not apply when the audio is completly replaced with other audio data)
    /// </summary>
    FileAlterPreservation = 0b_0100_0000__0000_0000,
    /// <summary>
    /// Indicates that the contents of this tag are intended to be read only (changing the contents may break something, e.g. signature)
    /// If the contents are changed without taking proper means to compensate (e.g. recalculating signature), the flag should be cleared
    /// </summary>
    ReadOnly = 0b_0010_0000__0000_0000,
    /// <summary>
    /// Indicates whether the frame is compressed
    /// </summary>
    Compression = 0b_0000_0000__1000_0000,
    /// <summary>
    /// Indicates whether the frame is encrypted
    /// </summary>
    Encryption = 0b_0000_0000__0100_0000,
    /// <summary>
    /// Indicates whether the frame belongs in a group with other frames
    /// </summary>
    GroupingIdentity = 0b_0000_0000__0010_0000,
}