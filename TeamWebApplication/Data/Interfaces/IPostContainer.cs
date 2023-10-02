using TeamWebApplication.Models;

namespace TeamWebApplication.Data
{
    public interface IPostContainer
    {
        void FetchPosts();
        void PrintPostList();
        void WritePosts();
        ICollection<Post> PostList { get; }
        public int CreatePost(LinkPost post);
        public int CreatePost(TextPost post);
        public int DeletePost(LinkPost postToRemove);
        public int DeletePost(TextPost postToRemove);
        public LinkPost? GetLinkPost(int postId);
        public TextPost? GetTextPost(int postId);
    }
}
