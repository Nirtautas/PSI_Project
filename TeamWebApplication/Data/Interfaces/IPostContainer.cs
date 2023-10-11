using TeamWebApplication.Models;

namespace TeamWebApplication.Data
{
    public interface IPostContainer
    {
        void FetchPosts();
        void PrintPostList();
        void WritePosts();
        ICollection<Post> PostList { get; }
        int CreatePost(Post post, int CurrentCourseId);
        void DeletePost(Post post);
        Post? GetPost(int postId);
    }
}
