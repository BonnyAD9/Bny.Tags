namespace Bny.Tags;

public enum TagError
{
    Debug = -1,
    None = 0,
    Unsupported = 1,
    InvalidData = 2,
    UnexpectedEnd = 3,
    FileNotFound = 4,
    WrongVersion = 5,
    InvalidSize = 6,
    NoTag = 7,
    CannotRead = 8,
    CannotSeak = 9,
}