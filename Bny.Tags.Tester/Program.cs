using Bny.Tags;

const string file = "The Beatles - Please Please Me - 10 Baby It's You.mp3";

if (!Tag.TryFromFile(file, out Tag t))
{
    Console.WriteLine($"Failed to read data from file '{file}'");
    return;
}

Console.WriteLine(t.Title);
Console.WriteLine(t.Artist);
Console.WriteLine(t.Album);
Console.WriteLine(t.Year);
Console.WriteLine(t.Comment);
Console.WriteLine(t.Track);
Console.WriteLine(t.Genre);
