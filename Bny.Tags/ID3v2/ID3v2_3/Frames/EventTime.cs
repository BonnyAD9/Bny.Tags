namespace Bny.Tags.ID3v2.ID3v2_3.Frames;

public struct EventTime
{
    public const int size = 5;

    public EventType Type;
    public uint TimeStamp;

    public EventTime()
    {
        Type = EventType.Padding;
        TimeStamp = 0;
    }

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

public enum EventType : byte
{
    Padding = 0x00,
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
    MomentaryUnwantedNoise = 0x0D,
    SustainedNoise = 0x0E,
    SustainedNoiseEnd = 0x0F,
    IntroEnd = 0x10,
    MainpartEnd = 0x11,
    VerseEnd = 0x12,
    RefrainEnd = 0x13,
    ThemeEnd = 0x14,

    Sync0 = 0xE0,
    Sync1 = 0xE1,
    Sync2 = 0xE2,
    Sync3 = 0xE3,
    Sync4 = 0xE4,
    Sync5 = 0xE5,
    Sync6 = 0xE6,
    Sync7 = 0xE7,
    Sync8 = 0xE8,
    Sync9 = 0xE9,
    SyncA = 0xEA,
    SyncB = 0xEB,
    SyncC = 0xEC,
    SyncD = 0xED,
    SyncE = 0xEE,
    SyncF = 0xEF,

    AudioEnd = 0xFD,
    AudioFileEnd = 0xFE,
    EventByteFollows = 0xFF,
}
