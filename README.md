# Bny.Tags
Library that reads tags from files

## Currently supported formats
- ID3v1 and ID3v1.1 (mp3)
- Basic info from ID3v2.3

## Example
```
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
```
### Output
```
Title: Baby It's You
Artist: The Beatles
Album: Please Please Me
Year: 1963
Comment: Their first-ever album, raw and rough and still very rock & roll. Lennon and McCartney begin to flex their writing muscles and had already scored two UK hits when this appeared, but they still relied heavily on the cover material to see them through. Their insecurity about their own abilities seems curious in hindsight since they'd pulled the title song and "I Saw Her Standing There" (with thanks to Little Richard) out of their hats. But they were an unknown quantity, still to launch a million bands and take pop music to places it had never dreamed off. A small step for four men, a giant leap for music. --Chris Nickson
Track: 10
Genre: Rock
```
