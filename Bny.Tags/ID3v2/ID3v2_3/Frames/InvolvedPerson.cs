namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Contains name of person and their involvement
/// </summary>
public class InvolvedPerson
{
    /// <summary>
    /// Involvement of the Involvee
    /// </summary>
    public string Involvment { get; set; }
    /// <summary>
    /// Involved person
    /// </summary>
    public string Involvee { get; set; }

    /// <summary>
    /// Creates empty person
    /// </summary>
    public InvolvedPerson()
    {
        Involvment = "";
        Involvee = "";
    }

    /// <summary>
    /// Initializes person from binary data
    /// </summary>
    /// <param name="data">Binary data of the person</param>
    /// <param name="encoding">Encoding of the strings in the binary data</param>
    /// <param name="pos">How many bytes were read</param>
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
