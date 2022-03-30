using Bny.Tags;
using Bny.Tags.ID3v2.ID3v2_3;
using Bny.Tags.ID3v2.ID3v2_3.Frames;

const string file = @"The Beatles - Please Please Me - 10 Baby It's You.mp3";

ID3v2_3Tag tag = new();
var err = tag.Read(file);
if (err != TagError.None)
    Console.WriteLine($"Error: {err}");

Console.WriteLine($"Title: {tag.GetFrame<TextFrame>(FrameID.TIT2)!.Information}");
Console.WriteLine($"Author: {tag.GetFrame<TextFrame>(FrameID.TPE1)!.Information}");