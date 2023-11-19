using Microsoft.EntityFrameworkCore;
using TeamWebApplication.Models;
using TeamWebApplication.Models.Enums;
using TeamWebApplication.Repositories;
using TeamWebApplicationTests.DatabaseFixture;

namespace TeamWebApplicationTests.Repositories.UsersRepositoryTests
{
    public class UsersRepositoryTests : IClassFixture<DBFixture>
    {
        private readonly DBFixture _fixture;
        private readonly UsersRepository _userRepository;

        public UsersRepositoryTests(DBFixture fixture)
        {
            _fixture = fixture;
            _fixture.RepopulateData();
            _userRepository = new UsersRepository(_fixture.Context);
        }

        [Fact]
        public async Task GetUserByIdAsync_PassingValidId_ReturnsUserWithCorrectId()
        {
            var id = 20000;
            var gotUser = await _userRepository.GetUserByIdAsync(id);

            Assert.NotNull(gotUser);
            Assert.Equal(id, gotUser.UserId);
        }

        [Fact]
        public async Task GetUsersInCourseAsync_PassingValidId_ReturnsUsersInCourse()
        {
            var usersInCourse = await _userRepository.GetUsersInCourseAsync(10000);

            Assert.Equal(2, usersInCourse.Count());
            Assert.Contains(usersInCourse, t => t.UserId == 20000);
            Assert.Contains(usersInCourse, t => t.UserId == 20001);
        }

        [Fact]
        public async Task GetUserByCredentialsAsync_PassingValidIdAndPassword_ReturnsUserWithCorrectCredentials()
        {
            var id = 20000;
            var user = await _userRepository.GetUserByCredentialsAsync(id, "qweryty1");

            Assert.NotNull(user);
            Assert.Equal(id, user.UserId);
        }

        [Fact]
        public async Task UserWithSuchEmailExistsAsync_PassingEmailOfExistingUser_ReturnsTrue()
        {
            var boolean = await _userRepository.UserWithSuchEmailExistsAsync("j.paguzinskas@mif.stuf.vu.lt");

            Assert.True(boolean);
        }

        [Fact]
        public async Task UserWithSuchEmailExistsAsync_PassingEmailOfNotExistingUser_ReturnsFalse()
        {
            var boolean = await _userRepository.UserWithSuchEmailExistsAsync("aaaa");

            Assert.False(boolean);
        }

        [Fact]
        public async Task GetUserByEmailAsync_PassingEmailOfExistingUser_ReturnsUser()
        {
            var user = await _userRepository.GetUserByEmailAsync("j.paguzinskas@mif.stuf.vu.lt");

            Assert.NotNull(user);
            Assert.Equal(20000, user.UserId);
        }

        [Fact]
        public async Task InsertUserAsync_PassingUser_InsertsUser()
        {
            var id = 20006;
            var user = new User(id);
            await _userRepository.InsertUserAsync(user);

            Assert.Equal(4, _fixture.Context.Users.Count());
            Assert.Contains(_fixture.Context.Users, t => t.UserId == id);
        }

        [Fact]
        public async Task DeleteUserByIdAsync_PassingUser_DeletesUser()
        {
            var id = 20000;
            Assert.Contains(_fixture.Context.Users, t => t.UserId == id);
            await _userRepository.DeleteUserByIdAsync(id);

            Assert.Equal(2, _fixture.Context.Users.Count());
            Assert.DoesNotContain(_fixture.Context.Users, t => t.UserId == id);
        }

        [Fact]
        public async Task DeleteUserAsync_PassingUser_DeletesUser()
        {
            var id = 20000;
            Assert.Contains(_fixture.Context.Users, t => t.UserId == id);
            var user = await _fixture.Context.Users.SingleOrDefaultAsync(t => t.UserId == id);
            await _userRepository.DeleteUserAsync(user);

            Assert.Equal(2, _fixture.Context.Users.Count());
            Assert.DoesNotContain(_fixture.Context.Users, t => t.UserId == id);
        }

        [Fact]
        public async Task UpdateUserAsync_PassingUser_UpdatesUser()
        {
            var id = 20000;
            Assert.Contains(_fixture.Context.Users, t => t.UserId == id);
            var userToUpdate = new User(id, "TestName", "TestSurname", "TestEmail", "TestPassword", Role.None, Faculty.None, Specialization.None);
            await _userRepository.UpdateUserAsync(userToUpdate);
            var user = await _fixture.Context.Users.SingleOrDefaultAsync(t => t.UserId == id);

            Assert.NotNull(user);
            Assert.Equal(user.UserId, userToUpdate.UserId);
            Assert.Equal(user.Name, userToUpdate.Name);
            Assert.Equal(user.Surname, userToUpdate.Surname);
            Assert.Equal(user.Email, userToUpdate.Email);
            Assert.Equal(user.Password, userToUpdate.Password);
            Assert.Equal(user.Role, userToUpdate.Role);
            Assert.Equal(user.Faculty, userToUpdate.Faculty);
            Assert.Equal(user.Specialization, userToUpdate.Specialization);
        }

        [Fact]
        public async Task GetUserByIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userRepository.GetUserByIdAsync(null));
        }

        [Fact]
        public async Task GetUsersInCourseAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userRepository.GetUsersInCourseAsync(null));
        }

        [Theory]
        [InlineData(1, null)]
        [InlineData(null, "i")]
        [InlineData(null, null)]
        public async Task GetUserByCredentialsAsync_PassingNullValue_ThrowsArgumentNullException(int? userId, string? password)
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userRepository.GetUserByCredentialsAsync(userId, password));
        }

        [Fact]
        public async Task UserWithSuchEmailExistsAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userRepository.UserWithSuchEmailExistsAsync(null));
        }

        [Fact]
        public async Task GetUserByEmailAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userRepository.GetUserByEmailAsync(null));
        }

        [Fact]
        public async Task InsertUserAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userRepository.InsertUserAsync(null));
        }

        [Fact]
        public async Task DeleteUserByIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userRepository.DeleteUserByIdAsync(null));
        }

        [Fact]
        public async Task DeleteUserAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userRepository.DeleteUserAsync(null));
        }

        [Fact]
        public async Task UpdateUserAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userRepository.UpdateUserAsync(null));
        }
    }
}
