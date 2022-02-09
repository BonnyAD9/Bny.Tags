namespace Bny.Tags;
public struct Genre : IGenre
{
    public string Name { get; set; } = "";

    public Genre(string genre) => Name = genre;

    public override string ToString() => Name;
}
