using System.Reflection;
using System.Text;

namespace Bny.Tags.ID3v2.ID3v2_3;

/// <summary>
/// ID3v2.3 Frame
/// </summary>
public abstract class Frame
{
    /// <summary>
    /// Frame header
    /// </summary>
    internal FrameHeader Header { get; set; }
    /// <summary>
    /// ID of the frame
    /// </summary>
    public FrameID ID => Header.ID;

    /// <summary>
    /// Creates default frame with default header
    /// </summary>
    public Frame()
    {
        Header = default;
    }

    /// <summary>
    /// Craetes frame with the given header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    internal Frame(FrameHeader header)
    {
        Header = header;
    }

    public override string ToString() => ID.String();

    /// <summary>
    /// Returns the frame represented as string based on the given format
    /// 
    /// "G" - General: ID
    /// "C" - Consise: ID: (Most important data)
    /// "A" - All:     ID: (Frame name)\n  Field1Name: Field1Value\n  Field2Name...
    /// </summary>
    /// <param name="fmt">Format of the data</param>
    /// <returns>String representation of the frame</returns>
    public virtual string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            return ToString("G");
        
        var obj = GetType();

        switch (fmt)
        {
            case "G":
                return ID.String();
            case "C":
                return $"{ID.String()}: {obj.Name}";
            case "A":
                {
                    StringBuilder sb = new();
                    sb.Append(ID.String()).Append('(').Append(obj.Name).Append(')');
                    foreach (var p in obj.GetProperties(BindingFlags.Public))
                        sb.Append("\n  ").Append(p.Name).Append(": ").Append(p.GetValue(this));
                    return sb.ToString();
                }
            default:
                throw new FormatException();
        }
    }
}
