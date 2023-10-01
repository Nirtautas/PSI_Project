using TeamWebApplication.Models;

namespace TeamWebApplication.Data
{
    public interface IPostContainer
    {
        void FetchPosts();
        void PrintPostList();
        void WritePosts();
        ICollection<Post> PostList { get; }
        public int CreatePost(Post post, int currentCourseId);
        public void DeletePost(Post post);
        public Post? GetPost(int postId);
    }
}
