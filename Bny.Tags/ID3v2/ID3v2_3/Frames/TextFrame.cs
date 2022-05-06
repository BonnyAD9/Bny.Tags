using System.Text;
using Bny.Tags.ID3v1;

namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Text information frame (ID3v2.3)
/// </summary>
public class TextFrame : Frame
{
    /// <summary>
    /// Text in the frame
    /// </summary>
    public string Information { get; set; }

    /// <summary>
    /// Creates empty frame
    /// </summary>
    public TextFrame() : base()
    {
        Information = "";
    }

    /// <summary>
    /// Initializes the frame from binary data and header
    /// </summary>
    /// <param name="header">Header of the frame</param>
    /// <param name="data">Binary data of the frame</param>
    internal TextFrame(FrameHeader header, ReadOnlySpan<byte> data) : base(header)
    {
        Information = data.ToID3v2_3VariableEncoding();
    }

    public override string ToString(string? fmt)
    {
        if (string.IsNullOrEmpty(fmt))
            fmt = "G";

        return fmt switch
        {
            "G" => ID.String(),
            "C" => $"{ID.String()}: {Information}",
            "A" => $"{ID.String()}: (Text Frame)\n" +
                   $"  Information: {Information}",
            _ => throw new FormatException()
        };
    }

    /// <summary>
    /// Gets or sets the bpm
    /// </summary>
    public uint BPM
    {
        get => uint.Parse(Information);
        set => Information = value.ToString();
    }

    /// <summary>
    /// Gets or sets the composers
    /// </summary>
    public string[] Composers
    {
        get => Information.Split('/');
        set => Information = string.Join('/', value);
    }

    /// <summary>
    /// Gets and sets the content type (genre)
    /// </summary>
    public string ContentType
    {
        get
        {
            ReadOnlySpan<char> info = Information;
            StringBuilder sb = new(info.Length);

            bool addSpace = false;

            for (int i = 0; i < info.Length; i++)
            {
                if (addSpace)
                    sb.Append(' ');

                switch (info[i])
                {
                    case '(':
                        if (++i >= info.Length || info[i] == '(')
                        {
                            sb.Append('(');
                            break;
                        }

                        int ind = info[i..].IndexOf(')');
                        if (ind < 0)
                        {
                            sb.Append('(');
                            break;
                        }

                        var exm = info[i..(i + ind)];
                        i += ind;

                        if (exm.Equals("RX", StringComparison.Ordinal))
                        {
                            sb.Append("Remix");
                            break;
                        }

                        if (exm.Equals("CR", StringComparison.Ordinal))
                        {
                            sb.Append("Cover");
                            break;
                        }

                        if (!byte.TryParse(exm, out byte val))
                        {
                            sb.Append('(').Append(exm).Append(')');
                            break;
                        }

                        addSpace = true;

                        if (sb.Length > 0)
                            sb.Append(' ');
                        sb.Append(((ID3v1Genre)val).AsString());
                        break;
                    default:
                        sb.Append(info[i]);
                        break;
                }
            }

            return sb.ToString();
        }

        set => Information = value;
    }

    /// <summary>
    /// Gets and sets the copyright message
    /// </summary>
    public string CopyrightMessage
    {
        get => "Copyright © " + Information;
        set
        {
            ReadOnlySpan<char> info = value.StartsWith("Copyright © ") ? value.AsSpan()[12..] : value;
            if (info.Length < 5)
                throw new InvalidDataException("the Copyright Message must always start with year and space");

            if (char.IsDigit(info[0]) &&
                char.IsDigit(info[1]) &&
                char.IsDigit(info[2]) &&
                char.IsDigit(info[3]) &&
                info[4] == ' ')
            {
                Information = info.ToString();
                return;
            }
            throw new InvalidDataException("the Copyright Message must always start with year and space");
        }
    }

    /// <summary>
    /// Gets or sets the date
    /// </summary>
    public DateOnly Date
    {
        get => DateOnly.ParseExact(Information, "DDMM");
        set => Information = value.ToString("DDMM");
    }

    /// <summary>
    /// Gets or sets the lyricists/text writers
    /// </summary>
    public string[] TextWriters
    {
        get => Information.Split('/');
        set => string.Join('/', value);
    }

    /// <summary>
    /// Gets and sets the type of the audio
    /// </summary>
    public FileType FileType
    {
        get => Information switch
        {
            "MPG" => FileType.MPEG,
            "/1" => FileType.MPEGLayer1,
            "/2" => FileType.MPEGLayer2,
            "/3" => FileType.MPEGLayer3,
            "/2.5" => FileType.MPEG2_5,
            "/AAC" => FileType.AAC,
            "VQF" => FileType.VQF,
            "PCM" => FileType.PCM,
            _ => FileType.Other,
        };

        set => Information = value switch
        {
            FileType.MPEG => "MPG",
            FileType.MPEGLayer1 => "/1",
            FileType.MPEGLayer2 => "/2",
            FileType.MPEGLayer3 => "/3",
            FileType.MPEG2_5 => "/2.5",
            FileType.AAC => "/AAC",
            FileType.VQF => "VQF",
            FileType.PCM => "PCM",
            _ => "Other",
        };
    }

    /// <summary>
    /// Gets and sets the time of recording
    /// </summary>
    public TimeOnly Time
    {
        get => TimeOnly.ParseExact(Information, "HHmm");
        set => Information = value.ToString("HHmm");
    }

    /// <summary>
    /// Gets the length of the audio
    /// </summary>
    public TimeSpan Length
    {
        get => TimeSpan.FromMilliseconds(double.Parse(Information));
        set => Information = ((ulong)value.TotalMilliseconds).ToString();
    }
}

/// <summary>
/// Specifies type of audio in this file
/// </summary>
public enum FileType : byte
{
    /// <summary>
    /// Other
    /// </summary>
    Other = 0,
    /// <summary>
    /// MPEG Audio
    /// </summary>
    MPEG,
    /// <summary>
    /// MPEG 1/2 layer I
    /// </summary>
    MPEGLayer1,
    /// <summary>
    /// MPEG 1/2 layer II
    /// </summary>
    MPEGLayer2,
    /// <summary>
    /// MPEG 1/2 layer III
    /// </summary>
    MPEGLayer3,
    /// <summary>
    /// MPEG 2.5
    /// </summary>
    MPEG2_5,
    /// <summary>
    /// Advanced audio compression
    /// </summary>
    AAC,
    /// <summary>
    /// Transform-domain Weighted Interleave Vector Quantization
    /// </summary>
    VQF,
    /// <summary>
    /// Pulse Code Modulated audio
    /// </summary>
    PCM,
}