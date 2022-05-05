# Bny.Tags
Library that reads tags from files

The interface for reading tags isn't very friendly yet (you need to choose from ID3v1 and ID3v2.3 manually, you need to know the ID of the frame you want to get from ID3v2.3)

## Currently supported formats
- ID3v1 and ID3v1.1 (mp3) (reading only)
- ID3v2.3 (mp3) (reading only)

## Example 1
This example shows reading ID3v2.3 tag from a mp3 file and printing all the information.

### Code
```C#
using Bny.Tags.ID3v2.ID3v2_3;

const string file = @"The Beatles - Please Please Me - 10 Baby It's You.mp3";

ID3v2_3Tag tag = new();
Console.WriteLine($"Error: {tag.Read(file)}");

foreach (var t in tag.FramesEnum)
    Console.WriteLine(t.ToString("A"));
```
### Output
```
Error: None
TIT2: (Text Frame)
  Information: Baby It's You
TPE1: (Text Frame)
  Information: The Beatles
TALB: (Text Frame)
  Information: Please Please Me
TYER: (Text Frame)
  Information: 1963
TRCK: (Text Frame)
  Information: 10
TCON: (Text Frame)
  Information: Rock
TPE2: (Text Frame)
  Information: The Beatles
TPUB: (Text Frame)
  Information: Capitol
WCOM: (URL)
  URL: http://www.amazon.com/exec/obidos/redirect?tag=softpointer-20%26link_code=xm2%26camp=2025%26creative=165953%26path=http://www.amazon.com/gp/redirect.html%253fASIN=B000002UA9%2526tag=softpointer-20%2526lcode=xm2%2526cID=2025%2526ccmID=165953%2526location=/o/ASIN/B000002UA9%25253FSubscriptionId=0RXJS26C80QSDEB56CR2
COMM (Comment):
  Language: eng
  Description:
  Text: Their first-ever album, raw and rough and still very rock & roll. Lennon and McCartney begin to flex their writing muscles and had already scored two UK hits when this appeared, but they still relied heavily on the cover material to see them through. Their insecurity about their own abilities seems curious in hindsight since they'd pulled the title song and "I Saw Her Standing There" (with thanks to Little Richard) out of their hats. But they were an unknown quantity, still to launch a million bands and take pop music to places it had never dreamed off. A small step for four men, a giant leap for music. --Chris Nickson
APIC: (Picture)
  MIME Type: image/jpg
  Picture Type: CoverFrom
  Description:
  Picture Data: 32361 B
```

## Example 2
This example shows reading ID3v2.3 tag from a mp3 file and printing only the Title and Author.

### Code
```C#
using Bny.Tags.ID3v2.ID3v2_3;
using Bny.Tags.ID3v2.ID3v2_3.Frames;

const string file = @"The Beatles - Please Please Me - 10 Baby It's You.mp3";

ID3v2_3Tag tag = new();
Console.WriteLine($"Error: {tag.Read(file)}");

Console.WriteLine($"Title: {tag.GetFrame<TextFrame>(FrameID.TIT2)}");
Console.WriteLine($"Author: {tag.GetFrame<TextFrame>(FrameID.TPE1)}");
```

### Output
```
Error: None
Title: Baby It's You
Author: The Beatles
```
