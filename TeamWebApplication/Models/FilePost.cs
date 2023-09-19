namespace TeamWebApplication.Models
{
    public class FilePost : Post
    {
        public int UserThatUplodedFile { get; set; } //id of user
        public string TextContent { get; set; }
        //public string FileType { get; set; } possible future implementation with specific icons
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public FilePost(int id, string name, DateTime creationDate, bool isVisible, string textContent, string fileName, string filePath)
        {
            Id = id;
            Name = name;
            CreationDate = creationDate;
            IsVisible = isVisible;
            TextContent = textContent;
            FileName = fileName;
            //FileType = fileType; 
            FilePath = filePath;
        }
    }
}
