using TeamWebApplication.Models.Enums;

namespace TeamWebApplication.Models
{
    public class FilePost : Post
    {
        public string? FileName { get; set; } = null;

        public FilePost()
        {

        }

        public FilePost(int id, int courseId, string name = "dname",
            bool IsVisible = true, string fileName = "dname")
        {
            base.PostId = id;
            base.CourseId = courseId;
            base.Name = name;
            base.CreationDate = DateTime.Now;
            base.IsVisible = IsVisible;
            base.PostType = PostType.File;
            this.FileName = fileName;
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

