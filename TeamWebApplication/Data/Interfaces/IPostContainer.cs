using TeamWebApplication.Models;

namespace TeamWebApplication.Data
{
    public interface IPostContainer
    {
        void FetchPosts();
        void PrintPostList();
        void WritePosts();
        ICollection<Post> PostList { get; }
        int CreatePost(Post post, int currentCourseId);
        void DeletePost(Post post);
        Post? GetPost(int postId);
    }
}
