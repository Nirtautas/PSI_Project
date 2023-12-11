using Microsoft.EntityFrameworkCore;
using TeamWebApplicationAPI.Data.Database;
using TeamWebApplicationAPI.Models;
using TeamWebApplicationAPI.Repositories.Interfaces;

namespace TeamWebApplicationAPI.Repositories
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

        public async Task DeleteRatingAsync(int? userId, int? courseId)
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

        public async Task DeleteRatingAsync(Rating? rating)
        {
            if (rating == null)
                throw new ArgumentNullException(nameof(rating));

                _db.Ratings.Remove(rating);
                await SaveAsync();
        }

        public async Task UpdateRatingAsync(Rating? rating)
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

        public async Task UpdateRatingAsync(Rating? originalRating, Rating? rating)
        {
            if (originalRating == null)
                throw new ArgumentNullException(nameof(originalRating));
            if (rating == null)
                throw new ArgumentNullException(nameof(rating));

            originalRating.UserRating = rating.UserRating;
            await SaveAsync();
        }

        public async Task<decimal> GetCourseRatingAsync(int? courseId)
        {
            if (courseId == null)
                throw new ArgumentNullException(nameof(courseId));

            var averageRating = await _db.Ratings
                .Where(t => t.CourseId == courseId)
                .Select(t => t.UserRating)
                .ToListAsync();

            if (averageRating.Count == 0)
            {
                return 0; // or another default value
            }

            return (decimal)averageRating.Average();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
