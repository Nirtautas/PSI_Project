using TeamWebApplication.Models;

namespace TeamWebApplication.Repositories.Interfaces
{
    public delegate Task ActionDelegate<T>(T originalPost, T post) where T : Post?;
    public interface IPostsRepository
    {
        Task<bool> IsFileUsedInOtherPostsAsync(string fileName, int PostId);
        Task<IEnumerable<Post>> GetPostsByCourseAsync(int? id);
        Task<Post?> GetPostByIdAsync(int? id);
        Task InsertPostAsync<T>(T post) where T : Post?;
        Task DeletePostByIdAsync(int? id);
        Task DeletePostAsync<T>(T post) where T : Post?;
        Task UpdatePostAsync<T>(T originalPost, T post) where T : Post?;
        Task SaveAsync();
        ActionDelegate<Post> UpdateAndSaveDelegate { get; set; }
    }
}
