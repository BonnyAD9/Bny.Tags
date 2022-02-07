namespace Bny.Tags;

public class ID3v1Tag : IID3v1Tag
{
    public string Title { get; set; } = "";
    public string Artist { get; set; } = "";
    public string Album { get; set; } = "";
    public string Year { get; set; } = "";
    public string Comment { get; set; } = "";
    public byte TrackNumber { get; set; } = 0;
    public ID3v1Genre GenreEnum { get; set; } = ID3v1Genre.Unset;
    public string Genre => GenreEnum.String();
}
