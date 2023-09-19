namespace TeamWebApplication.Models
{
    public class TextPost : Post
    {
        public string textContent { get; set; }
        public TextPost(int id, int courseId, string name, DateTime creationDate, bool isVisible, PostType postType, string textContent)
        {
            base.id = id;
            base.courseId = courseId;
            base.name = name;
            base.creationDate = creationDate;
            base.isVisible = isVisible;
            base.postType = postType;
            this.textContent = textContent;
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
                textContent;
        }

        public override string DataToString()
        {
            return textContent;
        }
    }
}
