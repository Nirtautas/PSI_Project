using TeamWebApplication.Models.Enums;

namespace TeamWebApplication.Models
{
    public class TextPost : Post
    {
        public string? TextContent { get; set; } = null;
        public TextPost(int id, int courseId, string name = "dname",
            bool IsVisible = true, string textContent = "dcontent")
        {
            base.PostId = id;
            base.CourseId = courseId;
            base.Name = name;
            base.CreationDate = DateTime.Now;
            base.IsVisible = IsVisible;
            base.PostType = PostType.Text;
            this.TextContent = textContent;
        }

        public TextPost()
        {

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

        public override void ApplyData(string? textData)
        {
            TextContent = textData;
        }

        public override string? DataToString()
        {
            return TextContent;
        }

        public override string? DataToHtml()
        {
            return "<p>" + LinkValidation.ValidateAndReplaceLinks(DataToString()) + "</p>";
        }
    }
}
