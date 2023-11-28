using TeamWebApplication.Models;
using TeamWebApplication.Repositories;
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
        public async Task UpdateRatingsAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _ratingRepository.UpdateRatingsAsync(null));
        }

        [Fact]
        public async Task UpdateRatingsAsync_Overload_PassingNullValue_ThrowsArgumentNullException()
        {
            Rating r = new Rating(1, 1, 1);
            await Assert.ThrowsAsync<ArgumentNullException>(() => _ratingRepository.UpdateRatingsAsync(null, r));
            await Assert.ThrowsAsync<ArgumentNullException>(() => _ratingRepository.UpdateRatingsAsync(r, null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => _ratingRepository.UpdateRatingsAsync(null, null));
        }

        [Fact]
        public async Task GetCourseRatingAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _ratingRepository.GetCourseRatingAsync(null));
        }
    }
}
