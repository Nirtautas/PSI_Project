using System.ComponentModel.DataAnnotations.Schema;
using TeamWebApplication.Models.Enums;

namespace TeamWebApplication.Models
{
    public class FilePost : Post
    {
        public string? FilePath { get; set; } = null;

        public FilePost(int id, int courseId, string name, DateTime creationDate, bool IsVisible, PostType PostType, string filePath)
        {
            base.PostId = id;
            base.CourseId = courseId;
            base.Name = name;
            base.CreationDate = creationDate;
            base.IsVisible = IsVisible;
            base.PostType = PostType;
            this.FilePath = filePath;
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
                FilePath;
        }

        public override void ApplyData(string? textData)
        {
            FilePath = textData;
        }

        public override string? DataToString()
        {
            return FilePath;
        }

        public override string? DataToHtml()
        {
            return "<p>" + DataToString() + "</p>";
        }
    }
}

