using Bny.Tags;

const string file = "The Beatles - Please Please Me - 10 Baby It's You.mp3";

if (!Tag.TryFromFile(file, out Tag t))
{
    Console.WriteLine($"Failed to read from file ${file}");
    return;
}

Console.WriteLine($"Title: {t.Title}");
Console.WriteLine($"Artist: {t.Artist}");
Console.WriteLine($"Album: {t.Album}");
Console.WriteLine($"Year: {t.Year}");
Console.WriteLine($"Comment: {t.Comment}");
Console.WriteLine($"Track: {t.Track}");
Console.WriteLine($"Genre: {t.Genre}");
