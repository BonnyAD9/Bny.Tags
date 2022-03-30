namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Tempo code (ID3v2.3)
/// Indicates tempo in a position in a audio file
/// </summary>
public struct TempoCode
{
    /// <summary>
    /// Tempo of the audio
    /// 2..510 indicates tempo
    /// 0 indicates tempo free part
    /// 1 indicates single beat-stroke
    /// > 510 is invalid
    /// </summary>
    public ushort Tempo { get; set; }
    /// <summary>
    /// Position of the tempo
    /// May be in Milliseconds or MPEG frames
    /// </summary>
    public uint TimeStamp { get; set; }

    /// <summary>
    /// Creates empty code
    /// </summary>
    public TempoCode()
    {
        Tempo = 0;
        TimeStamp = 0;
    }

    /// <summary>
    /// Initializes code from binary data
    /// </summary>
    /// <param name="data">Binay data to read from</param>
    /// <param name="pos">number of bytes read</param>
    internal TempoCode(ReadOnlySpan<byte> data, out int pos)
    {
        pos = 1;
        Tempo = data[0];
        if (Tempo == 0xFF)
        {
            Tempo += data[1];
            pos++;
        }
        TimeStamp = data[pos..].ToUInt32();
        pos += 4;
    }

    public override string ToString()
    {
        return $"{TimeStamp}: {Tempo}";
    }
}
