using Bny.Tags;

const string file = "The Beatles - Please Please Me - 10 Baby It's You.mp3";

Tag t = new();
if (!ID3v1.Read(t, file))
{
    Console.WriteLine($"Failed to read data from file '{file}'");
    return;
}

Console.WriteLine(t.Title);
Console.WriteLine(t.Artist);
Console.WriteLine(t.Album);
Console.WriteLine(t.Year);
Console.WriteLine(t.Comment);
Console.WriteLine(t.TrackNumber);
Console.WriteLine(t.Genre);
