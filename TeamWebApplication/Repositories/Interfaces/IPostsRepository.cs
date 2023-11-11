using TeamWebApplication.Models;

namespace TeamWebApplication.Repositories.Interfaces
{
    public interface IPostsRepository
    {
        IEnumerable<Post> GetPostsByCourse(int id);
        Post GetPostById(int id);
        void InsertPost(Post post);
        void DeletePostById(int id);
        void UpdatePost(Comment comment);
        void Save();
    }
}
