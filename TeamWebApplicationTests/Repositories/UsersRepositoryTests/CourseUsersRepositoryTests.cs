using TeamWebApplication.Repositories;
using TeamWebApplicationTests.DatabaseFixture;

namespace TeamWebApplicationTests.Repositories.UsersRepositoryTests
{
    public class CourseUsersRepositoryTests : IClassFixture<DBFixture>
    {
        private readonly DBFixture _fixture;

        public CourseUsersRepositoryTests(DBFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetUsersByCourseIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var courseUsersRepository = new CourseUsersRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => courseUsersRepository.GetUsersByCourseIdAsync(null));
        }

        [Fact]
        public async Task GetCoursesByUserIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var courseUsersRepository = new CourseUsersRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => courseUsersRepository.GetCoursesByUserIdAsync(null));
        }

        [Theory]
        [InlineData(1, null)]
        [InlineData(null, 1)]
        [InlineData(null, null)]
        public async Task CheckIfRelationExistsAsync_PassingNullValue_ThrowsArgumentNullException(int? courseId, int? userId)
        {
            var courseUsersRepository = new CourseUsersRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => courseUsersRepository.CheckIfRelationExistsAsync(courseId, userId));
        }

        [Theory]
        [InlineData(1, null)]
        [InlineData(null, 1)]
        [InlineData(null, null)]
        public async Task InsertRelationAsync_PassingNullValue_ThrowsArgumentNullException(int? courseId, int? userId)
        {
            var courseUsersRepository = new CourseUsersRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => courseUsersRepository.InsertRelationAsync(courseId, userId));
        }

        [Theory]
        [InlineData(1, null)]
        [InlineData(null, 1)]
        [InlineData(null, null)]
        public async Task DeleteRelationAsync_PassingNullValue_ThrowsArgumentNullException(int? courseId, int? userId)
        {
            var courseUsersRepository = new CourseUsersRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => courseUsersRepository.DeleteRelationAsync(courseId, userId));
        }
    }
}
