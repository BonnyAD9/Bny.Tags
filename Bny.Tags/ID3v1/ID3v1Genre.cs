namespace Bny.Tags.ID3v1;

/// <summary>
/// Enumeration containing all officially supported genres in ID3v1 and genres that added winamp
/// </summary>
public enum ID3v1Genre : byte
{
    Unset = 255,
    Blues = 0,
    ClassicRock = 1,
    Country = 2,
    Dance = 3,
    Disco = 4,
    Funk = 5,
    Grunge = 6,
    HipHop = 7,
    Jazz = 8,
    Metal = 9,
    NewAge = 10,
    Oldies = 11,
    Other = 12,
    Pop = 13,
    RhythmAndBlues = 14,
    Rap = 15,
    Reggae = 16,
    Rock = 17,
    Techno = 18,
    Industrial = 19,
    Alternative = 20,
    Ska = 21,
    DeathMetal = 22,
    Pranks = 23,
    Soundtrack = 24,
    EuroTechno = 25,
    Ambient = 26,
    TripHop = 27,
    Vocal = 28,
    JazzAndFunk = 29,
    Fusion = 30,
    Trance = 31,
    Classical = 32,
    Instrumental = 33,
    Acid = 34,
    House = 35,
    Game = 36,
    SoundClip = 37,
    Gospel = 38,
    Noise = 39,
    AlternativeRock = 40,
    Bass = 41,
    Soul = 42,
    Punk = 43,
    Space = 44,
    Meditative = 45,
    InstrumentalPop = 46,
    InstrumentalRock = 47,
    Ethnic = 48,
    Gothic = 49,
    Darkwawe = 50,
    TechnoIndustrial = 51,
    Electronic = 52,
    PopFolk = 53,
    Eurodance = 54,
    Dream = 55,
    SouthernRock = 56,
    Comedy = 57,
    Cult = 58,
    Gangsta = 59,
    Top40 = 60,
    ChristianRap = 61,
    PopFunk = 62,
    Jungle = 63,
    NativeUS = 64,
    Cabaret = 65,
    NewWave = 66,
    Psychedelic = 67,
    Rave = 68,
    ShowTunes = 69,
    Trailer = 70,
    LoFi = 71,
    Tribal = 72,
    AcidPunk = 73,
    AcidJazz = 74,
    Polka = 75,
    Retro = 76,
    Musical = 77,
    RockNRoll = 78,
    HardRock = 79,
    //================<<Winamp>>================
    Folk = 80,
    FolkRock = 81,
    NationalFolk = 82,
    Swing = 83,
    FastFusion = 84,
    Bebop = 85,
    Latin = 86,
    Revival = 87,
    Celtic = 88,
    Bluegrass = 89,
    Avantgare = 90,
    GothicRock = 91,
    ProgressiveRock = 92,
    PsychedelicRock = 93,
    SymphonicRock = 94,
    SlowRock = 95,
    BigBand = 96,
    Chorus = 97,
    EasyListening = 98,
    Acoustic = 99,
    Humor = 100,
    Speech = 101,
    Chanson = 102,
    Opera = 103,
    ChamberMusic = 104,
    Sonata = 105,
    Symphony = 106,
    BootyBass = 107,
    Primus = 108,
    PornGroove = 109,
    Satire = 110,
    SlowJam = 111,
    Club = 112,
    Tango = 113,
    Samba = 114,
    Folklore = 115,
    Ballad = 116,
    PowerBallad = 117,
    RhytmicSoul = 118,
    Freestyle = 119,
    Duet = 120,
    PunkRock = 121,
    DrumSolo = 122,
    ACappella = 123,
    EuroHouse = 124,
    Dancehall = 125,
    Goa = 126,
    DrumAndBass = 127,
    ClubHouse = 128,
    HarcoreTechno = 129,
    Terror = 130,
    Indie = 131,
    BritPop = 132,
    Negerpunk = 133,
    PolskPunk = 134,
    Beat = 135,
    ChristianGangstaRap = 136,
    HeavyMetal = 137,
    BlackMetal = 138,
    Crossover = 139,
    ContemporaryChristian = 140,
    ChristianRock = 141,
    //=============<<Winamp  1.91>>=============
    Merengue = 142,
    Salsa = 143,
    ThrashMetal = 144,
    Anime = 145,
    Jpop = 146,
    Synthpop = 147,
    //==============<<Winamp 5.6>>==============
    Abstract = 148,
    ArtRock = 149,
    Baroque = 150,
    Bhangra = 151,
    BigBeat = 152,
    Breakbeat = 153,
    Chillout = 154,
    Downtempo = 155,
    Dub = 156,
    EBM = 157,
    Eclectic = 158,
    Electro = 159,
    Electrodash = 160,
    Emo = 161,
    Experimental = 162,
    Garage = 163,
    Global = 164,
    IDM = 165,
    Illbient = 166,
    IndustroGoth = 167,
    JamBand = 168,
    ArtRock2 = 169,
    Leftfield = 170,
    Longue = 171,
    MathRock = 172,
    NewRomantic = 173,
    NuBreakz = 174,
    PostPunk = 175,
    PostRock = 176,
    Psytrance = 177,
    Shoegaze = 178,
    SpaceRock = 179,
    TropRock = 180,
    WorldMusic = 181,
    Neoclassical = 182,
    Audiobook = 183,
    AudioTheatre = 184,
    NeueDeutscheWelle = 185,
    Podcast = 186,
    IndieRock = 187,
    GFunk = 188,
    Dubstep = 189,
    GarageRock = 190,
    Psybient = 191,
    //================<<ID3v2.3>>===============
    Remix = 253,
    Cover = 254,
}

/// <summary>
/// Helpful extensions for ID3v1Genre
/// </summary>
public static class ID3v1GenreExtensions
{
    /// <summary>
    /// Checks whether the genre has valid value
    /// </summary>
    /// <param name="genre">Genre to check</param>
    /// <returns>True if the genre has valid value, false if not</returns>
    public static bool IsValid(this ID3v1Genre genre) => (byte)genre < 192;

    /// <summary>
    /// Converts the given genre to proper string
    /// 
    /// This will have better results for displaying the value than the default ToString method
    /// </summary>
    /// <param name="genre">Genre to convert</param>
    /// <returns>String representing the genre</returns>
    public static string AsString(this ID3v1Genre genre) => genre switch
    {
        ID3v1Genre.Blues => "Blues",
        ID3v1Genre.ClassicRock => "Classic rock",
        ID3v1Genre.Country => "Country",
        ID3v1Genre.Dance => "Dance",
        ID3v1Genre.Disco => "Disco",
        ID3v1Genre.Funk => "Funk",
        ID3v1Genre.Grunge => "Grunge",
        ID3v1Genre.HipHop => "Hip-Hop",
        ID3v1Genre.Jazz => "Jazz",
        ID3v1Genre.Metal => "Metal",
        ID3v1Genre.NewAge => "New Age",
        ID3v1Genre.Oldies => "Oldies",
        ID3v1Genre.Other => "Other",
        ID3v1Genre.Pop => "Pop",
        ID3v1Genre.RhythmAndBlues => "Rhythm and Blues",
        ID3v1Genre.Rap => "Rap",
        ID3v1Genre.Reggae => "Reggae",
        ID3v1Genre.Rock => "Rock",
        ID3v1Genre.Techno => "Techno",
        ID3v1Genre.Industrial => "Industrial",
        ID3v1Genre.Alternative => "Alternative",
        ID3v1Genre.Ska => "Ska",
        ID3v1Genre.DeathMetal => "Death Metal",
        ID3v1Genre.Pranks => "Pranks",
        ID3v1Genre.Soundtrack => "Soundtrack",
        ID3v1Genre.EuroTechno => "Euro-Techno",
        ID3v1Genre.Ambient => "Ambient",
        ID3v1Genre.TripHop => "Trip-Hop",
        ID3v1Genre.Vocal => "Vocal",
        ID3v1Genre.JazzAndFunk => "Jazz & Funk",
        ID3v1Genre.Fusion => "Fusion",
        ID3v1Genre.Trance => "Trance",
        ID3v1Genre.Classical => "Classical",
        ID3v1Genre.Instrumental => "Instrumental",
        ID3v1Genre.Acid => "Acid",
        ID3v1Genre.House => "House",
        ID3v1Genre.Game => "Game",
        ID3v1Genre.SoundClip => "Sound clip",
        ID3v1Genre.Gospel => "Gospel",
        ID3v1Genre.Noise => "Noise",
        ID3v1Genre.AlternativeRock => "Alternative Rock",
        ID3v1Genre.Bass => "Bass",
        ID3v1Genre.Soul => "Soul",
        ID3v1Genre.Punk => "Punk",
        ID3v1Genre.Space => "Space",
        ID3v1Genre.Meditative => "Meditative",
        ID3v1Genre.InstrumentalPop => "Instrumental Pop",
        ID3v1Genre.InstrumentalRock => "Instrumental Rock",
        ID3v1Genre.Ethnic => "Ethnic",
        ID3v1Genre.Gothic => "Gothic",
        ID3v1Genre.Darkwawe => "Darkwawe",
        ID3v1Genre.TechnoIndustrial => "Techno-Industrial",
        ID3v1Genre.Electronic => "Electronic",
        ID3v1Genre.PopFolk => "Pop-Folk",
        ID3v1Genre.Eurodance => "Eurodance",
        ID3v1Genre.Dream => "Dream",
        ID3v1Genre.SouthernRock => "Southern Rock",
        ID3v1Genre.Comedy => "Comedy",
        ID3v1Genre.Cult => "Cult",
        ID3v1Genre.Gangsta => "Gangsta",
        ID3v1Genre.Top40 => "Top 40",
        ID3v1Genre.ChristianRap => "Christian Rap",
        ID3v1Genre.PopFunk => "Pop/Funk",
        ID3v1Genre.Jungle => "Jungle",
        ID3v1Genre.NativeUS => "Native US",
        ID3v1Genre.Cabaret => "Cabaret",
        ID3v1Genre.NewWave => "New Wave",
        ID3v1Genre.Psychedelic => "Psychedelic",
        ID3v1Genre.Rave => "rave",
        ID3v1Genre.ShowTunes => "Show tunes",
        ID3v1Genre.Trailer => "Trailer",
        ID3v1Genre.LoFi => "Lo-Fi",
        ID3v1Genre.Tribal => "Tribal",
        ID3v1Genre.AcidPunk => "Acid Punk",
        ID3v1Genre.AcidJazz => "Acid Jazz",
        ID3v1Genre.Polka => "Polka",
        ID3v1Genre.Retro => "Retro",
        ID3v1Genre.Musical => "Musical",
        ID3v1Genre.RockNRoll => "Rock 'n' Roll",
        ID3v1Genre.HardRock => "Hard Rock",
        //================<<Winamp>>================
        ID3v1Genre.Folk => "Folk",
        ID3v1Genre.FolkRock => "Folk-Rock",
        ID3v1Genre.NationalFolk => "National Folk",
        ID3v1Genre.Swing => "Swing",
        ID3v1Genre.FastFusion => "FastFusion",
        ID3v1Genre.Bebop => "Bebop",
        ID3v1Genre.Latin => "Latin",
        ID3v1Genre.Revival => "Revival",
        ID3v1Genre.Celtic => "Celtic",
        ID3v1Genre.Bluegrass => "Bluegrass",
        ID3v1Genre.Avantgare => "Avantgare",
        ID3v1Genre.GothicRock => "Gothic Rock",
        ID3v1Genre.ProgressiveRock => "Progressive Rock",
        ID3v1Genre.PsychedelicRock => "Psychadelic Rock",
        ID3v1Genre.SymphonicRock => "Symphonic Rock",
        ID3v1Genre.SlowRock => "Slow Rock",
        ID3v1Genre.BigBand => "Big Band",
        ID3v1Genre.Chorus => "Chorus",
        ID3v1Genre.EasyListening => "Easy Listening",
        ID3v1Genre.Acoustic => "Acoustic",
        ID3v1Genre.Humor => "Humor",
        ID3v1Genre.Speech => "Speech",
        ID3v1Genre.Chanson => "Chanson",
        ID3v1Genre.Opera => "Opera",
        ID3v1Genre.ChamberMusic => "Chamber Music",
        ID3v1Genre.Sonata => "Sonata",
        ID3v1Genre.Symphony => "Symphony",
        ID3v1Genre.BootyBass => "Booty bass",
        ID3v1Genre.Primus => "Primus",
        ID3v1Genre.PornGroove => "Porn groove",
        ID3v1Genre.Satire => "Satire",
        ID3v1Genre.SlowJam => "Slow Jam",
        ID3v1Genre.Club => "Club",
        ID3v1Genre.Tango => "Tango",
        ID3v1Genre.Samba => "Samba",
        ID3v1Genre.Folklore => "Folklore",
        ID3v1Genre.Ballad => "Ballad",
        ID3v1Genre.PowerBallad => "Pwoer ballad",
        ID3v1Genre.RhytmicSoul => "Rhytmic Soul",
        ID3v1Genre.Freestyle => "Freestyle",
        ID3v1Genre.Duet => "Duet",
        ID3v1Genre.PunkRock => "Punk Rock",
        ID3v1Genre.DrumSolo => "Drum Solo",
        ID3v1Genre.ACappella => "A Cappella",
        ID3v1Genre.EuroHouse => "Euro-House",
        ID3v1Genre.Dancehall => "Dancehall",
        ID3v1Genre.Goa => "Goa",
        ID3v1Genre.DrumAndBass => "Drum & Bass",
        ID3v1Genre.ClubHouse => "Club-House",
        ID3v1Genre.HarcoreTechno => "Hardcore Techno",
        ID3v1Genre.Terror => "Terror",
        ID3v1Genre.Indie => "Indie",
        ID3v1Genre.BritPop => "BritPop",
        ID3v1Genre.Negerpunk => "Negerpunk",
        ID3v1Genre.PolskPunk => "Polsk Punk",
        ID3v1Genre.Beat => "Beat",
        ID3v1Genre.ChristianGangstaRap => "Christian Gangsta Rap",
        ID3v1Genre.HeavyMetal => "Heavy Metal",
        ID3v1Genre.BlackMetal => "Black Metal",
        ID3v1Genre.Crossover => "Crossover",
        ID3v1Genre.ContemporaryChristian => "Contemporary Christian",
        ID3v1Genre.ChristianRock => "Christian Rock",
        //=============<<Winamp  1.91>>=============
        ID3v1Genre.Merengue => "Merengue",
        ID3v1Genre.Salsa => "Salsa",
        ID3v1Genre.ThrashMetal => "Thrash metal",
        ID3v1Genre.Anime => "Anime",
        ID3v1Genre.Jpop => "Jpop",
        ID3v1Genre.Synthpop => "Synthpop",
        //==============<<Winamp 5.6>>==============
        ID3v1Genre.Abstract => "Abstract",
        ID3v1Genre.ArtRock => "Art Rock",
        ID3v1Genre.Baroque => "Baroque",
        ID3v1Genre.Bhangra => "Bhangra",
        ID3v1Genre.BigBeat => "Big beat",
        ID3v1Genre.Breakbeat => "Breakbeat",
        ID3v1Genre.Chillout => "Chillout",
        ID3v1Genre.Downtempo => "Downtempo",
        ID3v1Genre.Dub => "Dub",
        ID3v1Genre.EBM => "EBM",
        ID3v1Genre.Eclectic => "Eclectic",
        ID3v1Genre.Electro => "Electro",
        ID3v1Genre.Electrodash => "Electrodash",
        ID3v1Genre.Emo => "Emo",
        ID3v1Genre.Experimental => "Experimental",
        ID3v1Genre.Garage => "Garage",
        ID3v1Genre.Global => "Global",
        ID3v1Genre.IDM => "IDM",
        ID3v1Genre.Illbient => "Illbient",
        ID3v1Genre.IndustroGoth => "Industro-Goth",
        ID3v1Genre.JamBand => "Jam Band",
        ID3v1Genre.ArtRock2 => "Art Rock",
        ID3v1Genre.Leftfield => "Leftfield",
        ID3v1Genre.Longue => "Longue",
        ID3v1Genre.MathRock => "Math Rock",
        ID3v1Genre.NewRomantic => "New Romantic",
        ID3v1Genre.NuBreakz => "Nu-Breakz",
        ID3v1Genre.PostPunk => "Post-Punk",
        ID3v1Genre.PostRock => "Post-Rock",
        ID3v1Genre.Psytrance => "Psytrance",
        ID3v1Genre.Shoegaze => "Shoegaze",
        ID3v1Genre.SpaceRock => "Space Rock",
        ID3v1Genre.TropRock => "Trop Rock",
        ID3v1Genre.WorldMusic => "World Music",
        ID3v1Genre.Neoclassical => "Neoclassical",
        ID3v1Genre.Audiobook => "Audiobook",
        ID3v1Genre.AudioTheatre => "Audio Theatre",
        ID3v1Genre.NeueDeutscheWelle => "Neue Detsche Welle",
        ID3v1Genre.Podcast => "Podcast",
        ID3v1Genre.IndieRock => "Indie-Rock",
        ID3v1Genre.GFunk => "G-Funk",
        ID3v1Genre.Dubstep => "Dubstep",
        ID3v1Genre.GarageRock => "Garage Rock",
        ID3v1Genre.Psybient => "Psybient",
        //================<<ID3v2.3>>===============
        ID3v1Genre.Remix => "Remix",
        ID3v1Genre.Cover => "Cover",
        _ => "Unknown",
    };

    /// <summary>
    /// Converts string created by AsString extension back to ID3v1Genre value
    /// </summary>
    /// <param name="str">String to convert</param>
    /// <returns>Valid enumeration value if the given string could be converted, Unset if not</returns>
    public static ID3v1Genre Parse(string str) => str.ToLower().Replace(" ", "") switch
    {
        "blues" => ID3v1Genre.Blues,
        "classicrock" => ID3v1Genre.ClassicRock,
        "country" => ID3v1Genre.Country,
        "dance" => ID3v1Genre.Dance,
        "disco" => ID3v1Genre.Disco,
        "funk" => ID3v1Genre.Funk,
        "grunge" => ID3v1Genre.Grunge,
        "hiphop" or "hip-hop" => ID3v1Genre.HipHop,
        "jazz" => ID3v1Genre.Jazz,
        "metal" => ID3v1Genre.Metal,
        "newage" => ID3v1Genre.NewAge,
        "oldies" => ID3v1Genre.Oldies,
        "other" => ID3v1Genre.Other,
        "pop" => ID3v1Genre.Pop,
        "rhythmandblues" => ID3v1Genre.RhythmAndBlues,
        "rap" => ID3v1Genre.Rap,
        "reggae" => ID3v1Genre.Reggae,
        "rock" => ID3v1Genre.Rock,
        "techno" => ID3v1Genre.Techno,
        "industrial" => ID3v1Genre.Industrial,
        "alternative" => ID3v1Genre.Alternative,
        "ska" => ID3v1Genre.Ska,
        "deathmetal" => ID3v1Genre.DeathMetal,
        "pranks" => ID3v1Genre.Pranks,
        "soundtrack" => ID3v1Genre.Soundtrack,
        "eurotechno" or "euro-techno" => ID3v1Genre.EuroTechno,
        "ambient" => ID3v1Genre.Ambient,
        "triphop" or "trip-hop" => ID3v1Genre.TripHop,
        "vocal" => ID3v1Genre.Vocal,
        "jazzandfunk" or "jazz&funk" => ID3v1Genre.JazzAndFunk,
        "fusion" => ID3v1Genre.Fusion,
        "trance" => ID3v1Genre.Trance,
        "classical" => ID3v1Genre.Classical,
        "instrumental" => ID3v1Genre.Instrumental,
        "acid" => ID3v1Genre.Acid,
        "house" => ID3v1Genre.House,
        "game" => ID3v1Genre.Game,
        "soundclip" => ID3v1Genre.SoundClip,
        "gospel" => ID3v1Genre.Gospel,
        "noise" => ID3v1Genre.Noise,
        "alternativerock" => ID3v1Genre.AlternativeRock,
        "bass" => ID3v1Genre.Bass,
        "soul" => ID3v1Genre.Soul,
        "punk" => ID3v1Genre.Punk,
        "space" => ID3v1Genre.Space,
        "meditative" => ID3v1Genre.Meditative,
        "instrumentalpop" => ID3v1Genre.InstrumentalPop,
        "instrumentalrock" => ID3v1Genre.InstrumentalRock,
        "ethnic" => ID3v1Genre.Ethnic,
        "gothic" => ID3v1Genre.Gothic,
        "darkwawe" => ID3v1Genre.Darkwawe,
        "technoindustrial" or "techno-industrial" => ID3v1Genre.TechnoIndustrial,
        "electronic" => ID3v1Genre.Electronic,
        "popfolk" or "pop-folk" => ID3v1Genre.PopFolk,
        "eurodance" => ID3v1Genre.Eurodance,
        "dream" => ID3v1Genre.Dream,
        "southernrock" => ID3v1Genre.SouthernRock,
        "comedy" => ID3v1Genre.Comedy,
        "cult" => ID3v1Genre.Cult,
        "gangsta" => ID3v1Genre.Gangsta,
        "top40" => ID3v1Genre.Top40,
        "christianrap" => ID3v1Genre.ChristianRap,
        "popfunk" or "pop/funk" => ID3v1Genre.PopFunk,
        "jungle" => ID3v1Genre.Jungle,
        "nativeus" => ID3v1Genre.NativeUS,
        "cabaret" => ID3v1Genre.Cabaret,
        "newwawe" => ID3v1Genre.NewWave,
        "psychedelic" => ID3v1Genre.Psychedelic,
        "rave" => ID3v1Genre.Rave,
        "showtunes" => ID3v1Genre.ShowTunes,
        "trailer" => ID3v1Genre.Trailer,
        "lofi" or "lo-fi" => ID3v1Genre.LoFi,
        "tribal" => ID3v1Genre.Tribal,
        "acidpunk" => ID3v1Genre.AcidPunk,
        "acidjazz" => ID3v1Genre.AcidJazz,
        "polka" => ID3v1Genre.Polka,
        "retro" => ID3v1Genre.Retro,
        "musical" => ID3v1Genre.Musical,
        "rocknroll" or "rock'n'roll" => ID3v1Genre.RockNRoll,
        "hardrock" => ID3v1Genre.HardRock,
        //================<<Winamp>>================
        "folk" => ID3v1Genre.Folk,
        "folkrock" or "folk-rock" => ID3v1Genre.FolkRock,
        "nationalfolk" => ID3v1Genre.NationalFolk,
        "swing" => ID3v1Genre.Swing,
        "fastfusion" => ID3v1Genre.FastFusion,
        "bebop" => ID3v1Genre.Bebop,
        "latin" => ID3v1Genre.Latin,
        "revival" => ID3v1Genre.Revival,
        "celtic" => ID3v1Genre.Celtic,
        "bluegrass" => ID3v1Genre.Bluegrass,
        "avantgare" => ID3v1Genre.Avantgare,
        "gothicrock" => ID3v1Genre.GothicRock,
        "progressiverock" => ID3v1Genre.ProgressiveRock,
        "psychadelicrock" => ID3v1Genre.PsychedelicRock,
        "symphonicrock" => ID3v1Genre.SymphonicRock,
        "slowrock" => ID3v1Genre.SlowRock,
        "bigband" => ID3v1Genre.BigBand,
        "chorus" => ID3v1Genre.Chorus,
        "easylistening" => ID3v1Genre.EasyListening,
        "acoustic" => ID3v1Genre.Acoustic,
        "humor" => ID3v1Genre.Humor,
        "speech" => ID3v1Genre.Speech,
        "chanson" => ID3v1Genre.Chanson,
        "opera" => ID3v1Genre.Opera,
        "chambermusic" => ID3v1Genre.ChamberMusic,
        "sonata" => ID3v1Genre.Sonata,
        "symphony" => ID3v1Genre.Symphony,
        "bootybas" => ID3v1Genre.BootyBass,
        "primus" => ID3v1Genre.Primus,
        "porngroove" => ID3v1Genre.PornGroove,
        "satire" => ID3v1Genre.Satire,
        "slowjam" => ID3v1Genre.SlowJam,
        "club" => ID3v1Genre.Club,
        "tango" => ID3v1Genre.Tango,
        "samba" => ID3v1Genre.Samba,
        "folklore" => ID3v1Genre.Folklore,
        "ballad" => ID3v1Genre.Ballad,
        "powerballad" => ID3v1Genre.PowerBallad,
        "rhytmicsoul" => ID3v1Genre.RhytmicSoul,
        "freestyle" => ID3v1Genre.Freestyle,
        "duet" => ID3v1Genre.Duet,
        "punkrock" => ID3v1Genre.PunkRock,
        "drumsolo" => ID3v1Genre.DrumSolo,
        "acappella" => ID3v1Genre.ACappella,
        "eurohouse" or "euro-house" => ID3v1Genre.EuroHouse,
        "dancehall" => ID3v1Genre.Dancehall,
        "goa" => ID3v1Genre.Goa,
        "drumandbass" or "drum&bass" => ID3v1Genre.DrumAndBass,
        "clubhouse" or "club-house" => ID3v1Genre.ClubHouse,
        "hardcoretechno" => ID3v1Genre.HarcoreTechno,
        "terror" => ID3v1Genre.Terror,
        "indie" => ID3v1Genre.Indie,
        "britpop" => ID3v1Genre.BritPop,
        "negerpunk" => ID3v1Genre.Negerpunk,
        "polskpunk" => ID3v1Genre.PolskPunk,
        "beat" => ID3v1Genre.Beat,
        "christiangangstarap" => ID3v1Genre.ChristianGangstaRap,
        "heavymetal" => ID3v1Genre.HeavyMetal,
        "blackmetal" => ID3v1Genre.BlackMetal,
        "crossover" => ID3v1Genre.Crossover,
        "contemporarychristian" => ID3v1Genre.ContemporaryChristian,
        "christianrock" => ID3v1Genre.ChristianRock,
        //=============<<Winamp  1.91>>=============
        "merengue" => ID3v1Genre.Merengue,
        "salsa" => ID3v1Genre.Salsa,
        "thrashmetal" => ID3v1Genre.ThrashMetal,
        "anime" => ID3v1Genre.Anime,
        "jpop" => ID3v1Genre.Jpop,
        "synthpop" => ID3v1Genre.Synthpop,
        //==============<<Winamp 5.6>>==============
        "abstract" => ID3v1Genre.Abstract,
        "artrock" => ID3v1Genre.ArtRock,
        "baroque" => ID3v1Genre.Baroque,
        "bhangra" => ID3v1Genre.Bhangra,
        "bigbeat" => ID3v1Genre.BigBeat,
        "breakbeat" => ID3v1Genre.Breakbeat,
        "chillout" => ID3v1Genre.Chillout,
        "downtempo" => ID3v1Genre.Downtempo,
        "dub" => ID3v1Genre.Dub,
        "ebm" => ID3v1Genre.EBM,
        "eclectic" => ID3v1Genre.Eclectic,
        "electro" => ID3v1Genre.Electro,
        "electrodash" => ID3v1Genre.Electrodash,
        "emo" => ID3v1Genre.Emo,
        "experimental" => ID3v1Genre.Experimental,
        "garage" => ID3v1Genre.Garage,
        "global" => ID3v1Genre.Global,
        "idm" => ID3v1Genre.IDM,
        "illbient" => ID3v1Genre.Illbient,
        "industrogoth" or "industro-goth" => ID3v1Genre.IndustroGoth,
        "jamband" => ID3v1Genre.JamBand,
        "artrock2" => ID3v1Genre.ArtRock2,
        "leftfield" => ID3v1Genre.Leftfield,
        "longue" => ID3v1Genre.Longue,
        "mathrock" => ID3v1Genre.MathRock,
        "newromantic" => ID3v1Genre.NewRomantic,
        "nubreakz" or "nu-breakz" => ID3v1Genre.NuBreakz,
        "postpunk" or "post-punk" => ID3v1Genre.PostPunk,
        "psytrance" => ID3v1Genre.Psytrance,
        "shoegaze" => ID3v1Genre.Shoegaze,
        "troprock" => ID3v1Genre.TropRock,
        "worldmusic" => ID3v1Genre.WorldMusic,
        "neoclassical" => ID3v1Genre.Neoclassical,
        "audiobook" => ID3v1Genre.Audiobook,
        "audiotheatre" => ID3v1Genre.AudioTheatre,
        "neuedeutschewelle" => ID3v1Genre.NeueDeutscheWelle,
        "podcast" => ID3v1Genre.Podcast,
        "indierock" or "indie-rock" => ID3v1Genre.IndieRock,
        "gfunk" or "g-funk" => ID3v1Genre.GFunk,
        "dubstep" => ID3v1Genre.Dubstep,
        "garagerock" => ID3v1Genre.GarageRock,
        "psybient" => ID3v1Genre.Psybient,
        _ => ID3v1Genre.Unset,
    };
}


