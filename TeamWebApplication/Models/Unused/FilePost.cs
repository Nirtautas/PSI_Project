﻿namespace TeamWebApplication.Models.Unused
{
    public class FilePost : Post
    {        public int UserThatUplodedFile { get; set; } //id of user
        public string TextContent { get; set; }
        //public string FileType { get; set; } possible future implementation with specific icons
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public FilePost(int id, string name, DateTime creationDate, bool IsVisible, string textContent, string fileName, string filePath)
        {
            PostId = id;
            Name = name;
            CreationDate = creationDate;
            base.IsVisible = IsVisible;
            TextContent = textContent;
            FileName = fileName;
            //FileType = fileType; 
            FilePath = filePath;
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

		}

		public override string DataToString()
        {
            return TextContent;
        }

        public override string? DataToHtml()
        {
            return TextContent;
        }
    }
}