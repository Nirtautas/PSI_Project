using TeamWebApplication.Models;

namespace TeamWebApplication.Repositories.Interfaces
{
    public interface IPostsRepository
    {
        Task<IEnumerable<Post>> GetPostsByCourseAsync(int? id);
        Task<Post?> GetPostByIdAsync(int? id);
        Task InsertPostAsync<T>(T post) where T : Post?;
        Task DeletePostByIdAsync(int? id);
        Task DeletePostAsync<T>(T post) where T : Post?;
        Task UpdateTextPostAsync(TextPost? post);
        Task UpdateFilePostAsync(FilePost? post);
        Task SaveAsync();
    }
}
