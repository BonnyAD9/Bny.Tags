namespace Bny.Tags.ID3v2.ID3v2_3;

/// <summary>
/// ID3v2.3 Frame
/// </summary>
public interface IFrame
{
    /// <summary>
    /// ID of the frame
    /// </summary>
    public FrameID ID { get; }
    /// <summary>
    /// Frame header
    /// </summary>
    internal FrameHeader Header { get; }
    /// <summary>
    /// Returns the frame represented as string based on the given format
    /// 
    /// "G" - General: ID
    /// "C" - Consise: ID: (Most important data)
    /// "A" - All:     ID: (Frame name)\n  Field1Name: Field1Value\n  Field2Name...
    /// </summary>
    /// <param name="fmt">Format of the data</param>
    /// <returns>String representation of the frame</returns>
    public string ToString(string? fmt);
}
