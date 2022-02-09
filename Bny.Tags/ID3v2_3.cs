namespace Bny.Tags;

public static class ID3v2_3
{
    internal static ID3v2Error Read(ITag tag, Stream stream, ID3v2Header header)
    {
        if (header.Flags != ID3v2HeaderFlags.None)
            return ID3v2Error.Unsupported;

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
            case ID3v2_3FrameHeaderID.TIT2:
                tag.SetTag(data.ToID3v2_3VariableEncoding(), "Title");
                return ID3v2Error.None;
            default:
                return ID3v2Error.Unsupported;
        }
    }
}
