using TeamWebApplicationAPI.Models;

namespace TeamWebApplication.Repositories.Interfaces
{
    public interface IRatingsRepository
    {
        Task<Rating?> GetRatingAsync(int? userId, int? courseId);
        Task InsertRatingAsync(Rating? rating);
        Task DeleteRatingAsync(int? userId, int? courseId);
        Task DeleteRatingAsync(Rating? rating);
        Task UpdateRatingAsync(Rating? rating);
        Task UpdateRatingAsync(Rating? originalRating, Rating? rating);
        Task<decimal> GetCourseRatingAsync(int? courseId);
    }
}
