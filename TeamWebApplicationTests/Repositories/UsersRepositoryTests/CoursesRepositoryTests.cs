using TeamWebApplication.Repositories;
using TeamWebApplicationTests.DatabaseFixture;

namespace TeamWebApplicationTests.Repositories.UsersRepositoryTests
{
    public class CoursesRepositoryTests : IClassFixture<DBFixture>
    {
        private readonly DBFixture _fixture;

        public CoursesRepositoryTests(DBFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task DeleteCourseByIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var courseRepository = new CoursesRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => courseRepository.DeleteCourseByIdAsync(null));
        }

        [Fact]
        public async Task GetCourseByIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var courseRepository = new CoursesRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => courseRepository.GetCourseByIdAsync(null));
        }

        [Fact]
        public async Task GetCoursesByUserIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var courseRepository = new CoursesRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => courseRepository.GetCoursesByUserIdAsync(null));
        }

        [Fact]
        public async Task InsertCourseAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var courseRepository = new CoursesRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => courseRepository.InsertCourseAsync(null));
        }

        [Fact]
        public async Task UpdateCourseAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var courseRepository = new CoursesRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => courseRepository.UpdateCourseAsync(null));
        }
    }
}
