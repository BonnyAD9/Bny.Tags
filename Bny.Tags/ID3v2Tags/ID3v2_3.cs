namespace Bny.Tags.ID3v2Tags;

public static class ID3v2_3
{
    internal static ID3v2Error Read(ITag tag, Stream read, ID3v2Header header)
    {
        if (header.Flags.HasFlag(ID3v2HeaderFlags.Experimental_3))
            return ID3v2Error.Unsupported;

        UnsynchronizedStream stream = new(read, header.Flags.HasFlag(ID3v2HeaderFlags.Unsynchronisation_3));

        tag.SetTag(null, "init", false);
        
        if (header.Flags.HasFlag(ID3v2HeaderFlags.ExtendedHeader_3))
        {
            var exHeader = ID3v2_3ExtendedHeader.FromStream(stream);
            tag.SetTag(exHeader.CRCData, "CRC");
        }


        byte[] headerBuffer = new byte[ID3v2_3FrameHeader.size];
        while (stream.Position + ID3v2_3FrameHeader.size < header.Size)
        {
            if (stream.Read(headerBuffer) != ID3v2_3FrameHeader.size)
                return ID3v2Error.UnexpecterEnd;

            ReadOnlySpan<byte> headerBufSpan = headerBuffer.AsSpan();

            ID3v2_3FrameHeader frameHeader = ID3v2_3FrameHeader.FromBytes(headerBufSpan);

            if (frameHeader.IsEmpty)
                return ID3v2Error.None;

            if (stream.Position + frameHeader.Size >= header.Size)
                return ID3v2Error.InvalidSize;

            ID3v2Error err = ReadFrame(tag, stream, frameHeader);
            switch (err)
            {
                case ID3v2Error.None or ID3v2Error.InvalidData or ID3v2Error.Unsupported:
                    break;
                default:
                    return err;
            }
        }

        return ID3v2Error.None;
    }

    private static ID3v2Error ReadFrame(ITag tag, Stream stream, ID3v2_3FrameHeader header)
    {
        byte[] buffer = new byte[header.Size];

        if (stream.Read(buffer) != header.Size)
            return ID3v2Error.UnexpecterEnd;

        if (header.Flags != ID3v2_3FrameHeaderFlags.None)
            return ID3v2Error.Unsupported;

        ReadOnlySpan<byte> data = buffer.AsSpan();

        switch (header.ID)
        {
            case ID3v2_3FrameHeaderID.TIT2: // Title
                tag.SetTag(data.ToID3v2_3String(), "Title");
                return ID3v2Error.None;
            case ID3v2_3FrameHeaderID.TPE1: // Artist
                tag.SetTag(ReadArtists(data), "Artist", false);
                return ID3v2Error.None;
            case ID3v2_3FrameHeaderID.TALB: // Album
                tag.SetTag(data.ToID3v2_3String(), "Album");
                return ID3v2Error.None;
            case ID3v2_3FrameHeaderID.TYER: // Year
                tag.SetTag(data.ToID3v2_3String(), "Year");
                return ID3v2Error.None;
            case ID3v2_3FrameHeaderID.COMM: // Comment
                tag.SetTag(ID3v2_3Comment.FromBytes(data), "Comment");
                return ID3v2Error.None;
            case ID3v2_3FrameHeaderID.TRCK: // Track
                tag.SetTag(data.ToID3v2_3String(), "Track");
                return ID3v2Error.None;
            case ID3v2_3FrameHeaderID.TCON: // Genre
                tag.SetTag(ID3v2_3Genre.FromBytes(data), "Genre");
                return ID3v2Error.None;
            default:
                return ID3v2Error.Unsupported;
        }
    }

    private static string[] ReadArtists(ReadOnlySpan<byte> data)
    {
        string str = data.ToID3v2_3String();
        return str.Split('/');
    }
}
