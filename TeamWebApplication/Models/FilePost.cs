using TeamWebApplication.Models.Enums;

namespace TeamWebApplication.Models
{
    public class FilePost : Post
    {
        public string? FileName { get; set; } = null;

        public FilePost(int id, int courseId, string name, DateTime creationDate, bool IsVisible, PostType PostType, string fileName)
        {
            base.PostId = id;
            base.CourseId = courseId;
            base.Name = name;
            base.CreationDate = creationDate;
            base.IsVisible = IsVisible;
            base.PostType = PostType;
            this.FileName = fileName;
        }

        public FilePost()
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
                FileName;
        }

        public override void ApplyData(string? textData)
        {
            FileName = textData;
        }

        public override string? DataToString()
        {
            return FileName;
        }

        public override string? DataToHtml()
        {
            return "<a href = \"" + DataToString() + " target=\" _blank\"\" >" + DataToString() + "</a>";
        }
    }
}

