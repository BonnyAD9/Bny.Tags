namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Reverb (ID3v2.3)
/// Allows to adjust echos of different kinds
/// </summary>
public class ReverbFrame : Frame
{
    /// <summary>
    /// Delay between left bounces in milliseconds
    /// </summary>
    public uint ReverbLeft { get; set; }
    /// <summary>
    /// Delay between right bounces in milliseconds
    /// </summary>
    public uint ReverbRight { get; set; }
    /// <summary>
    /// Number of left bounces to do
    /// 0xFF (255) means infinitely many
    /// </summary>
    public byte ReverbBouncesLeft { get; set; }
    /// <summary>
    /// Number of roght bounces to do
    /// 0xFF (255) means infinitely many
    /// </summary>
    public byte ReverbBouncesRight { get; set; }
    /// <summary>
    /// Reverb feedback, left to left
    /// </summary>
    public byte ReverbFeedbackLeftLeft { get; set; }
    /// <summary>
    /// Reverb feedback, left to right
    /// </summary>
    public byte ReverbFeedbackLeftRight { get; set; }
    /// <summary>
    /// Reverb feedback, right to right
    /// </summary>
    public byte ReverbFeedbackRightRight { get; set; }
    /// <summary>
    /// Reverb feedback, right to left
    /// </summary>
    public byte ReverbFeedbackRightLeft { get; set; }
    /// <summary>
    /// Premix left to right
    /// </summary>
    public byte PremixLeftRight { get; set; }
    /// <summary>
    /// Premix right to left
    /// </summary>
    public byte PremixRightLeft { get; set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public ReverbFrame() : base()
    {
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

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal ReverbFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
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

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {ReverbLeft} : {ReverbRight}",
            "A" => $"{ID.String()}: (Reverb)\n" +
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