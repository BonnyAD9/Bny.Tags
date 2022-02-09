using Bny.Tags.ID3v1Tags;
using Bny.Tags.ID3v2Tags;
using System.Text;

namespace Bny.Tags;

public class Tag : ITag
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
    public byte Track { get; set; } = 0;
    public byte TrackOf { get; set; } = 0;
    public string TrackStr
    {
        get
        {
            StringBuilder sb = new();
            sb.Append(Track);
            if (TrackOf == 0)
                return sb.ToString();
            return sb.Append('/').Append(TrackOf).ToString();
        }
        set
        {
            byte track;
            int i = value.IndexOf('/');
            if (i == -1)
            {
                if (byte.TryParse(value, out track))
                {
                    Track = track;
                    TrackOf = 0;
                }
                return;
            }

            var valSpan = value.AsSpan();

            if (byte.TryParse(valSpan[..i], out track))
                Track = track;
            if (byte.TryParse(valSpan[(i + 1)..], out byte trackOf))
                TrackOf = trackOf;
        }
    }
    public IGenre Genre { get; set; } = new Genre();
    public uint CRC { get; set; } = 0;

    public static bool TryFromFile(string file, out Tag tag)
    {
        tag = new();
        FileInfo fi = new(file);
        if (!fi.Exists)
            return false;
        switch (fi.Extension.ToLower())
        {
            case ".mp3":
                if (ID3v2.Read(tag, file) == ID3v2Error.None)
                    return true;
                return ID3v1.Read(tag, file);
            default:
                return false;
        }
    }

    public static Tag FromFile(string file)
    {
        if (TryFromFile(file, out Tag tag))
            return tag;
        throw new FileNotFoundException($"Couldn't filnd file '{file}'");
    }

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
            case byte b:
                if (tagId == "Track")
                {
                    Track = b;
                    TrackOf = 0;
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
            case string[] s:
                if (tagId == "Artist")
                {
                    Artists = s;
                    return true;
                }
                return false;
            case IComment c:
                if (tagId == "Comment")
                {
                    Comments.Add(c);
                    return true;
                }
                return false;
            case uint i:
                if (tagId == "CRC")
                {
                    CRC = i;
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
                TrackStr = str;
                return false;
            case "CRC":
                if (uint.TryParse(str, out uint crc))
                {
                    CRC = crc;
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
        "Genre" => Genre.Name,
        "Track" => TrackStr,
        "CRC" => CRC.ToString(),
        "Artist[]" => Artists,
        "Comment:IComment[]" => Comments.ToArray(),
        "Track:u8" => Track,
        "Genre:IGenre" => Genre,
        "CRC:u32" => CRC,
        _ => null,
    };
}
