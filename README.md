# Bny.Tags
Library that reads tags from files

## Currently supported formats
- ID3v1 and ID3v1.1 (mp3)

## Example
```
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
```
### Output
```
Baby It's You
The Beatles
Please Please Me
1963
Their first-ever album, raw
10
Rock
```
