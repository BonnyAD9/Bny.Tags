namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

/// <summary>
/// Symbolizes key event in audio file (ID3v2.3)
/// </summary>
public struct EventTime
{
    /// <summary>
    /// Size of EventTime in bytes
    /// </summary>
    public const int size = 5;

    /// <summary>
    /// Type of the event
    /// </summary>
    public EventType Type;
    /// <summary>
    /// Time at which the event occurs
    /// Can be in milliseconds or MPEG frames
    /// </summary>
    public uint TimeStamp;

    /// <summary>
    /// Creates empty Event
    /// </summary>
    public EventTime()
    {
        Type = EventType.Padding;
        TimeStamp = 0;
    }

    /// <summary>
    /// Creates Event from binary data
    /// </summary>
    /// <param name="data">Binary data to read</param>
    public EventTime(ReadOnlySpan<byte> data)
    {
        Type = (EventType)data[0];
        TimeStamp = data[1..].ToUInt32();
    }

    public override string ToString()
    {
        return $"{TimeStamp}: {Type}";
    }
}

/// <summary>
/// Describes type of event
/// </summary>
public enum EventType : byte
{
    /// <summary>
    /// Padding (has no meaning)
    /// </summary>
    Padding = 0x00,
    /// <summary>
    /// End of initial silence
    /// </summary>
    InitialSilenceEnd = 0x01,
    IntroStart = 0x02,
    MainpartStart = 0x03,
    OutroStart = 0x04,
    OutroEnd = 0x05,
    VerseStart = 0x06,
    RefrainStart = 0x07,
    InterludeStart = 0x08,
    ThemeStart = 0x09,
    VariationStart = 0x0A,
    KeyChange = 0x0B,
    TimeChange = 0x0C,
    /// <summary>
    /// Momentary unwanted noise (Snap, Crackle & Pop)
    /// </summary>
    MomentaryUnwantedNoise = 0x0D,
    SustainedNoise = 0x0E,
    SustainedNoiseEnd = 0x0F,
    IntroEnd = 0x10,
    MainpartEnd = 0x11,
    VerseEnd = 0x12,
    RefrainEnd = 0x13,
    ThemeEnd = 0x14,

    /// <summary>
    /// Not predefined (user event) 1/16
    /// </summary>
    Sync0 = 0xE0,
    /// <summary>
    /// Not predefined (user event) 2/16
    /// </summary>
    Sync1 = 0xE1,
    /// <summary>
    /// Not predefined (user event) 3/16
    /// </summary>
    Sync2 = 0xE2,
    /// <summary>
    /// Not predefined (user event) 4/16
    /// </summary>
    Sync3 = 0xE3,
    /// <summary>
    /// Not predefined (user event) 5/16
    /// </summary>
    Sync4 = 0xE4,
    /// <summary>
    /// Not predefined (user event) 6/16
    /// </summary>
    Sync5 = 0xE5,
    /// <summary>
    /// Not predefined (user event) 7/16
    /// </summary>
    Sync6 = 0xE6,
    /// <summary>
    /// Not predefined (user event) 8/16
    /// </summary>
    Sync7 = 0xE7,
    /// <summary>
    /// Not predefined (user event) 9/16
    /// </summary>
    Sync8 = 0xE8,
    /// <summary>
    /// Not predefined (user event) 10/16
    /// </summary>
    Sync9 = 0xE9,
    /// <summary>
    /// Not predefined (user event) 11/16
    /// </summary>
    SyncA = 0xEA,
    /// <summary>
    /// Not predefined (user event) 12/16
    /// </summary>
    SyncB = 0xEB,
    /// <summary>
    /// Not predefined (user event) 13/16
    /// </summary>
    SyncC = 0xEC,
    /// <summary>
    /// Not predefined (user event) 14/16
    /// </summary>
    SyncD = 0xED,
    /// <summary>
    /// Not predefined (user event) 15/16
    /// </summary>
    SyncE = 0xEE,
    /// <summary>
    /// Not predefined (user event) 16/16
    /// </summary>
    SyncF = 0xEF,

    /// <summary>
    /// Audio end (start of silence)
    /// </summary>
    AudioEnd = 0xFD,
    AudioFileEnds = 0xFE,
    /// <summary>
    /// One more byte of events follows (all the following bytes with the value 0xFF have the same function)
    /// </summary>
    EventByteFollows = 0xFF,
}
