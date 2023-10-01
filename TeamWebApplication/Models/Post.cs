namespace TeamWebApplication.Models
{
    public abstract class Post
    {
        public int PostId { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsVisible { get; set; }
        public PostType PostType { get; set; }

        //Abstract definitions
        public abstract void ApplyData(string? textData);
        public abstract string? DataToString();
        public abstract string? DataToHtml();
    }
    public enum PostType
    {
        Text,
        Link
    }
}
