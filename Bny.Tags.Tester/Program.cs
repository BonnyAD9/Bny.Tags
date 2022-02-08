using Bny.Tags;

const string file = "The Beatles - Please Please Me - 10 Baby It's You.mp3";

//Console.WriteLine(BitConverter.IsLittleEndian);

Tag t = new();
Console.WriteLine(ID3v2.Read(t, file));

Console.WriteLine(t.Title);
/*Console.WriteLine(t.Artist);
Console.WriteLine(t.Album);
Console.WriteLine(t.Year);
Console.WriteLine(t.Comment);
Console.WriteLine(t.Track);
Console.WriteLine(t.Genre);*/
