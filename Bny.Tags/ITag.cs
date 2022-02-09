namespace Bny.Tags;

public interface ITag
{
    public bool SetTag(object? tag, string tagId, bool canToString = true);
    public object? GetTag(string tagId);
}