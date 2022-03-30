using System.Numerics;

namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public struct EqualisationBand
{
    /// <summary>
    /// Describes whether te adjustment is incremented or decremeted
    /// </summary>
    public bool Increment { get; set; }
    /// <summary>
    /// Frequency
    /// </summary>
    public short Frequency { get; set; }
    /// <summary>
    /// Volume adjustment of the frequency
    /// </summary>
    public BigInteger Adjustment { get; set; }

    /// <summary>
    /// Creates empty equalization band
    /// </summary>
    public EqualisationBand()
    {
        Increment = false;
        Frequency = 0;
        Adjustment = BigInteger.Zero;
    }

    /// <summary>
    /// Initializes band from binary data
    /// </summary>
    /// <param name="data">Binary data to read from</param>
    internal EqualisationBand(ReadOnlySpan<byte> data)
    {
        Increment = (data[0] & 0b_1000_0000) == 1;
        Frequency = (short)(data[..2].ToInt16() & 0b_0111_1111__1111_1111);
        Adjustment = new(data[2..], true, true);
    }

    public override string ToString()
    {
        return $"[{Frequency}] {(Increment ? '+' : '-')}{Adjustment}";
    }
}
