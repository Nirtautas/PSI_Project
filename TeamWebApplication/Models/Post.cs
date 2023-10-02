namespace TeamWebApplication.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsVisible { get; set; }
        public PostType PostType { get; set; }
        public virtual string DataToString()
        {
            return Name;
        }
    }
    public enum PostType
    {
        Text,
        Link
    }
}
