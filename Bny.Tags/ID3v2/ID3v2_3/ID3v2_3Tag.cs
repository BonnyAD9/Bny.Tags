using Bny.Tags.ID3v2.ID3v2_3.Frames;

namespace Bny.Tags.ID3v2.ID3v2_3;

public class ID3v2_3Tag
{
    public const string id = "ID3";
    private Header Header { get; set; }
    private ExtendedHeader ExtendedHeader_3 { get; set; }
    private List<IFrame> Frames { get; set; } = new();
    public IEnumerable<IFrame> FramesEnum => Frames;

    public TagError Read(string file)
    {
        if (!File.Exists(file))
            return TagError.FileNotFound;

        using FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read);
        return ReadStream(fs);
    }

    public TagError Read(Stream stream)
    {
        if (!stream.CanRead)
            return TagError.CannotRead;

        return ReadStream(stream);
    }

    public TagError Read(ReadOnlySpan<byte> data)
    {
        if (data.Length < Header.size)
            return TagError.InvalidSize;

        if (data[..3].ToAscii() != id)
            return TagError.NoTag;

        Header = new(data);

        if (Header.Version != Version.ID3v2_3)
            return TagError.WrongVersion;

        if (Header.Flags.HasFlag(HeaderFlags.Experimental_3))
            return TagError.Unsupported;

        if (Header.Flags.HasFlag(HeaderFlags.Unsynchronisation_3))
            return TagError.Unsupported;

        return ReadBytes(data[Header.Size..]);
    }

    private TagError ReadStream(Stream stream)
    {
        byte[] headerBuffer = new byte[Header.size];
        if (stream.Read(headerBuffer) != Header.size)
            return TagError.UnexpectedEnd;

        ReadOnlySpan<byte> headerBufSpan = headerBuffer.AsSpan();

        if (headerBufSpan[..3].ToAscii() != id)
            return TagError.NoTag;

        Header = new(headerBufSpan);

        if (Header.Version != Version.ID3v2_3)
            return TagError.WrongVersion;

        if (Header.Flags.HasFlag(HeaderFlags.Experimental_3))
            return TagError.Unsupported;

        int bufferLen = Header.Size;
        byte[] buffer = new byte[bufferLen];

        if (stream.Read(buffer) != bufferLen)
            return TagError.UnexpectedEnd;

        if (Header.Flags.HasFlag(HeaderFlags.Unsynchronisation_3))
            Deunsynchronize(buffer, out bufferLen);

        ReadOnlySpan<byte> data = buffer.AsSpan();

        return ReadBytes(data[..bufferLen]);
    }

    private TagError ReadBytes(ReadOnlySpan<byte> data)
    {
        int pos = 0;

        if (Header.Flags.HasFlag(HeaderFlags.ExtendedHeader_3))
        {
            ExtendedHeader_3 = new(data);
            pos += (int)ExtendedHeader_3.Size;
        }

        while (pos + FrameHeader.size < data.Length)
        {
            FrameHeader frameHeader = FrameHeader.FromBytes(data[pos..], out int headerSize);
            pos += headerSize;

            if (frameHeader.IsEmpty)
                return TagError.None;

            if (pos + frameHeader.Size >= data.Length)
                return TagError.InvalidSize;

            var err = ReadFrame(data[pos..(pos + (int)frameHeader.Size)], frameHeader);
            switch (err)
            {
                case TagError.None or TagError.InvalidData or TagError.Unsupported:
                    break;
                default:
                    return err;
            }
            pos += (int)frameHeader.Size;
        }

        return TagError.None;
    }

    private TagError ReadFrame(ReadOnlySpan<byte> data, FrameHeader header)
    {
        if (header.IsCompressed || header.IsEncrypted)
            return TagError.Unsupported;

        if (!Enum.IsDefined(header.ID))
        {
            Frames.Add(new UnknownFrame(header, data));
            return TagError.Unsupported;
        }

        switch (header.ID)
        {
            case FrameID.UFID:
                Frames.Add(new UniqueFileIDFrame(header, data));
                return TagError.None;
            case FrameID.TXXX:
                Frames.Add(new UserDefinedTextFrame(header, data));
                return TagError.None;
            case FrameID.WXXX:
                Frames.Add(new UserDefinedTextFrame(header, data));
                return TagError.None;
            case FrameID.IPLS:
                Frames.Add(new InvolvedPeopleFrame(header, data));
                return TagError.None;
            case FrameID.MCDI:
                Frames.Add(new MusicCDIDFrame(header, data));
                return TagError.None;
            case FrameID.ETCO:
                Frames.Add(new EventTimingFrame(header, data));
                return TagError.None;
            case FrameID.MLLT:
                Frames.Add(new MPEGLocationLookupTableFrame(header, data));
                return TagError.None;
            case FrameID.SYTC:
                Frames.Add(new SynchronizedTempoCodesFrame(header, data));
                return TagError.None;
            case FrameID.USLT:
                Frames.Add(new UnsynchronisedLyricsFrame(header, data));
                return TagError.None;
            case FrameID.SYLT:
                Frames.Add(new SynchronizedLyricsFrame(header, data));
                return TagError.None;
            case FrameID.COMM:
                Frames.Add(new CommentFrame(header, data));
                return TagError.None;
            case FrameID.RVAD:
                Frames.Add(new RelativeVolumeAdjustmentFrame(header, data));
                return TagError.None;
            case FrameID.EQUA:
                Frames.Add(new EqualisationFrame(header, data));
                return TagError.None;
            case FrameID.RVRB:
                Frames.Add(new ReverbFrame(header, data));
                return TagError.None;
            case FrameID.APIC:
                Frames.Add(new PictureFrame(header, data));
                return TagError.None;
            case FrameID.GEOB:
                Frames.Add(new GeneralObjectFrame(header, data));
                return TagError.None;
            case FrameID.PCTN:
                Frames.Add(new PlayCounterFrame(header, data));
                return TagError.None;
            case FrameID.POPM:
                Frames.Add(new PopularimeterFrame(header, data));
                return TagError.None;
            case FrameID.RBUF:
                Frames.Add(new RecommendedBufferSizeFrame(header, data));
                return TagError.None;
            case FrameID.AENC:
                Frames.Add(new EncryptionFrame(header, data));
                return TagError.None;
            case FrameID.LINK:
                Frames.Add(new LinkedInformationFrame(header, data));
                return TagError.None;
            case FrameID.POSS:
                Frames.Add(new PositionSyncFrame(header, data));
                return TagError.None;
            case FrameID.USER:
                Frames.Add(new TermsOfUseFrame(header, data));
                return TagError.None;
            case FrameID.OWNE:
                Frames.Add(new OwnershipFrame(header, data));
                return TagError.None;
            case FrameID.COMR:
                Frames.Add(new CommercialFrame(header, data));
                return TagError.None;
            case FrameID.ENCR:
                Frames.Add(new EncryptionRegistrationFrame(header, data));
                return TagError.None;
            case FrameID.GRID:
                Frames.Add(new GroupRegistrationFrame(header, data));
                return TagError.None;
            case FrameID.PRIV:
                Frames.Add(new PrivateFrame(header, data));
                return TagError.None;
        }

        if (header.IsText)
        {
            Frames.Add(new TextFrame(header, data));
            return TagError.None;
        }

        if (header.IsURL)
        {
            Frames.Add(new URLFrame(header, data));
            return TagError.None;
        }

        return TagError.Unsupported;
    }

    public T? GetFrame<T>(FrameID id) where T : IFrame
    {
        var frame = Frames.FirstOrDefault(p => p.ID == id);
        if (frame is T res)
            return res;
        return default;
    }

    public IFrame? GetFrame(FrameID id)
    {
        return Frames.FirstOrDefault(p => p.ID == id);
    }

    public static void Deunsynchronize(byte[] buffer, out int len)
    {
        len = 0;
        for (int i = 1; i < buffer.Length; i++)
        {
            if (buffer[i - 1] == 255 && buffer[i] == 0)
                continue;
            buffer[len] = buffer[i - 1];
            len++;
        }
    }
}
