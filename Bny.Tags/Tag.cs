namespace Bny.Tags;

public class Tag : ITag
{
    public string Title { get; set; } = "";
    public string Artist { get; set; } = "";
    public string Album { get; set; } = "";
    public string Year { get; set; } = "";
    public string Comment { get; set; } = "";
    public byte Track { get; set; } = 0;
    public ID3v1Genre ID3v1Genre { get; set; } = ID3v1Genre.Unset;
    public string Genre => ID3v1Genre.AsString();

    public static bool TryFromFile(string file, out Tag tag)
    {
        tag = new();
        return ID3v1.Read(tag, file);
    }

    public static Tag FromFile(string file)
    {
        if (TryFromFile(file, out Tag tag))
            return tag;
        throw new FileNotFoundException($"Couldn't filnd file '{file}'");
    }

    public bool SetTag(object? tag, string tagId)
    {
        if (tag is null)
            return false;

        switch (tag)
        {
            case string s:
                return SetString(s, tagId);
            case byte b:
                if (tagId == "Track")
                {
                    Track = b;
                    return true;
                }
                return false;
            case ID3v1Genre g:
                if (tagId == "Genre")
                {
                    ID3v1Genre = g;
                    return true;
                }
                return false;
            default:
                string? stag = tag.ToString();
                return stag is not null && SetString(stag, tagId);
        }
    }

    private bool SetString(string str, string id)
    {
        switch (id)
        {
            case "Title":
                Title = str;
                return true;
            case "Artist":
                Artist = str;
                return true;
            case "Album":
                Album = str;
                return true;
            case "Year":
                Year = str;
                return true;
            case "Comment":
                Comment = str;
                return true;
            case "Genre":
                ID3v1Genre = ID3v1GenreExtensions.Parse(str);
                return true;
            case "Track":
                if (byte.TryParse(str, out byte b))
                {
                    Track = b;
                    return true;
                }
                return false;
            default:
                return false;
        }
    }

    public object? GetTag(string tagId) => tagId switch
    {
        "Title" => Title,
        "Artist" => Artist,
        "Album" => Album,
        "Year" => Year,
        "Comment" => Comment,
        "Genre" => Genre,
        "Track" => Track.ToString(),
        "Track:u8" => Track,
        "Genre:ID3v1" => ID3v1Genre,
        "Track:n" => Track,
        _ => null,
    };
}
