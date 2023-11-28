using TeamWebApplication.Models;

namespace TeamWebApplication.Repositories.Interfaces
{
    public interface IRatingsRepository
    {
        Task<Rating?> GetRatingAsync(int? userId, int? courseId);
        Task InsertRatingAsync(Rating? rating);
        Task DeleteRatingAsync(int? userId, int? courseId);
        Task DeleteRatingAsync(Rating? rating);
        Task UpdateRatingsAsync(Rating? rating);
        Task UpdateRatingsAsync(Rating? originalRating, Rating? rating);
        Task<double> GetCourseRatingAsync(int? courseId);
    }
}
