namespace TeamWebApplication.Models
{
    public abstract class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsVisible { get; set; }

        //Will need to add child classes with specific post types.

        public override string ToString()
        {
            return
                Id.ToString() + ";" +
                Name + ";" +
                CreationDate.ToString() + ";" +
                IsVisible;
        }
    }
}
