using Bny.Tags.ID3v1Tags;

namespace Bny.Tags.ID3v2Tags;

public struct ID3v2_3Genre : IGenre
{
    public ID3v1GenreEnum[] Genres { get; set; } = Array.Empty<ID3v1GenreEnum>();
    public string Genre { get; set; } = "";
    public string Name
    {
        get => Genre == "" && Genres.Length > 0 ? Genres[0].AsString() : Genre;
        set
        {
            Genres = Array.Empty<ID3v1GenreEnum>();
            Genre = value;
        }
    }

    public ID3v2_3Genre(string genre) => Genre = genre;

    public ID3v1GenreEnum GetID3V1Genre()
    {
        foreach (var g in Genres)
        {
            if (g is not ID3v1GenreEnum.Remix or ID3v1GenreEnum.Cover)
                return g;
        }
        return ID3v1GenreExtensions.Parse(Genre);
    }

    internal static ID3v2_3Genre FromBytes(ReadOnlySpan<byte> data)
    {
        string sData = data.ToID3v2_3String();
        ReadOnlySpan<char> span = sData.AsSpan();
        ID3v2_3Genre genre = new();
        List<ID3v1GenreEnum> genres = new();

        for (int i = 0; i < span.Length - 1;)
        {
            if (span[i] != '(')
            {
                genre.Genre = span[i..].ToString();
                break;
            }

            if (span[i..].StartsWith("(("))
            {
                genre.Genre = span[(i + 1)..].ToString();
                break;
            }

            if (span[i..].StartsWith("(RX)"))
            {
                genres.Add(ID3v1GenreEnum.Remix);
                i += 4;
                continue;
            }

            if (span[i..].StartsWith("(RC)"))
            {
                genres.Add(ID3v1GenreEnum.Cover);
                i += 4;
                continue;
            }

            i++;
            int index = span[i..].IndexOf(')');
            if (index == -1)
                break;

            index += i;

            if (byte.TryParse(span[i..index], out byte g))
                genres.Add((ID3v1GenreEnum)g);
            
            i = index + 1;
        }

        genre.Genres = genres.ToArray();

        return genre;
    }

    public override string ToString() => Name;
}
