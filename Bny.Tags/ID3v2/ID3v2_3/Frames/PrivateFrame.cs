﻿namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class PrivateFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public string OwnerID { get; set; }
    public byte[] Data { get; set; }

    public PrivateFrame()
    {
        Header = default;
        OwnerID = "";
        Data = Array.Empty<byte>();
    }

    internal PrivateFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
        int pos = 0;
        OwnerID = data.ToID3v2_3String(Encoding.ISO_8859_1, ref pos);
        Data = data[pos..].ToArray();
    }

    public override string ToString()
    {
        return ToString("G");
    }

    public string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.ToString(),
            "C" => $"{ID}: {OwnerID}",
            "A" => $"{ID}: (Private Frame)\n" +
                   $"  Owner ID: {OwnerID}\n" +
                   $"  Data: {Data.Length} B",
            _ => throw new FormatException()
        };
    }
}
