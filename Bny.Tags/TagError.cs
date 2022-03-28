namespace Bny.Tags;

/// <summary>
/// Represents an error that occured while reading tag
/// </summary>
public enum TagError
{
    /// <summary>
    /// This is used for debugging; You should never encounter this error
    /// </summary>
    Debug = -1,
    /// <summary>
    /// Tag was readed successfully
    /// </summary>
    None = 0,
    /// <summary>
    /// Tag contains unsupported functionality (encryption, compression, experimental flags,...)
    /// </summary>
    Unsupported = 1,
    /// <summary>
    /// The tag includes invalid data
    /// </summary>
    InvalidData = 2,
    /// <summary>
    /// Readed data didn't match expectations
    /// </summary>
    UnexpectedEnd = 3,
    /// <summary>
    /// The specified file was not found
    /// </summary>
    FileNotFound = 4,
    /// <summary>
    /// Unsupported version of the tag
    /// </summary>
    WrongVersion = 5,
    /// <summary>
    /// The data or tag has invalid size
    /// </summary>
    InvalidSize = 6,
    /// <summary>
    /// The tag was not found in the data
    /// </summary>
    NoTag = 7,
    /// <summary>
    /// Cannot read from the stream
    /// </summary>
    CannotRead = 8,
    /// <summary>
    /// Cannot seek in the stream
    /// </summary>
    CannotSeak = 9,
}