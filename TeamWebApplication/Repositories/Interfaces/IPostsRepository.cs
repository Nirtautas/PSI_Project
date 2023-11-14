using TeamWebApplication.Models;

namespace TeamWebApplication.Repositories.Interfaces
{
    public interface IPostsRepository
    {
        Task<IEnumerable<Post>> GetPostsByCourseAsync(int id);
        Task<Post> GetPostByIdAsync(int id);
        Task InsertPostAsync(Post post);
        Task DeletePostByIdAsync(int id);
        Task UpdateTextPostAsync(TextPost post);
        Task UpdateFilePostAsync(FilePost post);
        Task SaveAsync();
    }
}
