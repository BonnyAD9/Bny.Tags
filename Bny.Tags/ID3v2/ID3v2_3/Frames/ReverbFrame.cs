namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public class ReverbFrame : IFrame
{
    internal FrameHeader Header { get; set; }
    public FrameID ID => Header.ID;
    FrameHeader IFrame.Header => Header;

    public uint ReverbLeft { get; set; }
    public uint ReverbRight { get; set; }
    public byte ReverbBouncesLeft { get; set; }
    public byte ReverbBouncesRight { get; set; }
    public byte ReverbFeedbackLeftLeft { get; set; }
    public byte ReverbFeedbackLeftRight { get; set; }
    public byte ReverbFeedbackRightRight { get; set; }
    public byte ReverbFeedbackRightLeft { get; set; }
    public byte PremixLeftRight { get; set; }
    public byte PremixRightLeft { get; set; }

    public ReverbFrame()
    {
        Header = default;
        ReverbLeft = 0;
        ReverbRight = 0;
        ReverbBouncesLeft = 0;
        ReverbBouncesRight = 0;
        ReverbFeedbackLeftLeft = 0;
        ReverbFeedbackLeftRight = 0;
        ReverbFeedbackRightRight = 0;
        ReverbFeedbackRightLeft = 0;
        PremixLeftRight = 0;
        PremixRightLeft = 0;
    }

    internal ReverbFrame(FrameHeader header, ReadOnlySpan<byte> data)
    {
        Header = header;
        ReverbLeft = data[..2].ToUInt16();
        ReverbRight = data[2..4].ToUInt16();
        ReverbBouncesLeft = data[4];
        ReverbBouncesRight = data[5];
        ReverbFeedbackLeftLeft = data[6];
        ReverbFeedbackLeftRight = data[7];
        ReverbFeedbackRightRight = data[8];
        ReverbFeedbackRightLeft = data[9];
        PremixLeftRight = data[10];
        PremixRightLeft = data[11];
    }

    public override string ToString()
    {
        return ToString("G");
    }

    public string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.ToString(),
            "C" => $"{ID}: {ReverbLeft} : {ReverbRight}",
            "A" => $"{ID}: (Reverb)\n" +
                   $"  Reverb Left: {ReverbLeft}\n" +
                   $"  Reverb Right: {ReverbRight}\n" +
                   $"  Reverb Bounces Left: {ReverbBouncesLeft}\n" +
                   $"  Reverb Bounces Right: {ReverbBouncesRight}\n" +
                   $"  Reverb Feedback Left to Left: {ReverbFeedbackLeftLeft}\n" +
                   $"  Reverb Feedback Left to Right: {ReverbFeedbackLeftRight}\n" +
                   $"  Reverb Feedback Right to Right: {ReverbFeedbackRightRight}\n" +
                   $"  Reverb Feedback Roght to Left: {ReverbFeedbackRightLeft}\n" +
                   $"  Premix Left to Right: {PremixLeftRight}\n" +
                   $"  Premix Right to Left: {PremixRightLeft}",
            _ => throw new FormatException()
        };
    }
}