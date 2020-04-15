namespace FoxHound.App.Blogs.Common
{
    public interface IBlogCommand
    {
        string Title { get; set; }
        string Owner { get; set; }
    }
}
