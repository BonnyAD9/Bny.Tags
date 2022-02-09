namespace Bny.Tags.ID3v1Tags;

public class ID3v1Tag : ITag
{
    public string Title { get; set; } = "";
    public string Artist { get; set; } = "";
    public string Album { get; set; } = "";
    public string Year { get; set; } = "";
    public string Comment { get; set; } = "";
    public byte Track { get; set; } = 0;
    public IGenre Genre { get; set; } = new ID3v1Genre();

    public bool SetTag(object? tag, string tagId, bool canToString)
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
            case IGenre g:
                if (tagId == "Genre")
                {
                    Genre = g;
                    return true;
                }
                return false;
            default:
                if (!canToString)
                    return false;
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
                Genre = new Genre(str);
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
        "Track" => Track.ToString(),
        "Genre" => Genre,
        "Track:u8" => Track,
        "Genre:IGenre" => Genre,
        _ => null,
    };
}