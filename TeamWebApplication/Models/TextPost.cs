namespace TeamWebApplication.Models
{
    public class TextPost : Post
    {
        public string TextContent { get; set; }
        public TextPost(int id, int courseId, string name, DateTime creationDate, bool IsVisible, PostType PostType, string textContent)
        {
            base.PostId = id;
            base.CourseId = courseId;
            base.Name = name;
            base.CreationDate = creationDate;
            base.IsVisible = IsVisible;
            base.PostType = PostType;
            this.TextContent = textContent;
        }

        public override string ToString()
        {
            return
                PostId.ToString() + ";" +
                CourseId.ToString() + ";" +
                Name + ";" +
                CreationDate.ToString() + ";" +
                IsVisible + ";" +
                PostType.ToString() + ';' +
                TextContent;
        }

        public override string DataToString()
        {
            return TextContent;
        }
    }
}
