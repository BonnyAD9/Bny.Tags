namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Ownership frame
/// </summary>
public class OwnershipFrame : Frame
{
    /// <summary>
    /// Price payed preceaded with currency code according to ISO-4217
    /// </summary>
    public string PricePayed { get; set; }
    /// <summary>
    /// Date of purchase in format "YYYYMMDD"
    /// </summary>
    public string DateOfPurchase { get; set; }
    /// <summary>
    /// Seller
    /// </summary>
    public string Seller { get; set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public OwnershipFrame() : base()
    {
        PricePayed = "cur0";
        DateOfPurchase = "00000000";
        Seller = "";
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal OwnershipFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        var enc = (Encoding)data[0];
        int pos = 1;
        PricePayed = data[1..].ToID3v2_3String(Encoding.ISO_8859_1, ref pos);
        DateOfPurchase = data[pos..(pos + 8)].ToISO_8859_1();
        pos += 8;
        Seller = data[pos..].ToID3v2_3String(enc);
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {Seller}",
            "A" => $"{ID.String()}: (Ownership)\n" +
                   $"  Price Payed: {PricePayed}\n" +
                   $"  Date Of Purchase: {DateOfPurchase}\n" +
                   $"  Seller: {Seller}",
            _ => throw new FormatException()
        };
    }
}
