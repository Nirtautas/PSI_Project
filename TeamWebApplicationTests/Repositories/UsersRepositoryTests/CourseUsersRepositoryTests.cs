using TeamWebApplicationAPI.Repositories;
using TeamWebApplicationTests.DatabaseFixture;

namespace TeamWebApplicationTests.Repositories.UsersRepositoryTests
{
    public class CourseUsersRepositoryTests : IClassFixture<DBFixture>
    {
        private readonly DBFixture _fixture;
        private readonly CourseUsersRepository _courseUsersRepository;

        public CourseUsersRepositoryTests(DBFixture fixture)
        {
            _fixture = fixture;
            _fixture.RepopulateData();
            _courseUsersRepository = new CourseUsersRepository(_fixture.Context);
        }

        [Fact]
        public async Task GetUsersByCourseIdAsync_PassingCourseId_ReturnsUserIdListInCourse()
        {
            var id = 10000;
            Assert.Contains(_fixture.Context.Courses, t => t.CourseId == id);
            var userIdList = await _courseUsersRepository.GetUsersByCourseIdAsync(id);

            Assert.Equal(2, userIdList.Count());
            Assert.Contains(userIdList, t => t == 20000);
            Assert.Contains(userIdList, t => t == 20001);
        }

        [Fact]
        public async Task GetCoursesByUserIdAsync_PassingUserId_ReturnsCourseIdListWithUser()
        {
            var id = 20000;
            Assert.Contains(_fixture.Context.Users, t => t.UserId == id);
            var courseIdList = await _courseUsersRepository.GetCoursesByUserIdAsync(id);

            Assert.Equal(2, courseIdList.Count());
            Assert.Contains(courseIdList, t => t == 10000);
            Assert.Contains(courseIdList, t => t == 10001);
        }

        [Fact]
        public async Task GetRelationsByUserIdAsync_PassingUserId_ReturnsCourseIdListWithUser()
        {
            var id = 20001;
            Assert.Contains(_fixture.Context.Users, t => t.UserId == id);
            var courseIdList = await _courseUsersRepository.GetRelationsByUserIdAsync(id);

            Assert.Equal(2, courseIdList.Count());
            Assert.Contains(courseIdList, t => t.UserId == id && t.CourseId == 10000);
            Assert.Contains(courseIdList, t => t.UserId == id && t.CourseId == 10002);
        }

        [Fact]
        public async Task CheckIfRelationExistsAsync_PassingCourseIdUserId_ReturnsTrue()
        {
            var cid = 10000; var uid = 20000;
            var boolean = await _courseUsersRepository.CheckIfRelationExistsAsync(cid, uid);

            Assert.True(boolean);
        }

        [Fact]
        public async Task CheckIfRelationExistsAsync_PassingCourseIdUserId_ReturnsFalse()
        {
            var cid = 1; var uid = 2;
            var boolean = await _courseUsersRepository.CheckIfRelationExistsAsync(cid, uid);

            Assert.False(boolean);
        }

        [Fact]
        public async Task InsertRelationAsync_PassingCourseIdUserId_InsertsRelation()
        {
            var cid = 1; var uid = 2;
            await _courseUsersRepository.InsertRelationAsync(cid, uid);

            Assert.Equal(6, _fixture.Context.CoursesUsers.Count());
            Assert.Contains(_fixture.Context.CoursesUsers, t => t.CourseId == cid && t.UserId == uid);
        }

        [Fact]
        public async Task DeleteRelationAsync_PassingCourseIdUserId_DeletesRelation()
        {
            var cid = 10000; var uid = 20000;
            Assert.Contains(_fixture.Context.CoursesUsers, t => t.CourseId == cid && t.UserId == uid);
            await _courseUsersRepository.DeleteRelationAsync(cid, uid);

            Assert.Equal(4, _fixture.Context.CoursesUsers.Count());
            Assert.DoesNotContain(_fixture.Context.CoursesUsers, t => t.CourseId == cid && t.UserId == uid);
        }

        [Fact]
        public async Task GetUsersByCourseIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _courseUsersRepository.GetUsersByCourseIdAsync(null));
        }

        [Fact]
        public async Task GetCoursesByUserIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _courseUsersRepository.GetCoursesByUserIdAsync(null));
        }

        [Fact]
        public async Task GetRelationsByUserIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _courseUsersRepository.GetRelationsByUserIdAsync(null));
        }

        [Theory]
        [InlineData(1, null)]
        [InlineData(null, 1)]
        [InlineData(null, null)]
        public async Task CheckIfRelationExistsAsync_PassingNullValue_ThrowsArgumentNullException(int? courseId, int? userId)
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _courseUsersRepository.CheckIfRelationExistsAsync(courseId, userId));
        }

        [Theory]
        [InlineData(1, null)]
        [InlineData(null, 1)]
        [InlineData(null, null)]
        public async Task InsertRelationAsync_PassingNullValue_ThrowsArgumentNullException(int? courseId, int? userId)
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _courseUsersRepository.InsertRelationAsync(courseId, userId));
        }

        [Theory]
        [InlineData(1, null)]
        [InlineData(null, 1)]
        [InlineData(null, null)]
        public async Task DeleteRelationAsync_PassingNullValue_ThrowsArgumentNullException(int? courseId, int? userId)
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _courseUsersRepository.DeleteRelationAsync(courseId, userId));
        }
    }
}
