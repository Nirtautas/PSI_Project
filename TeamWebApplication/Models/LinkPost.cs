namespace TeamWebApplication.Models
{
    public class LinkPost : Post
    {
        public string linkContent { get; set; }
        public LinkPost(int id, int courseId, string name, DateTime creationDate, bool isVisible, PostType postType, string linkContent)
        {
            base.id = id;
            base.courseId = courseId;
            base.name = name;
            base.creationDate = creationDate;
            base.isVisible = isVisible;
            base.postType = postType;
            this.linkContent = linkContent;
        }

        public override string ToString()
        {
            return
                id.ToString() + ";" +
                courseId.ToString() + ";" +
                name + ";" +
                creationDate.ToString() + ";" +
                isVisible + ";" +
                postType.ToString() + ';' +
                linkContent;
        }

        public override string DataToString()
        {
            return linkContent;
        }
    }
}
