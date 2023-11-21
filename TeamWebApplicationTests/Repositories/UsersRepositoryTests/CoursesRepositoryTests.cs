using Microsoft.EntityFrameworkCore;
using TeamWebApplication.Models;
using TeamWebApplication.Models.Enums;
using TeamWebApplication.Repositories;
using TeamWebApplication.Repositories.Interfaces;
using TeamWebApplicationTests.DatabaseFixture;

namespace TeamWebApplicationTests.Repositories.UsersRepositoryTests
{
    public class CoursesRepositoryTests : IClassFixture<DBFixture>
    {
        private readonly DBFixture _fixture;
        private readonly CoursesRepository _courseRepository;

        public CoursesRepositoryTests(DBFixture fixture)
        {
            _fixture = fixture;
            _fixture.RepopulateData();
            _courseRepository = new CoursesRepository(_fixture.Context);
        }

        [Fact]
        public async Task DeleteCourseByIdAsync_PassingCourseId_DeletesCourse()
        {
            var id = 10000;
            Assert.Contains(_fixture.Context.Courses, t => t.CourseId == id);
            await _courseRepository.DeleteCourseByIdAsync(id);

            Assert.DoesNotContain(_fixture.Context.Courses, t => t.CourseId == id);
        }

        [Fact]
        public async Task GetCourseByIdAsync_PassingCourseId_ReturnsCourse()
        {
            var id = 10000;
            var course = await _courseRepository.GetCourseByIdAsync(id);

            Assert.NotNull(course);
            Assert.Equal(course.CourseId, id);
        }

        [Fact]
        public async Task GetCoursesAsync_Executing_ReturnsCourses()
        {
            var courses = await _courseRepository.GetCoursesAsync();

            Assert.Equal(3, courses.Count());
            Assert.Contains(courses, t => t.CourseId == 10000);
            Assert.Contains(courses, t => t.CourseId == 10001);
            Assert.Contains(courses, t => t.CourseId == 10002);
        }

        [Fact]
        public async Task GetPublicCoursesAsync_Executing_ReturnsPublicCourses()
        {
            var courses = await _courseRepository.GetPublicCoursesAsync();

            Assert.Single(courses);
            Assert.Contains(courses, t => t.CourseId == 10002);
        }

        [Fact]
        public async Task GetCoursesByUserIdAsync_PassingValidUserId_ReturnsCoursesUserAttends()
        {
            var courses = await _courseRepository.GetCoursesByUserIdAsync(20000);

            Assert.Equal(2, courses.Count());
            Assert.Contains(courses, t => t.CourseId == 10000);
            Assert.Contains(courses, t => t.CourseId == 10001);
        }

        [Fact]
        public async Task InsertCourseAsync_PassingCourse_InsertsCourse()
        {
            var id = 10003;
            var course = new Course(id);
            await _courseRepository.InsertCourseAsync(course);

            Assert.Equal(4, _fixture.Context.Courses.Count());
            Assert.Contains(_fixture.Context.Courses, t => t.CourseId == id);
        }

        [Fact]
        public async Task UpdateCourseAsync_PassingCourse_UpdatesCourse()
        {
            var id = 10000;
            Assert.Contains(_fixture.Context.Courses, t => t.CourseId == id);
            var courseToUpdate = new Course(id, "TestName", "TestDescription", false, false);
            await _courseRepository.UpdateCourseAsync(courseToUpdate);
            var course = await _fixture.Context.Courses.SingleOrDefaultAsync(t => t.CourseId == id);

            Assert.NotNull(course);
            Assert.Equal(course.CourseId, courseToUpdate.CourseId);
            Assert.Equal(course.Name, courseToUpdate.Name);
            Assert.Equal(course.Description, courseToUpdate.Description);
            Assert.Equal(course.IsPublic, courseToUpdate.IsPublic);
            Assert.Equal(course.IsVisible, courseToUpdate.IsVisible);
        }

        [Fact]
        public async Task DeleteCourseByIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _courseRepository.DeleteCourseByIdAsync(null));
        }

        [Fact]
        public async Task GetCourseByIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _courseRepository.GetCourseByIdAsync(null));
        }

        [Fact]
        public async Task GetCoursesByUserIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _courseRepository.GetCoursesByUserIdAsync(null));
        }

        [Fact]
        public async Task InsertCourseAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _courseRepository.InsertCourseAsync(null));
        }

        [Fact]
        public async Task UpdateCourseAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _courseRepository.UpdateCourseAsync(null));
        }
    }
}
