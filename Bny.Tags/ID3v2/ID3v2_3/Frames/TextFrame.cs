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
    public Genre ContentType
    {
        get => new(Information);
        set => Information = value.ToString();
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
        set => Information = string.Join('/', value);
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
    /// Gets and sets the length of the audio
    /// </summary>
    public TimeSpan Length
    {
        get => TimeSpan.FromMilliseconds(double.Parse(Information));
        set => Information = ((ulong)value.TotalMilliseconds).ToString();
    }

    /// <summary>
    /// Gets and sets the media type
    /// </summary>
    public MediaType MediaType
    {
        get => new(Information);
        set => Information = value.ToString();
    }

    /// <summary>
    /// Gets or sets the original text writers/lyricists
    /// </summary>
    public string[] OriginalTextWriters
    {
        get => Information.Split('/');
        set => Information = string.Join('/', value);
    }

    /// <summary>
    /// Gets or sets the original performers
    /// </summary>
    public string[] OriginalPerformers
    {
        get => Information.Split('/');
        set => Information = string.Join('/', value);
    }

    /// <summary>
    /// Gets or sets the original release year
    /// </summary>
    public ushort OriginalReleaseYear
    {
        get => ushort.Parse(Information);
        set => Information = value.ToString();
    }

    /// <summary>
    /// Gets or sets the lead artists
    /// </summary>
    public string[] LeadArtists
    {
        get => Information.Split('/');
        set => Information = string.Join('/', value);
    }

    /// <summary>
    /// Gets the part of set
    /// </summary>
    public Part SetPart
    {
        get => new(Information);
        set => Information = value.ToString();
    }

    /// <summary>
    /// Gets the track number
    /// </summary>
    public Part Track
    {
        get => new(Information);
        set => Information = value.ToString();
    }

    /// <summary>
    /// Gets the size of the file excluding the tag
    /// </summary>
    public ulong Size
    {
        get => ulong.Parse(Information);
        set => Information = value.ToString();
    }

    /// <summary>
    /// Gets or sets the international standard recording code
    /// </summary>
    public string RecordingCode
    {
        get => Information;
        set => Information = value.Length == 12 ? value : throw new ArgumentOutOfRangeException(nameof(value));
    }

    /// <summary>
    /// Gets the year of recording
    /// </summary>
    public ushort Year
    {
        get => ushort.Parse(Information);
        set => Information = value.ToString();
    }
}

/// <summary>
/// Represents the genre
/// </summary>
/// <param name="Genres">List of ID3v1 genres</param>
/// <param name="Refainment">Additional string genre</param>
public record Genre(List<ID3v1Genre> Genres, string Refainment)
{
    /// <summary>
    /// Initializes the content type by parsing string
    /// </summary>
    /// <param name="ct">string to parse</param>
    public Genre(ReadOnlySpan<char> ct) : this(new(), "")
    {
        for (int i = 0; i < ct.Length; i++)
        {
            if (ct[i] != '(')
            {
                Refainment = ct[i..].ToString();
                return;
            }

            if (++i >= ct.Length)
                return;

            if (ct[i] == '(')
            {
                Refainment = ct[i..].ToString();
                return;
            }

            int ind = ct[i..].IndexOf(')');
            if (ind == -1)
            {
                Refainment = ct[(i - 1)..].ToString();
                return;
            }
            ind += i;

            if (ct[i..ind].Equals("RX", StringComparison.Ordinal))
            {
                Genres.Add(ID3v1Genre.Remix);
                i = ind;
                continue;
            }

            if (ct[i..ind].Equals("CR", StringComparison.Ordinal))
            {
                Genres.Add(ID3v1Genre.Cover);
                i = ind;
                continue;
            }

            if (!byte.TryParse(ct[i..ind], out byte b))
            {
                i = ind;
                continue;
            }

            Genres.Add((ID3v1Genre)b);
            i = ind;
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        foreach (var g in Genres)
            sb.Append('(').Append((byte)g).Append(')');
        if (Refainment.StartsWith('('))
            sb.Append('(');
        sb.Append(Refainment);
        return sb.ToString();
    }
}

/// <summary>
/// Represents the media type
/// </summary>
/// <param name="Type">Predefined type of the media</param>
/// <param name="Refainment">refainment of the type</param>
public record MediaType(List<PredefinedMediaType> Types, string Refainment)
{
    /// <summary>
    /// Creates new media type from its string representation
    /// </summary>
    /// <param name="str">representation of this media type</param>
    public MediaType(ReadOnlySpan<char> str) : this(new(), "")
    {
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] != '(')
            {
                Refainment = str[i..].ToString();
                return;
            }

            if (++i >= str.Length)
                return;

            if (str[i] == '(')
            {
                Refainment = str[i..].ToString();
                return;
            }

            int ind = str[i..].IndexOf(')');
            if (ind == -1)
            {
                Refainment = str[(i - 1)..].ToString();
                return;
            }
            ind += i;

            var inner = str[i..ind];

            PredefinedMediaType pmt = PredefinedMediaType.None;

            for (int j = 0; j < inner.Length; j++)
            {
                int jnd = inner[j..].IndexOf('/');
                string inr = jnd == -1 ? inner[j..].ToString() : inner[j..jnd].ToString();
                pmt |= inr switch
                {
                    "DIG" => PredefinedMediaType.Digital,
                    "ANA" => PredefinedMediaType.OtherAnalog,
                    "CD" => PredefinedMediaType.CD,
                    "LD" => PredefinedMediaType.Laserdisc,
                    "TT" => PredefinedMediaType.Turntable,
                    "MD" => PredefinedMediaType.MiniDisk,
                    "DAT" => PredefinedMediaType.DAT,
                    "DCC" => PredefinedMediaType.DCC,
                    "DVD" => PredefinedMediaType.DVD,
                    "TV" => PredefinedMediaType.Television,
                    "VID" => PredefinedMediaType.Video,
                    "RAD" => PredefinedMediaType.Radio,
                    "TEL" => PredefinedMediaType.Telephone,
                    "MC" => PredefinedMediaType.MC,
                    "REE" => PredefinedMediaType.Reel,
                    "A" => PredefinedMediaType.Analog,
                    "WAC" => PredefinedMediaType.WaxCylinder,
                    "8CA" => PredefinedMediaType.TapeCassette,
                    "DD" => PredefinedMediaType.DDD,
                    "AD" => PredefinedMediaType.ADD,
                    "AA" => PredefinedMediaType.AAD,
                    "33" => PredefinedMediaType.Rpm33_33,
                    "45" => PredefinedMediaType.Rpm45,
                    "71" => PredefinedMediaType.Rpm71_29,
                    "76" => PredefinedMediaType.Rpm76_59,
                    "78" => PredefinedMediaType.Rpm78_26,
                    "80" => PredefinedMediaType.Rpm80,
                    "1" => PredefinedMediaType.DATStandard,
                    "2" => PredefinedMediaType.DATMode2,
                    "3" => PredefinedMediaType.DATMode3,
                    "4" => PredefinedMediaType.DATMode4,
                    "5" => PredefinedMediaType.DATMode5,
                    "6" => PredefinedMediaType.DATMode6,
                    "PAL" => PredefinedMediaType.PAL,
                    "NTSC" => PredefinedMediaType.NTSC,
                    "SECAM" => PredefinedMediaType.SECAM,
                    "VHS" => PredefinedMediaType.VHS,
                    "SVHS" => PredefinedMediaType.SVHS,
                    "BETA" => PredefinedMediaType.BETAMAX,
                    "FM" => PredefinedMediaType.FM,
                    "AM" => PredefinedMediaType.AM,
                    "LW" => PredefinedMediaType.LW,
                    "MW" => PredefinedMediaType.MW,
                    "I" => PredefinedMediaType.ISDN,
                    "9" => PredefinedMediaType.Cm9_5,
                    "II" => PredefinedMediaType.Type2,
                    "III" => PredefinedMediaType.Type3,
                    "IV" => PredefinedMediaType.Type4,
                    "19" => PredefinedMediaType.Cm19,
                    "38" => PredefinedMediaType.Cm38,
                    _ => PredefinedMediaType.Error,
                };

                if (jnd == -1)
                    break;
                j = jnd;
            }
            Types.Add(pmt);

            i = ind;
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        foreach (var mt in Types)
        {
            sb.Append('(');
            switch (mt & PredefinedMediaType.MainFlag)
            {
                case PredefinedMediaType.Digital:
                    sb.Append("DIG");
                    if (mt.HasFlag(PredefinedMediaType.Analog))
                        sb.Append("/A");
                    break;
                case PredefinedMediaType.OtherAnalog:
                    sb.Append("WAC");
                    if (mt.HasFlag(PredefinedMediaType.WaxCylinder))
                        sb.Append("/WAC");
                    if (mt.HasFlag(PredefinedMediaType.TapeCassette))
                        sb.Append("/8CA");
                    break;
                case PredefinedMediaType.CD:
                    sb.Append("CD");
                    if (mt.HasFlag(PredefinedMediaType.Analog))
                        sb.Append("/A");
                    if (mt.HasFlag(PredefinedMediaType.DDD))
                        sb.Append("/DD");
                    if (mt.HasFlag(PredefinedMediaType.ADD))
                        sb.Append("/AD");
                    if (mt.HasFlag(PredefinedMediaType.AAD))
                        sb.Append("/AA");
                    break;
                case PredefinedMediaType.Laserdisc:
                    sb.Append("LD");
                    if (mt.HasFlag(PredefinedMediaType.Analog))
                        sb.Append("/A");
                    break;
                case PredefinedMediaType.Turntable:
                    sb.Append("TT");
                    if (mt.HasFlag(PredefinedMediaType.Rpm33_33))
                        sb.Append("/33");
                    if (mt.HasFlag(PredefinedMediaType.Rpm45))
                        sb.Append("/45");
                    if (mt.HasFlag(PredefinedMediaType.Rpm71_29))
                        sb.Append("/71");
                    if (mt.HasFlag(PredefinedMediaType.Rpm76_59))
                        sb.Append("/76");
                    if (mt.HasFlag(PredefinedMediaType.Rpm78_26))
                        sb.Append("/78");
                    if (mt.HasFlag(PredefinedMediaType.Rpm80))
                        sb.Append("/80");
                    break;
                case PredefinedMediaType.MiniDisk:
                    sb.Append("MD");
                    if (mt.HasFlag(PredefinedMediaType.Analog))
                        sb.Append("/A");
                    break;
                case PredefinedMediaType.DAT:
                    sb.Append("DAT");
                    if (mt.HasFlag(PredefinedMediaType.Analog))
                        sb.Append("/A");
                    if (mt.HasFlag(PredefinedMediaType.DATStandard))
                        sb.Append("/1");
                    if (mt.HasFlag(PredefinedMediaType.DATMode2))
                        sb.Append("/2");
                    if (mt.HasFlag(PredefinedMediaType.DATMode3))
                        sb.Append("/3");
                    if (mt.HasFlag(PredefinedMediaType.DATMode4))
                        sb.Append("/4");
                    if (mt.HasFlag(PredefinedMediaType.DATMode5))
                        sb.Append("/5");
                    if (mt.HasFlag(PredefinedMediaType.DATMode6))
                        sb.Append("/6");
                    break;
                case PredefinedMediaType.DCC:
                    sb.Append("DCC");
                    if (mt.HasFlag(PredefinedMediaType.Analog))
                        sb.Append("/A");
                    break;
                case PredefinedMediaType.DVD:
                    sb.Append("DVD");
                    if (mt.HasFlag(PredefinedMediaType.Analog))
                        sb.Append("/A");
                    break;
                case PredefinedMediaType.Television:
                    sb.Append("TV");
                    if (mt.HasFlag(PredefinedMediaType.PAL))
                        sb.Append("/PAL");
                    if (mt.HasFlag(PredefinedMediaType.NTSC))
                        sb.Append("/NTSC");
                    if (mt.HasFlag(PredefinedMediaType.SECAM))
                        sb.Append("/SECAM");
                    break;
                case PredefinedMediaType.Video:
                    sb.Append("VID");
                    if (mt.HasFlag(PredefinedMediaType.PAL))
                        sb.Append("/PAL");
                    if (mt.HasFlag(PredefinedMediaType.NTSC))
                        sb.Append("/NTSC");
                    if (mt.HasFlag(PredefinedMediaType.SECAM))
                        sb.Append("/SECAM");
                    if (mt.HasFlag(PredefinedMediaType.VHS))
                        sb.Append("/VHS");
                    if (mt.HasFlag(PredefinedMediaType.SVHS))
                        sb.Append("/SVHS");
                    if (mt.HasFlag(PredefinedMediaType.BETAMAX))
                        sb.Append("/BETA");
                    break;
                case PredefinedMediaType.Radio:
                    sb.Append("RAD");
                    if (mt.HasFlag(PredefinedMediaType.FM))
                        sb.Append("/FM");
                    if (mt.HasFlag(PredefinedMediaType.AM))
                        sb.Append("/AM");
                    if (mt.HasFlag(PredefinedMediaType.LW))
                        sb.Append("/LW");
                    if (mt.HasFlag(PredefinedMediaType.MW))
                        sb.Append("/MW");
                    break;
                case PredefinedMediaType.Telephone:
                    sb.Append("TEL");
                    if (mt.HasFlag(PredefinedMediaType.ISDN))
                        sb.Append("/I");
                    break;
                case PredefinedMediaType.MC:
                    sb.Append("MC");
                    if (mt.HasFlag(PredefinedMediaType.Cm4_75))
                        sb.Append("/4");
                    if (mt.HasFlag(PredefinedMediaType.Cm9_5))
                        sb.Append("/9");
                    if (mt.HasFlag(PredefinedMediaType.Type1))
                        sb.Append("/I");
                    if (mt.HasFlag(PredefinedMediaType.Type2))
                        sb.Append("/II");
                    if (mt.HasFlag(PredefinedMediaType.Type3))
                        sb.Append("/III");
                    if (mt.HasFlag(PredefinedMediaType.Type4))
                        sb.Append("/IV");
                    break;
                case PredefinedMediaType.Reel:
                    sb.Append("REE");
                    if (mt.HasFlag(PredefinedMediaType.Cm9_5))
                        sb.Append("/9");
                    if (mt.HasFlag(PredefinedMediaType.Cm19))
                        sb.Append("/19");
                    if (mt.HasFlag(PredefinedMediaType.Cm38))
                        sb.Append("/38");
                    if (mt.HasFlag(PredefinedMediaType.Cm76))
                        sb.Append("/76");
                    if (mt.HasFlag(PredefinedMediaType.Type1))
                        sb.Append("/I");
                    if (mt.HasFlag(PredefinedMediaType.Type2))
                        sb.Append("/II");
                    if (mt.HasFlag(PredefinedMediaType.Type3))
                        sb.Append("/III");
                    if (mt.HasFlag(PredefinedMediaType.Type4))
                        sb.Append("/IV");
                    break;
                default:
                    sb.Remove(sb.Length - 1, 1);
                    continue;
            }
            sb.Append(')');
        }
        if (Refainment.StartsWith('('))
            sb.Append('(');
        sb.Append(Refainment);
        return sb.ToString();
    }
}

/// <summary>
/// Represents part of set
/// </summary>
/// <param name="Position">position of the item in set</param>
/// <param name="Of">total number of items</param>
public record Part(uint Position, uint Of)
{
    /// <summary>
    /// Creates Part from its string representation
    /// </summary>
    /// <param name="sp">string with the part representation</param>
    public Part(ReadOnlySpan<char> sp) : this(0, 0)
    {
        int ind = sp.IndexOf('/');
        if (ind == -1)
        {
            Position = uint.Parse(sp);
            return;
        }
        Position = uint.Parse(sp[..ind]);
        Of = uint.Parse(sp[(ind + 1)..]);
    }

    public override string ToString()
    {
        if (Of == 0)
            return Position.ToString();
        StringBuilder sb = new();
        sb.Append(Position.ToString());
        sb.Append('/');
        sb.Append(Of);
        return sb.ToString();
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

/// <summary>
/// Describes the media from which the sound originated
/// </summary>
[Flags]
public enum PredefinedMediaType : ushort
{
    None = 0,
    Error = 0b_1000_0000__0000_0000,

    // Main flags
    /// <summary>
    /// Other digital media
    /// </summary>
    Digital     = 0x1,
    /// <summary>
    /// Other analog media
    /// </summary>
    OtherAnalog = 0x2,
    CD          = 0x3,
    Laserdisc   = 0x4,
    /// <summary>
    /// Turntable records
    /// </summary>
    Turntable   = 0x5,
    MiniDisk    = 0x6,
    DAT         = 0x7,
    DCC         = 0x8,
    DVD         = 0x9,
    Television  = 0xA,
    Video       = 0xB,
    Radio       = 0xC,
    Telephone   = 0xD,
    /// <summary>
    /// MC (normal cassette)
    /// </summary>
    MC          = 0xE,
    Reel        = 0xF,

#pragma warning disable CA1069 // Enums values should not be duplicated
    // Used by multiple
    /// <summary>
    /// Analog transfer from media
    /// </summary>
    Analog = 0b_0001_0000,

    PAL   = 0b_0001_0000,
    NTSC  = 0b_0010_0000,
    SECAM = 0b_0100_0000,

    /// <summary>
    /// Type I cassette (ferric/normal)
    /// </summary>
    Type1 = 0b_0000__0001_0000,
    /// <summary>
    /// Type II cassete (chrome)
    /// </summary>
    Type2 = 0B_0000__0010_0000,
    /// <summary>
    /// Type III cassette (ferric chrome)
    /// </summary>
    Type3 = 0B_0000__0100_0000,
    /// <summary>
    /// Type IV cassette (metal)
    /// </summary>
    Type4 = 0b_0000__1000_0000,
    /// <summary>
    /// 9.5 cm/s
    /// </summary>
    Cm9_5 = 0b_0100__0000_0000,

    // Same ID
    ISDN     = 0b_0000__0001_0000,
    /// <summary>
    /// 76.59 rpm
    /// </summary>
    Rpm76_59 = 0b_0001__0000_0000,
    /// <summary>
    /// 76 cm/s
    /// </summary>
    Cm76     = 0b_0001__0000_0000,
    /// <summary>
    /// Mode 4, 32 kHz/12 bits, 4 channels
    /// </summary>
    DATMode4 = 0b_0010__0000_0000,
    /// <summary>
    /// 4.75 cm/s (normal speed for a two sided cassette)
    /// </summary>
    Cm4_75   = 0b_0010__0000_0000,

    // Other digital media
    // Analog = 0b_0001_0000,

    // Other Analog media
    WaxCylinder  = 0b_0001_0000,
    /// <summary>
    /// 8-track tape cassette
    /// </summary>
    TapeCassette = 0b_0010_0000,

    // CD
    // Analog = 0b_0001__0000,
    DDD       = 0b_0010__0000,
    ADD       = 0b_0100__0000,
    AAD       = 0b_1000__0000,

    // Laserdisc
    // Analog = 0b_0001_0000,

    // Turntable records
    /// <summary>
    /// 33.33 rpm
    /// </summary>
    Rpm33_33    = 0b_0000_0001_0000,
    /// <summary>
    /// 45 rmp
    /// </summary>
    Rpm45       = 0b_0000_0010_0000,
    /// <summary>
    /// 71.29 rpm
    /// </summary>
    Rpm71_29    = 0b_0000_0100_0000,
    /// <summary>
    /// 78.26 rpm
    /// </summary>
    Rpm78_26    = 0b_0000_1000_0000,
    // Rpm76_59 = 0b_0001_0000_0000,
    /// <summary>
    /// 80 rpm
    /// </summary>
    Rpm80       = 0b_0010_0000_0000,

    // MiniDisc
    // Analog = 0b_0001_0000,

    // DAT
    // Analog   = 0b_0000__0001_0000,
    /// <summary>
    /// standard, 48 kHz/16 bits, linear
    /// </summary>
    DATStandard = 0b_0000__0010_0000,
    /// <summary>
    /// mode 2, 32 kHz/16 bits, linear
    /// </summary>
    DATMode2    = 0b_0000__0100_0000,
    /// <summary>
    /// mode 3, 32 kHz/12 bits, nonlinear, low speed
    /// </summary>
    DATMode3    = 0b_0000__1000_0000,
    /// <summary>
    /// mode 5, 44.1 kHz/16 bits, linear
    /// </summary>
    DATMode5    = 0b_0001__0000_0000,
    // DATMode4 = 0b_0010__0000_0000,
    /// <summary>
    /// mode 6, 44.1 kHz/16 bits, 'wide track' play
    /// </summary>
    DATMode6    = 0b_0100__0000_0000,

    // DCC
    // Analog = 0b_0001__0000,

    // DVD
    // Analog = 0b_0001__0000,

    // Television
    // PAL   = 0b_0001_0000,
    // TNSC  = 0b_0010_0000,
    // SECAM = 0b_0100_0000,

    // Video
    // PAL   = 0b_0000__0001_0000,
    // TNSC  = 0b_0000__0010_0000,
    // SECAM = 0b_0000__0100_0000,
    VHS      = 0b_0000__1000_0000,
    /// <summary>
    /// S-VHS
    /// </summary>
    SVHS     = 0b_0001__0000_0000,
    BETAMAX  = 0b_0010__0000_0000,

    // Radio
    FM = 0b_0001__0000,
    AM = 0b_0010__0000,
    LW = 0b_0100__0000,
    MW = 0b_1000__0000,

    // Telephone
    // ISDN = 0b_0001__0000,

    // MC (normal cassette)
    // Type1  = 0b_0000__0001_0000,
    // Type2  = 0b_0000__0010_0000,
    // Type3  = 0b_0000__0100_0000,
    // Type4  = 0b_0000__1000_0000,
    // Cm4_75 = 0b_0010__0000_0000,
    // Cm9_5  = 0b_0100__0000_0000,

    // Reel
    // Type1  = 0b_0000__0001_0000,
    // Type2  = 0b_0000__0010_0000,
    // Type3  = 0b_0000__0100_0000,
    // Type4  = 0b_0000__1000_0000,
    // Cm76   = 0b_0001__0000_0000,
    /// <summary>
    /// 19 cm/s
    /// </summary>
    Cm19      = 0b_0010__0000_0000,
    // Cm9_5  = 0b_0100__0000_0000,
    /// <summary>
    /// 38 cm/s
    /// </summary>
    Cm38      = 0b_1000__0000_0000,

    // Flags
    MainFlag = 0xF,
#pragma warning restore CA1069 // Enums values should not be duplicated
}