namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class CommercialFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public string Price { get; set; }
    public string ValidUntil { get; set; }
    public string ContactURL { get; set; }
    public DeliveryType RecievedAs { get; set; }
    public string SellerName { get; set; }
    public string Description { get; set; }
    public string PictureMIME { get; set; }
    public byte[] SellerLogo { get; set; }

    public CommercialFrame()
    {
        Header = default;
        Price = "cur0";
        ValidUntil = "00000000";
        ContactURL = "";
        RecievedAs = DeliveryType.Other;
        SellerName = "";
        Description = "";
        PictureMIME = "image/";
        SellerLogo = Array.Empty<byte>();
    }

    internal CommercialFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
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
            "C" => $"{ID}: {Description}",
            "A" => $"{ID}: (Commercial)\n" +
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

public enum DeliveryType : byte
{
    Other = 0x00,
    StandardCDAlbum = 0x01,
    CompressedCDAudio = 0x02,
    InternetFile = 0x03,
    InternetStream = 0x04,
    NoteSheets = 0x05,
    NoteSheetsBook = 0x06,
    OtherMediaMusic = 0x07,
    NonMusical = 0x08
}
