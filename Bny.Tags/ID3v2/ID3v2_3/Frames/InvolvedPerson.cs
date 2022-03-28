namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class InvolvedPerson
{
    public string Involvment { get; set; }
    public string Involvee { get; set; }

    public InvolvedPerson()
    {
        Involvment = "";
        Involvee = "";
    }

    internal InvolvedPerson(ReadOnlySpan<byte> data, Encoding encoding, out int pos)
    {
        pos = 0;
        Involvment = data.ToID3v2_3String(encoding, ref pos);
        Involvee = data[pos..].ToID3v2_3String(encoding, ref pos);
    }

    public override string ToString()
    {
        return $"{Involvment}: {Involvee}";
    }
}
