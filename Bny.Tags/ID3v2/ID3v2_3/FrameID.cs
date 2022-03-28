namespace Bny.Tags.ID3v2.ID3v2_3;

[Flags]
public enum FrameID : uint
{
    None = 0,
    Invalid = 0x_58_58_58_58,
    /// <summary>
    /// Experimental X (Flag)
    /// </summary>
    X = 0x_58_00_00_00,
    /// <summary>
    /// Experimental Y (Flag)
    /// </summary>
    Y = 0x_59_00_00_00,
    /// <summary>
    /// Experimental Z (Flag)
    /// </summary>
    Z = 0x_5a_00_00_00,
    /// <summary>
    /// Mask for the first byte (Flag)
    /// </summary>
    Mask1 = 0x_FF_00_00_00,
    /// <summary>
    /// Text frame (Flag)
    /// </summary>
    T = 0x_54_00_00_00,
    /// <summary>
    /// URL link frame (Flag)
    /// </summary>
    W = 0x_57_00_00_00,


    /// <summary>
    /// Audio encryption
    /// </summary>
    AENC = 0x_41_45_4e_43,
    /// <summary>
    /// Attached picture
    /// </summary>
    APIC = 0x_41_50_49_43,
    /// <summary>
    /// Comments
    /// </summary>
    COMM = 0x_43_4f_4d_4d,
    /// <summary>
    /// Commercial frame
    /// </summary>
    COMR = 0x_43_4f_4d_52,
    /// <summary>
    /// Encryption method registration
    /// </summary>
    ENCR = 0x_45_4e_43_52,
    /// <summary>
    /// Equalization
    /// </summary>
    EQUA = 0x_45_51_55_41,
    /// <summary>
    /// Event timing codes
    /// </summary>
    ETCO = 0x_45_54_43_4f,
    /// <summary>
    /// General encapsulated object
    /// </summary>
    GEOB = 0x_47_45_4f_42,
    /// <summary>
    /// Group identification registration
    /// </summary>
    GRID = 0x_47_52_49_44,
    /// <summary>
    /// Involved people list
    /// </summary>
    IPLS = 0x_49_50_4c_53,
    /// <summary>
    /// Linked information
    /// </summary>
    LINK = 0x_4c_49_4e_4b,
    /// <summary>
    /// Music CD identifier
    /// </summary>
    MCDI = 0x_4d_43_44_49,
    /// <summary>
    /// MPEG location lookup table
    /// </summary>
    MLLT = 0x_4d_4c_4c_54,
    /// <summary>
    /// Ownership frame
    /// </summary>
    OWNE = 0x_4f_57_4e_45,
    /// <summary>
    /// Private frame
    /// </summary>
    PRIV = 0x_50_52_49_56,
    /// <summary>
    /// Play counter
    /// </summary>
    PCTN = 0x_50_43_54_4e,
    /// <summary>
    /// Popularimeter
    /// </summary>
    POPM = 0x_50_4f_50_4d,
    /// <summary>
    /// Position synchronisation
    /// </summary>
    POSS = 0x_50_4f_53_53,
    /// <summary>
    /// Recommended buffer size
    /// </summary>
    RBUF = 0x_52_42_55_46,
    /// <summary>
    /// Relative volume adjustment
    /// </summary>
    RVAD = 0x_52_56_41_44,
    /// <summary>
    /// Reverb
    /// </summary>
    RVRB = 0x_52_56_52_42,
    /// <summary>
    /// Synchronized lyric/text
    /// </summary>
    SYLT = 0x_53_59_4c_54,
    /// <summary>
    /// Synchronized tempo codes
    /// </summary>
    SYTC = 0x_53_59_54_43,
    /// <summary>
    /// Album/Movie/Show title
    /// </summary>
    TALB = 0x_54_41_4c_42,
    /// <summary>
    /// BPM (beats per minute)
    /// </summary>
    TBPM = 0x_54_42_50_4d,
    /// <summary>
    /// Composer
    /// </summary>
    TCOM = 0x_54_43_4f_4d,
    /// <summary>
    /// Content type
    /// </summary>
    TCON = 0x_54_43_4f_4e,
    /// <summary>
    /// Copyright message
    /// </summary>
    TCOP = 0x_54_43_4f_50,
    /// <summary>
    /// Date
    /// </summary>
    TDAT = 0x_54_44_41_54,
    /// <summary>
    /// Playlist delay
    /// </summary>
    TDLY = 0x_54_44_4c_59,
    /// <summary>
    /// Encoded by
    /// </summary>
    TENC = 0x_54_45_4e_43,
    /// <summary>
    /// Lyricist/Text writer
    /// </summary>
    TEXT = 0x_54_45_58_54,
    /// <summary>
    /// File type
    /// </summary>
    TFLT = 0x_54_46_4c_54,
    /// <summary>
    /// Time
    /// </summary>
    TIME = 0x_54_49_4d_45,
    /// <summary>
    /// Content group description
    /// </summary>
    TIT1 = 0x_54_49_54_31,
    /// <summary>
    /// Title/songname/content description
    /// </summary>
    TIT2 = 0x_54_49_54_32,
    /// <summary>
    /// Subtitle/Description refinement
    /// </summary>
    TIT3 = 0x_54_49_54_33,
    /// <summary>
    /// Initial key
    /// </summary>
    TKEY = 0x_54_4b_45_59,
    /// <summary>
    /// Language(s)
    /// </summary>
    TLAN = 0x_54_4c_41_4e,
    /// <summary>
    /// Length
    /// </summary>
    TLEN = 0x_54_4c_45_4e,
    /// <summary>
    /// Media type
    /// </summary>
    TMED = 0x_54_4d_45_44,
    /// <summary>
    /// Original album/movie/show title
    /// </summary>
    TOAL = 0x_54_4f_41_4c,
    /// <summary>
    /// Original filename
    /// </summary>
    TOFN = 0x_54_4f_46_4e,
    /// <summary>
    /// Originale lyricist(s)/text writer(s)
    /// </summary>
    TOLY = 0x_54_4f_4c_59,
    /// <summary>
    /// Original artist(s)/performer(s)
    /// </summary>
    TOPE = 0x_54_4f_50_45,
    /// <summary>
    /// Original release year
    /// </summary>
    TORY = 0x_54_4f_52_59,
    /// <summary>
    /// File owner/license
    /// </summary>
    TOWN = 0x_54_4f_57_4e,
    /// <summary>
    /// Lead performer(s)/Solist(s)
    /// </summary>
    TPE1 = 0x_54_50_45_31,
    /// <summary>
    /// Band/orchestra/accompaniment
    /// </summary>
    TPE2 = 0x_54_50_45_32,
    /// <summary>
    /// Conductor/performer refinement
    /// </summary>
    TPE3 = 0x_54_50_45_33,
    /// <summary>
    /// Interpreted, remixed, or otherwise modified by
    /// </summary>
    TPE4 = 0x_54_50_45_34,
    /// <summary>
    /// Part of a set
    /// </summary>
    TPOS = 0x_54_50_4f_53,
    /// <summary>
    /// Publisher
    /// </summary>
    TPUB = 0x_54_50_55_42,
    /// <summary>
    /// Track number/Position in set
    /// </summary>
    TRCK = 0x_54_52_43_4b,
    /// <summary>
    /// Recording dates
    /// </summary>
    TRDA = 0x_54_52_44_41,
    /// <summary>
    /// Internet radio station name
    /// </summary>
    TRSN = 0x_54_52_53_4e,
    /// <summary>
    /// Internet radio station owner
    /// </summary>
    TRSO = 0x_54_52_53_4f,
    /// <summary>
    /// Size
    /// </summary>
    TSIZ = 0x_54_53_49_5a,
    /// <summary>
    /// ISRC (international standard recording code)
    /// </summary>
    TSRC = 0x_54_53_52_43,
    /// <summary>
    /// Software/Hardware and settings used for encoding
    /// </summary>
    TSSE = 0x_54_53_53_45,
    /// <summary>
    /// Year
    /// </summary>
    TYER = 0x_54_59_45_52,
    /// <summary>
    /// User defined text information frame
    /// </summary>
    TXXX = 0x_54_58_58_58,
    /// <summary>
    /// Unique file identifier
    /// </summary>
    UFID = 0x_55_46_49_44,
    /// <summary>
    /// Terms of use
    /// </summary>
    USER = 0x_55_53_45_52,
    /// <summary>
    /// Unsychronized lyric/text transcription
    /// </summary>
    USLT = 0x_55_53_4c_54,
    /// <summary>
    /// Commercial information
    /// </summary>
    WCOM = 0x_57_43_4f_4d,
    /// <summary>
    /// Copyright/Legal information
    /// </summary>
    WCOP = 0x_57_46_4f_50,
    /// <summary>
    /// Official audio file webpage
    /// </summary>
    WOAF = 0x_57_4f_41_46,
    /// <summary>
    /// Official artist/performer webpage
    /// </summary>
    WOAR = 0x_57_4f_41_52,
    /// <summary>
    /// Official audio source webpage
    /// </summary>
    WOAS = 0x_57_4f_41_53,
    /// <summary>
    /// Official internet radio station homepage
    /// </summary>
    WORS = 0x_57_4f_52_53,
    /// <summary>
    /// Payment
    /// </summary>
    WPAY = 0x_57_50_41_59,
    /// <summary>
    /// Publishers official webpage
    /// </summary>
    WPUB = 0x_57_50_55_42,
    /// <summary>
    /// User defined URL link frame
    /// </summary>
    WXXX = 0x_57_58_58_58,
}
