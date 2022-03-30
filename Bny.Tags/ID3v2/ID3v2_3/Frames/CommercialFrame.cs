namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Commercial frame (ID3v2.3)
/// </summary>
public class CommercialFrame : Frame
{
    /// <summary>
    /// Price preceeded by currency code according to ISO-4217
    /// </summary>
    public string Price { get; set; }
    /// <summary>
    /// Valid until date in format "YYYYMMDD"
    /// </summary>
    public string ValidUntil { get; set; }
    /// <summary>
    /// Contact URL
    /// </summary>
    public string ContactURL { get; set; }
    /// <summary>
    /// Product/delivery type
    /// </summary>
    public DeliveryType RecievedAs { get; set; }
    /// <summary>
    /// Name of seller
    /// </summary>
    public string SellerName { get; set; }
    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// Company logotype picture MIME type
    /// </summary>
    public string PictureMIME { get; set; }
    /// <summary>
    /// Company logotype picture (binary data)
    /// </summary>
    public byte[] SellerLogo { get; set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public CommercialFrame() : base()
    {
        Price = "cur0";
        ValidUntil = "00000000";
        ContactURL = "";
        RecievedAs = DeliveryType.Other;
        SellerName = "";
        Description = "";
        PictureMIME = "image/";
        SellerLogo = Array.Empty<byte>();
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal CommercialFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        var enc = (Encoding)data[0];
        int pos = 1;
        Price = data[1..].ToID3v2_3String(Encoding.ISO_8859_1, ref pos);
        ValidUntil = data[pos..(pos + 8)].ToISO_8859_1();
        pos += 8;
        ContactURL = data[pos..].ToID3v2_3String(Encoding.ISO_8859_1, ref pos);
        RecievedAs = (DeliveryType)data[pos];
        pos++;
        SellerName = data[pos..].ToID3v2_3String(enc, ref pos);
        Description = data[pos..].ToID3v2_3String(enc, ref pos);
        PictureMIME = data[pos..].ToID3v2_3String(Encoding.ISO_8859_1, ref pos);
        SellerLogo = data[pos..].ToArray();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {Description}",
            "A" => $"{ID.String()}: (Commercial)\n" +
                   $"  Price: {Price}\n" +
                   $"  Valid Until: {ValidUntil}\n" +
                   $"  Contact URL: {ContactURL}\n" +
                   $"  Recieved As: {RecievedAs}\n" +
                   $"  Seller Name: {SellerName}\n" +
                   $"  Description: {Description}\n" +
                   $"  Picture MIME: {PictureMIME}",
            _ => throw new FormatException()
        };
    }
}

/// <summary>
/// Product/delivery type (ID3v2.3)
/// </summary>
public enum DeliveryType : byte
{
    Other = 0x00,
    /// <summary>
    /// Standard CD album with other songs
    /// </summary>
    StandardCDAlbum = 0x01,
    /// <summary>
    /// Compressed audio on CD
    /// </summary>
    CompressedCDAudio = 0x02,
    /// <summary>
    /// File over the Internet
    /// </summary>
    InternetFile = 0x03,
    /// <summary>
    /// Stream over the Internet
    /// </summary>
    InternetStream = 0x04,
    /// <summary>
    /// As note sheets
    /// </summary>
    NoteSheets = 0x05,
    /// <summary>
    /// As note sheets in a book with other sheets
    /// </summary>
    NoteSheetsBook = 0x06,
    /// <summary>
    /// Music on other media
    /// </summary>
    OtherMediaMusic = 0x07,
    /// <summary>
    /// Non-musical merchandise
    /// </summary>
    NonMusical = 0x08
}
