namespace Bny.Tags.ID3v2.ID3v2_3;

public interface IFrame
{
    public FrameID ID { get; }
    internal FrameHeader Header { get; }
    public string ToString(string? fmt);
}
