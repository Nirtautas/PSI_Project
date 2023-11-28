using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Models;
using TeamWebApplication.Repositories.Interfaces;

namespace TeamWebApplication.Repositories
{
    public class RatingsRepository : IRatingsRepository
    {
        private readonly ApplicationDBContext _db;
        public RatingsRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<Rating?> GetRatingAsync(int? userId, int? courseId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));
            if (courseId == null)
                throw new ArgumentNullException(nameof(courseId));

            return await _db.Ratings.SingleOrDefaultAsync(t => t.UserId == userId && t.CourseId == courseId);
        }

        public async Task InsertRatingAsync(Rating? rating)
        {
            if (rating == null)
                throw new ArgumentNullException(nameof(rating));

            await _db.Ratings.AddAsync(rating);
            await SaveAsync();
        }

        public async Task DeleteRating(int? userId, int? courseId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));
            if (courseId == null)
                throw new ArgumentNullException(nameof(courseId));

            var rating = await _db.Ratings.FirstOrDefaultAsync(t => t.UserId == userId && t.CourseId == courseId);

            if (rating != null)
            {
                _db.Ratings.Remove(rating);
                await SaveAsync();
            }
        }

        public async Task DeleteRating(Rating? rating)
        {
            if (rating == null)
                throw new ArgumentNullException(nameof(rating));

                _db.Ratings.Remove(rating);
                await SaveAsync();
        }

        public async Task UpdateRatingsAsync(Rating? rating)
        {
            if (rating == null)
                throw new ArgumentNullException(nameof(rating));

            var existingRating = await _db.Ratings.FirstOrDefaultAsync(u => u.UserId == rating.UserId && u.CourseId == rating.CourseId);

            if (existingRating != null)
            {
                existingRating.UserRating = rating.UserRating;
                await SaveAsync();
            }
        }

        public async Task UpdateRatingsAsync(Rating? originalRating, Rating? rating)
        {
            if (originalRating == null)
                throw new ArgumentNullException(nameof(originalRating));
            if (rating == null)
                throw new ArgumentNullException(nameof(rating));

            originalRating.UserRating = rating.UserRating;
            await SaveAsync();
        }

        public async Task<double> GetCourseRatingAsync(int? courseId)
        {
            if (courseId == null)
                throw new ArgumentNullException(nameof(courseId));

            return await _db.Ratings.Where(t => t.CourseId == courseId)
                .AverageAsync(t => t.UserRating);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
