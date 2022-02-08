namespace Bny.Tags;

public interface ITag
{
    public bool SetTag(object? tag, string tagId);
    public object? GetTag(string tagId);
}