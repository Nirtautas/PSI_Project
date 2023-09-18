namespace TeamWebApplication.Models
{
    public class TextPost : Post
    {
        public string TextContent { get; set; }
        public TextPost(int id, string name, DateTime creationDate, bool isVisible, string textContent)
        {
            Id = id;
            Name = name;
            CreationDate = creationDate;
            IsVisible = isVisible;
            TextContent = textContent;
        }
    }
}
