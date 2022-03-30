namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Describes the format if time stamps (ID3v2.3)
/// </summary>
public enum TimeStampFormat : byte
{
    /// <summary>
    /// Absolute time in MPEG frames
    /// </summary>
    MPEGFrames = 0x01,
    /// <summary>
    /// Absolute time in milliseconds
    /// </summary>
    Milliseconds = 0x02
}