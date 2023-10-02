namespace TeamWebApplication.Models
{
    public class LinkPost : Post
    {
        public string LinkContent { get; set; }
        public LinkPost(int id, int courseId, string name, DateTime creationDate, bool IsVisible, PostType PostType, string linkContent)
        {
            base.PostId = id;
            base.CourseId = courseId;
            base.Name = name;
            base.CreationDate = creationDate;
            base.IsVisible = IsVisible;
            base.PostType = PostType;
            this.LinkContent = linkContent;
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
                LinkContent;
        }

        public override string DataToString()
        {
            return LinkContent;
        }
    }
}
