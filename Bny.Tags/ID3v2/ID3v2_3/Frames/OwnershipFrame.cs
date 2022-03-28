namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class OwnershipFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public string PricePayed { get; set; }
    public string DateOfPurchase { get; set; }
    public string Seller { get; set; }

    public OwnershipFrame()
    {
        Header = default;
        PricePayed = "cur0";
        DateOfPurchase = "00000000";
        Seller = "";
    }

    internal OwnershipFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
        var enc = (Encoding)data[0];
        int pos = 1;
        PricePayed = data[1..].ToID3v2_3String(Encoding.ISO_8859_1, ref pos);
        DateOfPurchase = data[pos..(pos + 8)].ToISO_8859_1();
        pos += 8;
        Seller = data[pos..].ToID3v2_3String(enc);
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
            "C" => $"{ID}: {Seller}",
            "A" => $"{ID}: (Ownership)\n" +
                   $"  Price Payed: {PricePayed}\n" +
                   $"  Date Of Purchase: {DateOfPurchase}\n" +
                   $"  Seller: {Seller}",
            _ => throw new FormatException()
        };
    }
}
