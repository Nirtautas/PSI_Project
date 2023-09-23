namespace TeamWebApplication.Models
{
    public abstract class Post
    {
        public int id { get; set; }
        public int courseId { get; set; }
        public string name { get; set; }
        public DateTime creationDate { get; set; }
        public bool isVisible { get; set; }
        public PostType postType { get; set; }

        //Will need to add child classes with specific post types.

        public abstract string DataToString();
    }

    public enum PostType
    {
        Text
    }
}
