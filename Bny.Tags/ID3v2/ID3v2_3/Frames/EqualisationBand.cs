using System.Numerics;

namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public struct EqualisationBand
{
    public bool Increment { get; set; }
    public short Frequency { get; set; }
    public BigInteger Adjustment { get; set; }

    public EqualisationBand()
    {
        Increment = false;
        Frequency = 0;
        Adjustment = BigInteger.Zero;
    }

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
