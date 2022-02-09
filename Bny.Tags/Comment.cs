namespace Bny.Tags;
public struct Comment : IComment
{
    public string Text { get; set; } = "";

    public Comment(string text) => Text = text;

    public override string ToString() => Text;
}
