namespace Bny.Tags.ID3v2Tags;
public class ID3v2Tag : ITag
{
    public string Title { get; set; } = "";
    public string[] Artists { get; set; } = Array.Empty<string>();
    public string Artist
    {
        get => Artists.Length == 0 ? "" : Artists[0];
        set => Artists = new string[] { value };
    }
    public string Album { get; set; } = "";
    public string Year { get; set; } = "";
    public List<IComment> Comments { get; set; } = new();
    public string Comment
    {
        get => Comments.Count == 0 ? "" : Comments[0].Text;
        set
        {
            Comments.Clear();
            Comments.Add(new Comment(value));
        }
    }
    public string Track { get; set; } = "";
    public IGenre Genre { get; set; } = new ID3v2_3Genre();

    public bool SetTag(object? tag, string tagId, bool canToString = false)
    {
        if (tagId == "init")
        {
            Comments.Clear();
            return true;
        }

        if (tag is null)
            return false;

        switch (tag)
        {
            case string s:
                return SetString(s, tagId);
            case string[] s:
                if (tagId == "Artist")
                {
                    Artists = s;
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
            case "Track":
                Track = str;
                return true;
            case "Genre":
                Genre = new ID3v2_3Genre(str);
                return true;
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
        "Track" => Track,
        "Genre" => Genre.Name,
        "Artist[]" => Artists,
        "Comment:IComment[]" => Comments,
        "Genre:IGenre" => Genre,
        _ => null,
    };
}
