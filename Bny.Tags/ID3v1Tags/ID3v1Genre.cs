﻿namespace Bny.Tags.ID3v1Tags;

public struct ID3v1Genre : IGenre
{
    public ID3v1GenreEnum Genre { get; set; } = ID3v1GenreEnum.Unset;
    public string Name
    {
        get => Genre.AsString();
        set => Genre = ID3v1GenreExtensions.Parse(value);
    }

    public override string ToString() => Name;
}

public enum ID3v1GenreEnum : byte
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

public static class ID3v1GenreExtensions
{
    public static bool IsValid(this ID3v1GenreEnum genre) => (byte)genre < 192;

    public static string AsString(this ID3v1GenreEnum genre) => genre switch
    {
        ID3v1GenreEnum.Blues => "Blues",
        ID3v1GenreEnum.ClassicRock => "Classic rock",
        ID3v1GenreEnum.Country => "Country",
        ID3v1GenreEnum.Dance => "Dance",
        ID3v1GenreEnum.Disco => "Disco",
        ID3v1GenreEnum.Funk => "Funk",
        ID3v1GenreEnum.Grunge => "Grunge",
        ID3v1GenreEnum.HipHop => "Hip-Hop",
        ID3v1GenreEnum.Jazz => "Jazz",
        ID3v1GenreEnum.Metal => "Metal",
        ID3v1GenreEnum.NewAge => "New Age",
        ID3v1GenreEnum.Oldies => "Oldies",
        ID3v1GenreEnum.Other => "Other",
        ID3v1GenreEnum.Pop => "Pop",
        ID3v1GenreEnum.RhythmAndBlues => "Rhythm and Blues",
        ID3v1GenreEnum.Rap => "Rap",
        ID3v1GenreEnum.Reggae => "Reggae",
        ID3v1GenreEnum.Rock => "Rock",
        ID3v1GenreEnum.Techno => "Techno",
        ID3v1GenreEnum.Industrial => "Industrial",
        ID3v1GenreEnum.Alternative => "Alternative",
        ID3v1GenreEnum.Ska => "Ska",
        ID3v1GenreEnum.DeathMetal => "Death Metal",
        ID3v1GenreEnum.Pranks => "Pranks",
        ID3v1GenreEnum.Soundtrack => "Soundtrack",
        ID3v1GenreEnum.EuroTechno => "Euro-Techno",
        ID3v1GenreEnum.Ambient => "Ambient",
        ID3v1GenreEnum.TripHop => "Trip-Hop",
        ID3v1GenreEnum.Vocal => "Vocal",
        ID3v1GenreEnum.JazzAndFunk => "Jazz & Funk",
        ID3v1GenreEnum.Fusion => "Fusion",
        ID3v1GenreEnum.Trance => "Trance",
        ID3v1GenreEnum.Classical => "Classical",
        ID3v1GenreEnum.Instrumental => "Instrumental",
        ID3v1GenreEnum.Acid => "Acid",
        ID3v1GenreEnum.House => "House",
        ID3v1GenreEnum.Game => "Game",
        ID3v1GenreEnum.SoundClip => "Sound clip",
        ID3v1GenreEnum.Gospel => "Gospel",
        ID3v1GenreEnum.Noise => "Noise",
        ID3v1GenreEnum.AlternativeRock => "Alternative Rock",
        ID3v1GenreEnum.Bass => "Bass",
        ID3v1GenreEnum.Soul => "Soul",
        ID3v1GenreEnum.Punk => "Punk",
        ID3v1GenreEnum.Space => "Space",
        ID3v1GenreEnum.Meditative => "Meditative",
        ID3v1GenreEnum.InstrumentalPop => "Instrumental Pop",
        ID3v1GenreEnum.InstrumentalRock => "Instrumental Rock",
        ID3v1GenreEnum.Ethnic => "Ethnic",
        ID3v1GenreEnum.Gothic => "Gothic",
        ID3v1GenreEnum.Darkwawe => "Darkwawe",
        ID3v1GenreEnum.TechnoIndustrial => "Techno-Industrial",
        ID3v1GenreEnum.Electronic => "Electronic",
        ID3v1GenreEnum.PopFolk => "Pop-Folk",
        ID3v1GenreEnum.Eurodance => "Eurodance",
        ID3v1GenreEnum.Dream => "Dream",
        ID3v1GenreEnum.SouthernRock => "Southern Rock",
        ID3v1GenreEnum.Comedy => "Comedy",
        ID3v1GenreEnum.Cult => "Cult",
        ID3v1GenreEnum.Gangsta => "Gangsta",
        ID3v1GenreEnum.Top40 => "Top 40",
        ID3v1GenreEnum.ChristianRap => "Christian Rap",
        ID3v1GenreEnum.PopFunk => "Pop/Funk",
        ID3v1GenreEnum.Jungle => "Jungle",
        ID3v1GenreEnum.NativeUS => "Native US",
        ID3v1GenreEnum.Cabaret => "Cabaret",
        ID3v1GenreEnum.NewWave => "New Wave",
        ID3v1GenreEnum.Psychedelic => "Psychedelic",
        ID3v1GenreEnum.Rave => "rave",
        ID3v1GenreEnum.ShowTunes => "Show tunes",
        ID3v1GenreEnum.Trailer => "Trailer",
        ID3v1GenreEnum.LoFi => "Lo-Fi",
        ID3v1GenreEnum.Tribal => "Tribal",
        ID3v1GenreEnum.AcidPunk => "Acid Punk",
        ID3v1GenreEnum.AcidJazz => "Acid Jazz",
        ID3v1GenreEnum.Polka => "Polka",
        ID3v1GenreEnum.Retro => "Retro",
        ID3v1GenreEnum.Musical => "Musical",
        ID3v1GenreEnum.RockNRoll => "Rock 'n' Roll",
        ID3v1GenreEnum.HardRock => "Hard Rock",
        //================<<Winamp>>================
        ID3v1GenreEnum.Folk => "Folk",
        ID3v1GenreEnum.FolkRock => "Folk-Rock",
        ID3v1GenreEnum.NationalFolk => "National Folk",
        ID3v1GenreEnum.Swing => "Swing",
        ID3v1GenreEnum.FastFusion => "FastFusion",
        ID3v1GenreEnum.Bebop => "Bebop",
        ID3v1GenreEnum.Latin => "Latin",
        ID3v1GenreEnum.Revival => "Revival",
        ID3v1GenreEnum.Celtic => "Celtic",
        ID3v1GenreEnum.Bluegrass => "Bluegrass",
        ID3v1GenreEnum.Avantgare => "Avantgare",
        ID3v1GenreEnum.GothicRock => "Gothic Rock",
        ID3v1GenreEnum.ProgressiveRock => "Progressive Rock",
        ID3v1GenreEnum.PsychedelicRock => "Psychadelic Rock",
        ID3v1GenreEnum.SymphonicRock => "Symphonic Rock",
        ID3v1GenreEnum.SlowRock => "Slow Rock",
        ID3v1GenreEnum.BigBand => "Big Band",
        ID3v1GenreEnum.Chorus => "Chorus",
        ID3v1GenreEnum.EasyListening => "Easy Listening",
        ID3v1GenreEnum.Acoustic => "Acoustic",
        ID3v1GenreEnum.Humor => "Humor",
        ID3v1GenreEnum.Speech => "Speech",
        ID3v1GenreEnum.Chanson => "Chanson",
        ID3v1GenreEnum.Opera => "Opera",
        ID3v1GenreEnum.ChamberMusic => "Chamber Music",
        ID3v1GenreEnum.Sonata => "Sonata",
        ID3v1GenreEnum.Symphony => "Symphony",
        ID3v1GenreEnum.BootyBass => "Booty bass",
        ID3v1GenreEnum.Primus => "Primus",
        ID3v1GenreEnum.PornGroove => "Porn groove",
        ID3v1GenreEnum.Satire => "Satire",
        ID3v1GenreEnum.SlowJam => "Slow Jam",
        ID3v1GenreEnum.Club => "Club",
        ID3v1GenreEnum.Tango => "Tango",
        ID3v1GenreEnum.Samba => "Samba",
        ID3v1GenreEnum.Folklore => "Folklore",
        ID3v1GenreEnum.Ballad => "Ballad",
        ID3v1GenreEnum.PowerBallad => "Pwoer ballad",
        ID3v1GenreEnum.RhytmicSoul => "Rhytmic Soul",
        ID3v1GenreEnum.Freestyle => "Freestyle",
        ID3v1GenreEnum.Duet => "Duet",
        ID3v1GenreEnum.PunkRock => "Punk Rock",
        ID3v1GenreEnum.DrumSolo => "Drum Solo",
        ID3v1GenreEnum.ACappella => "A Cappella",
        ID3v1GenreEnum.EuroHouse => "Euro-House",
        ID3v1GenreEnum.Dancehall => "Dancehall",
        ID3v1GenreEnum.Goa => "Goa",
        ID3v1GenreEnum.DrumAndBass => "Drum & Bass",
        ID3v1GenreEnum.ClubHouse => "Club-House",
        ID3v1GenreEnum.HarcoreTechno => "Hardcore Techno",
        ID3v1GenreEnum.Terror => "Terror",
        ID3v1GenreEnum.Indie => "Indie",
        ID3v1GenreEnum.BritPop => "BritPop",
        ID3v1GenreEnum.Negerpunk => "Negerpunk",
        ID3v1GenreEnum.PolskPunk => "Polsk Punk",
        ID3v1GenreEnum.Beat => "Beat",
        ID3v1GenreEnum.ChristianGangstaRap => "Christian Gangsta Rap",
        ID3v1GenreEnum.HeavyMetal => "Heavy Metal",
        ID3v1GenreEnum.BlackMetal => "Black Metal",
        ID3v1GenreEnum.Crossover => "Crossover",
        ID3v1GenreEnum.ContemporaryChristian => "Contemporary Christian",
        ID3v1GenreEnum.ChristianRock => "Christian Rock",
        //=============<<Winamp  1.91>>=============
        ID3v1GenreEnum.Merengue => "Merengue",
        ID3v1GenreEnum.Salsa => "Salsa",
        ID3v1GenreEnum.ThrashMetal => "Thrash metal",
        ID3v1GenreEnum.Anime => "Anime",
        ID3v1GenreEnum.Jpop => "Jpop",
        ID3v1GenreEnum.Synthpop => "Synthpop",
        //==============<<Winamp 5.6>>==============
        ID3v1GenreEnum.Abstract => "Abstract",
        ID3v1GenreEnum.ArtRock => "Art Rock",
        ID3v1GenreEnum.Baroque => "Baroque",
        ID3v1GenreEnum.Bhangra => "Bhangra",
        ID3v1GenreEnum.BigBeat => "Big beat",
        ID3v1GenreEnum.Breakbeat => "Breakbeat",
        ID3v1GenreEnum.Chillout => "Chillout",
        ID3v1GenreEnum.Downtempo => "Downtempo",
        ID3v1GenreEnum.Dub => "Dub",
        ID3v1GenreEnum.EBM => "EBM",
        ID3v1GenreEnum.Eclectic => "Eclectic",
        ID3v1GenreEnum.Electro => "Electro",
        ID3v1GenreEnum.Electrodash => "Electrodash",
        ID3v1GenreEnum.Emo => "Emo",
        ID3v1GenreEnum.Experimental => "Experimental",
        ID3v1GenreEnum.Garage => "Garage",
        ID3v1GenreEnum.Global => "Global",
        ID3v1GenreEnum.IDM => "IDM",
        ID3v1GenreEnum.Illbient => "Illbient",
        ID3v1GenreEnum.IndustroGoth => "Industro-Goth",
        ID3v1GenreEnum.JamBand => "Jam Band",
        ID3v1GenreEnum.ArtRock2 => "Art Rock",
        ID3v1GenreEnum.Leftfield => "Leftfield",
        ID3v1GenreEnum.Longue => "Longue",
        ID3v1GenreEnum.MathRock => "Math Rock",
        ID3v1GenreEnum.NewRomantic => "New Romantic",
        ID3v1GenreEnum.NuBreakz => "Nu-Breakz",
        ID3v1GenreEnum.PostPunk => "Post-Punk",
        ID3v1GenreEnum.PostRock => "Post-Rock",
        ID3v1GenreEnum.Psytrance => "Psytrance",
        ID3v1GenreEnum.Shoegaze => "Shoegaze",
        ID3v1GenreEnum.SpaceRock => "Space Rock",
        ID3v1GenreEnum.TropRock => "Trop Rock",
        ID3v1GenreEnum.WorldMusic => "World Music",
        ID3v1GenreEnum.Neoclassical => "Neoclassical",
        ID3v1GenreEnum.Audiobook => "Audiobook",
        ID3v1GenreEnum.AudioTheatre => "Audio Theatre",
        ID3v1GenreEnum.NeueDeutscheWelle => "Neue Detsche Welle",
        ID3v1GenreEnum.Podcast => "Podcast",
        ID3v1GenreEnum.IndieRock => "Indie-Rock",
        ID3v1GenreEnum.GFunk => "G-Funk",
        ID3v1GenreEnum.Dubstep => "Dubstep",
        ID3v1GenreEnum.GarageRock => "Garage Rock",
        ID3v1GenreEnum.Psybient => "Psybient",
        //================<<ID3v2.3>>===============
        ID3v1GenreEnum.Remix => "Remix",
        ID3v1GenreEnum.Cover => "Cover",
        _ => "Unknown",
    };

    public static ID3v1GenreEnum Parse(string str) => str.ToLower().Replace(" ", "") switch
    {
        "blues" => ID3v1GenreEnum.Blues,
        "classicrock" => ID3v1GenreEnum.ClassicRock,
        "country" => ID3v1GenreEnum.Country,
        "dance" => ID3v1GenreEnum.Dance,
        "disco" => ID3v1GenreEnum.Disco,
        "funk" => ID3v1GenreEnum.Funk,
        "grunge" => ID3v1GenreEnum.Grunge,
        "hiphop" or "hip-hop" => ID3v1GenreEnum.HipHop,
        "jazz" => ID3v1GenreEnum.Jazz,
        "metal" => ID3v1GenreEnum.Metal,
        "newage" => ID3v1GenreEnum.NewAge,
        "oldies" => ID3v1GenreEnum.Oldies,
        "other" => ID3v1GenreEnum.Other,
        "pop" => ID3v1GenreEnum.Pop,
        "rhythmandblues" => ID3v1GenreEnum.RhythmAndBlues,
        "rap" => ID3v1GenreEnum.Rap,
        "reggae" => ID3v1GenreEnum.Reggae,
        "rock" => ID3v1GenreEnum.Rock,
        "techno" => ID3v1GenreEnum.Techno,
        "industrial" => ID3v1GenreEnum.Industrial,
        "alternative" => ID3v1GenreEnum.Alternative,
        "ska" => ID3v1GenreEnum.Ska,
        "deathmetal" => ID3v1GenreEnum.DeathMetal,
        "pranks" => ID3v1GenreEnum.Pranks,
        "soundtrack" => ID3v1GenreEnum.Soundtrack,
        "eurotechno" or "euro-techno" => ID3v1GenreEnum.EuroTechno,
        "ambient" => ID3v1GenreEnum.Ambient,
        "triphop" or "trip-hop" => ID3v1GenreEnum.TripHop,
        "vocal" => ID3v1GenreEnum.Vocal,
        "jazzandfunk" or "jazz&funk" => ID3v1GenreEnum.JazzAndFunk,
        "fusion" => ID3v1GenreEnum.Fusion,
        "trance" => ID3v1GenreEnum.Trance,
        "classical" => ID3v1GenreEnum.Classical,
        "instrumental" => ID3v1GenreEnum.Instrumental,
        "acid" => ID3v1GenreEnum.Acid,
        "house" => ID3v1GenreEnum.House,
        "game" => ID3v1GenreEnum.Game,
        "soundclip" => ID3v1GenreEnum.SoundClip,
        "gospel" => ID3v1GenreEnum.Gospel,
        "noise" => ID3v1GenreEnum.Noise,
        "alternativerock" => ID3v1GenreEnum.AlternativeRock,
        "bass" => ID3v1GenreEnum.Bass,
        "soul" => ID3v1GenreEnum.Soul,
        "punk" => ID3v1GenreEnum.Punk,
        "space" => ID3v1GenreEnum.Space,
        "meditative" => ID3v1GenreEnum.Meditative,
        "instrumentalpop" => ID3v1GenreEnum.InstrumentalPop,
        "instrumentalrock" => ID3v1GenreEnum.InstrumentalRock,
        "ethnic" => ID3v1GenreEnum.Ethnic,
        "gothic" => ID3v1GenreEnum.Gothic,
        "darkwawe" => ID3v1GenreEnum.Darkwawe,
        "technoindustrial" or "techno-industrial" => ID3v1GenreEnum.TechnoIndustrial,
        "electronic" => ID3v1GenreEnum.Electronic,
        "popfolk" or "pop-folk" => ID3v1GenreEnum.PopFolk,
        "eurodance" => ID3v1GenreEnum.Eurodance,
        "dream" => ID3v1GenreEnum.Dream,
        "southernrock" => ID3v1GenreEnum.SouthernRock,
        "comedy" => ID3v1GenreEnum.Comedy,
        "cult" => ID3v1GenreEnum.Cult,
        "gangsta" => ID3v1GenreEnum.Gangsta,
        "top40" => ID3v1GenreEnum.Top40,
        "christianrap" => ID3v1GenreEnum.ChristianRap,
        "popfunk" or "pop/funk" => ID3v1GenreEnum.PopFunk,
        "jungle" => ID3v1GenreEnum.Jungle,
        "nativeus" => ID3v1GenreEnum.NativeUS,
        "cabaret" => ID3v1GenreEnum.Cabaret,
        "newwawe" => ID3v1GenreEnum.NewWave,
        "psychedelic" => ID3v1GenreEnum.Psychedelic,
        "rave" => ID3v1GenreEnum.Rave,
        "showtunes" => ID3v1GenreEnum.ShowTunes,
        "trailer" => ID3v1GenreEnum.Trailer,
        "lofi" or "lo-fi" => ID3v1GenreEnum.LoFi,
        "tribal" => ID3v1GenreEnum.Tribal,
        "acidpunk" => ID3v1GenreEnum.AcidPunk,
        "acidjazz" => ID3v1GenreEnum.AcidJazz,
        "polka" => ID3v1GenreEnum.Polka,
        "retro" => ID3v1GenreEnum.Retro,
        "musical" => ID3v1GenreEnum.Musical,
        "rocknroll" or "rock'n'roll" => ID3v1GenreEnum.RockNRoll,
        "hardrock" => ID3v1GenreEnum.HardRock,
        //================<<Winamp>>================
        "folk" => ID3v1GenreEnum.Folk,
        "folkrock" or "folk-rock" => ID3v1GenreEnum.FolkRock,
        "nationalfolk" => ID3v1GenreEnum.NationalFolk,
        "swing" => ID3v1GenreEnum.Swing,
        "fastfusion" => ID3v1GenreEnum.FastFusion,
        "bebop" => ID3v1GenreEnum.Bebop,
        "latin" => ID3v1GenreEnum.Latin,
        "revival" => ID3v1GenreEnum.Revival,
        "celtic" => ID3v1GenreEnum.Celtic,
        "bluegrass" => ID3v1GenreEnum.Bluegrass,
        "avantgare" => ID3v1GenreEnum.Avantgare,
        "gothicrock" => ID3v1GenreEnum.GothicRock,
        "progressiverock" => ID3v1GenreEnum.ProgressiveRock,
        "psychadelicrock" => ID3v1GenreEnum.PsychedelicRock,
        "symphonicrock" => ID3v1GenreEnum.SymphonicRock,
        "slowrock" => ID3v1GenreEnum.SlowRock,
        "bigband" => ID3v1GenreEnum.BigBand,
        "chorus" => ID3v1GenreEnum.Chorus,
        "easylistening" => ID3v1GenreEnum.EasyListening,
        "acoustic" => ID3v1GenreEnum.Acoustic,
        "humor" => ID3v1GenreEnum.Humor,
        "speech" => ID3v1GenreEnum.Speech,
        "chanson" => ID3v1GenreEnum.Chanson,
        "opera" => ID3v1GenreEnum.Opera,
        "chambermusic" => ID3v1GenreEnum.ChamberMusic,
        "sonata" => ID3v1GenreEnum.Sonata,
        "symphony" => ID3v1GenreEnum.Symphony,
        "bootybas" => ID3v1GenreEnum.BootyBass,
        "primus" => ID3v1GenreEnum.Primus,
        "porngroove" => ID3v1GenreEnum.PornGroove,
        "satire" => ID3v1GenreEnum.Satire,
        "slowjam" => ID3v1GenreEnum.SlowJam,
        "club" => ID3v1GenreEnum.Club,
        "tango" => ID3v1GenreEnum.Tango,
        "samba" => ID3v1GenreEnum.Samba,
        "folklore" => ID3v1GenreEnum.Folklore,
        "ballad" => ID3v1GenreEnum.Ballad,
        "powerballad" => ID3v1GenreEnum.PowerBallad,
        "rhytmicsoul" => ID3v1GenreEnum.RhytmicSoul,
        "freestyle" => ID3v1GenreEnum.Freestyle,
        "duet" => ID3v1GenreEnum.Duet,
        "punkrock" => ID3v1GenreEnum.PunkRock,
        "drumsolo" => ID3v1GenreEnum.DrumSolo,
        "acappella" => ID3v1GenreEnum.ACappella,
        "eurohouse" or "euro-house" => ID3v1GenreEnum.EuroHouse,
        "dancehall" => ID3v1GenreEnum.Dancehall,
        "goa" => ID3v1GenreEnum.Goa,
        "drumandbass" or "drum&bass" => ID3v1GenreEnum.DrumAndBass,
        "clubhouse" or "club-house" => ID3v1GenreEnum.ClubHouse,
        "hardcoretechno" => ID3v1GenreEnum.HarcoreTechno,
        "terror" => ID3v1GenreEnum.Terror,
        "indie" => ID3v1GenreEnum.Indie,
        "britpop" => ID3v1GenreEnum.BritPop,
        "negerpunk" => ID3v1GenreEnum.Negerpunk,
        "polskpunk" => ID3v1GenreEnum.PolskPunk,
        "beat" => ID3v1GenreEnum.Beat,
        "christiangangstarap" => ID3v1GenreEnum.ChristianGangstaRap,
        "heavymetal" => ID3v1GenreEnum.HeavyMetal,
        "blackmetal" => ID3v1GenreEnum.BlackMetal,
        "crossover" => ID3v1GenreEnum.Crossover,
        "contemporarychristian" => ID3v1GenreEnum.ContemporaryChristian,
        "christianrock" => ID3v1GenreEnum.ChristianRock,
        //=============<<Winamp  1.91>>=============
        "merengue" => ID3v1GenreEnum.Merengue,
        "salsa" => ID3v1GenreEnum.Salsa,
        "thrashmetal" => ID3v1GenreEnum.ThrashMetal,
        "anime" => ID3v1GenreEnum.Anime,
        "jpop" => ID3v1GenreEnum.Jpop,
        "synthpop" => ID3v1GenreEnum.Synthpop,
        //==============<<Winamp 5.6>>==============
        "abstract" => ID3v1GenreEnum.Abstract,
        "artrock" => ID3v1GenreEnum.ArtRock,
        "baroque" => ID3v1GenreEnum.Baroque,
        "bhangra" => ID3v1GenreEnum.Bhangra,
        "bigbeat" => ID3v1GenreEnum.BigBeat,
        "breakbeat" => ID3v1GenreEnum.Breakbeat,
        "chillout" => ID3v1GenreEnum.Chillout,
        "downtempo" => ID3v1GenreEnum.Downtempo,
        "dub" => ID3v1GenreEnum.Dub,
        "ebm" => ID3v1GenreEnum.EBM,
        "eclectic" => ID3v1GenreEnum.Eclectic,
        "electro" => ID3v1GenreEnum.Electro,
        "electrodash" => ID3v1GenreEnum.Electrodash,
        "emo" => ID3v1GenreEnum.Emo,
        "experimental" => ID3v1GenreEnum.Experimental,
        "garage" => ID3v1GenreEnum.Garage,
        "global" => ID3v1GenreEnum.Global,
        "idm" => ID3v1GenreEnum.IDM,
        "illbient" => ID3v1GenreEnum.Illbient,
        "industrogoth" or "industro-goth" => ID3v1GenreEnum.IndustroGoth,
        "jamband" => ID3v1GenreEnum.JamBand,
        "artrock2" => ID3v1GenreEnum.ArtRock2,
        "leftfield" => ID3v1GenreEnum.Leftfield,
        "longue" => ID3v1GenreEnum.Longue,
        "mathrock" => ID3v1GenreEnum.MathRock,
        "newromantic" => ID3v1GenreEnum.NewRomantic,
        "nubreakz" or "nu-breakz" => ID3v1GenreEnum.NuBreakz,
        "postpunk" or "post-punk" => ID3v1GenreEnum.PostPunk,
        "psytrance" => ID3v1GenreEnum.Psytrance,
        "shoegaze" => ID3v1GenreEnum.Shoegaze,
        "troprock" => ID3v1GenreEnum.TropRock,
        "worldmusic" => ID3v1GenreEnum.WorldMusic,
        "neoclassical" => ID3v1GenreEnum.Neoclassical,
        "audiobook" => ID3v1GenreEnum.Audiobook,
        "audiotheatre" => ID3v1GenreEnum.AudioTheatre,
        "neuedeutschewelle" => ID3v1GenreEnum.NeueDeutscheWelle,
        "podcast" => ID3v1GenreEnum.Podcast,
        "indierock" or "indie-rock" => ID3v1GenreEnum.IndieRock,
        "gfunk" or "g-funk" => ID3v1GenreEnum.GFunk,
        "dubstep" => ID3v1GenreEnum.Dubstep,
        "garagerock" => ID3v1GenreEnum.GarageRock,
        "psybient" => ID3v1GenreEnum.Psybient,
        _ => ID3v1GenreEnum.Unset,
    };
}

