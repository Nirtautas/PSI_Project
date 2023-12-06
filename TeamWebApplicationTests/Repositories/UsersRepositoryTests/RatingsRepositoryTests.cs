using Microsoft.EntityFrameworkCore;
using TeamWebApplicationAPI.Models;
using TeamWebApplicationAPI.Repositories;
using TeamWebApplicationTests.DatabaseFixture;

namespace TeamWebApplicationTests.Repositories.UsersRepositoryTests
{
    public class RatingsRepositoryTests : IClassFixture<DBFixture>
    {
        private readonly DBFixture _fixture;
        private readonly RatingsRepository _ratingRepository;

        public RatingsRepositoryTests(DBFixture fixture)
        {
            _fixture = fixture;
            _fixture.RepopulateData();
            _ratingRepository = new RatingsRepository(_fixture.Context);
        }

        [Fact]
        public async Task GetRatingAsync_PassingUserIdCourseId_ReturnsRating()
        {
            var uid = 20000;
            var cid = 10000;
            var expected = 3m;
            var rating = await _ratingRepository.GetRatingAsync(uid, cid);

            Assert.NotNull(rating);
            Assert.Equal(rating.UserId, uid);
            Assert.Equal(rating.CourseId, cid);
            Assert.Equal(rating.UserRating, expected);
        }

        [Theory]
        [InlineData(5, 5)]
        [InlineData(4.35, 4.5)]
        [InlineData(1.75, 2)]
        [InlineData(6.5, 5)]
        [InlineData(-5, 0)]
        public async Task InsertRatingAsync_PassingPost_InsertsRating(decimal passed, decimal expected)
        {
            var uid = 2;
            var cid = 1;
            var rating = new Rating(uid, cid, passed);
            await _ratingRepository.InsertRatingAsync(rating);

            Assert.Equal(5, _fixture.Context.Ratings.Count());
            Assert.Contains(_fixture.Context.Ratings, t => t.UserId == uid && t.CourseId == cid && t.UserRating == expected);
        }

        [Fact]
        public async Task DeleteRatingAsync_PassingUserIdCourseId_DeletesRating()
        {
            var uid = 20000;
            var cid = 10000;
            var expected = 3;
            await _ratingRepository.DeleteRatingAsync(uid, cid);

            Assert.Equal(3, _fixture.Context.Ratings.Count());
            Assert.DoesNotContain(_fixture.Context.Ratings, t => t.UserId == uid && t.CourseId == cid && t.UserRating == expected);
        }

        [Fact]
        public async Task DeleteRatingAsync_PassingRating_DeletesRating()
        {
            var uid = 20000;
            var cid = 10000;
            var urating = 3;
            Assert.Contains(_fixture.Context.Ratings, t => t.UserId == uid && t.CourseId == cid && t.UserRating == urating);
            var rating = await _fixture.Context.Ratings.SingleOrDefaultAsync(t => t.UserId == uid && t.CourseId == cid && t.UserRating == urating);
            await _ratingRepository.DeleteRatingAsync(rating);

            Assert.Equal(3, _fixture.Context.Ratings.Count());
            Assert.DoesNotContain(_fixture.Context.Ratings, t => t.UserId == uid && t.CourseId == cid && t.UserRating == urating);
        }

        [Fact]
        public async Task UpdateRatingAsync_PassingRating_UpdatesRating()
        {
            var uid = 20000; var cid = 10000; var urating = 3; var nurating = 5;
            var rating = new Rating(uid, cid, nurating);
            Assert.Contains(_fixture.Context.Ratings, t => t.UserId == uid && t.CourseId == cid && t.UserRating == urating);
            var existingRating = await _fixture.Context.Ratings.SingleOrDefaultAsync(t => t.UserId == uid && t.CourseId == cid && t.UserRating == urating);
            await _ratingRepository.UpdateRatingAsync(rating);

            Assert.Equal(4, _fixture.Context.Ratings.Count());
            Assert.Contains(_fixture.Context.Ratings, t => t.UserId == uid && t.CourseId == cid && t.UserRating == nurating);
        }

        [Fact]
        public async Task UpdateRatingAsync_PassingExistingRatingAndNewRating_UpdatesRating()
        {
            var uid = 20000; var cid = 10000; var urating = 3; var nurating = 5;
            var rating = new Rating(uid, cid, nurating);
            Assert.Contains(_fixture.Context.Ratings, t => t.UserId == uid && t.CourseId == cid && t.UserRating == urating);
            var existingRating = await _fixture.Context.Ratings.SingleOrDefaultAsync(t => t.UserId == uid && t.CourseId == cid && t.UserRating == urating);
            await _ratingRepository.UpdateRatingAsync(existingRating, rating);

            Assert.Equal(4, _fixture.Context.Ratings.Count());
            Assert.Contains(_fixture.Context.Ratings, t => t.UserId == uid && t.CourseId == cid && t.UserRating == nurating);
        }

        [Theory]
        [InlineData(null, 1)]
        [InlineData(1, null)]
        [InlineData(null, null)]
        public async Task GetRatingAsync_PassingNullValue_ThrowsArgumentNullException(int? userId, int? courseId)
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _ratingRepository.GetRatingAsync(userId, courseId));
        }

        [Fact]
        public async Task InsertRatingAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _ratingRepository.InsertRatingAsync(null));
        }

        [Fact]
        public async Task DeleteRatingAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _ratingRepository.DeleteRatingAsync(null));
        }

        [Theory]
        [InlineData(null, 1)]
        [InlineData(1, null)]
        [InlineData(null, null)]
        public async Task DeleteRatingAsync_Overload_PassingNullValue_ThrowsArgumentNullException(int? userId, int? courseId)
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _ratingRepository.DeleteRatingAsync(userId, courseId));
        }

        [Fact]
        public async Task UpdateRatingAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _ratingRepository.UpdateRatingAsync(null));
        }

        [Fact]
        public async Task UpdateRatingAsync_Overload_PassingNullValue_ThrowsArgumentNullException()
        {
            Rating r = new Rating(1, 1, 1);
            await Assert.ThrowsAsync<ArgumentNullException>(() => _ratingRepository.UpdateRatingAsync(null, r));
            await Assert.ThrowsAsync<ArgumentNullException>(() => _ratingRepository.UpdateRatingAsync(r, null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => _ratingRepository.UpdateRatingAsync(null, null));
        }

        [Fact]
        public async Task GetCourseRatingAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _ratingRepository.GetCourseRatingAsync(null));
        }
    }
}
