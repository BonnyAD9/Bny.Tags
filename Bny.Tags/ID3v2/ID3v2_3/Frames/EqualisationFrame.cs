namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class EqualisationFrame : Frame
{
    public byte AdjustmentBits { get; set; }
    public List<EqualisationBand> Bands { get; set; }

    public EqualisationFrame() : base()
    {
        AdjustmentBits = 0x10;
        Bands = new();
    }

    internal EqualisationFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        AdjustmentBits = data[0];
        int step = (AdjustmentBits > 255 - 7 ? 32 : (AdjustmentBits + 7) / 8) + 2;
        Bands = new();
        for (int i = 1; i < data.Length; i += step)
            Bands.Add(new(data[i..(i + step)]));
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {Bands.Count} bands",
            "A" => $"{ID.String()}: (Equalisation)\n" +
                   $"  Adjustment Bits: {AdjustmentBits}\n" +
                   $"  Bands: {string.Join(", ", Bands.Select(p => p.ToString()))}",
            _ => throw new FormatException()
        };
    }
}
