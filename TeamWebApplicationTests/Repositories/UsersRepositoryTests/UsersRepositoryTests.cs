using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Models;
using TeamWebApplication.Repositories;
using TeamWebApplicationTests.DatabaseFixture;

namespace TeamWebApplicationTests.Repositories.UsersRepositoryTests
{
    public class UsersRepositoryTests : IClassFixture<DBFixture>
    {
        private readonly DBFixture _fixture;

        public UsersRepositoryTests(DBFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetUserByIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var userRepository = new UsersRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => userRepository.GetUserByIdAsync(null));
        }

        [Fact]
        public async Task GetUserByIdAsync_PassingValidId_ReturnsUserWithCorrectId()
        {
            var userRepository = new UsersRepository(_fixture.Context);

            var user1 = new User(1); var user2 = new User(2); var user3 = new User(3);
            _fixture.Context.Users.Add(user1); _fixture.Context.Users.Add(user2); _fixture.Context.Users.Add(user3);
            _fixture.Context.SaveChanges();
            
            var gotUser = await userRepository.GetUserByIdAsync(2);
            Assert.Equal(2, gotUser.UserId);
        }

        [Fact]
        public async Task GetUsersInCourseAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var userRepository = new UsersRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => userRepository.GetUsersInCourseAsync(null));
        }

        [Theory]
        [InlineData(1, null)]
        [InlineData(null, "i")]
        [InlineData(null, null)]
        public async Task GetUserByCredentialsAsync_PassingNullValue_ThrowsArgumentNullException(int? userId, string? password)
        {
            var userRepository = new UsersRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => userRepository.GetUserByCredentialsAsync(userId, password));
        }

        [Fact]
        public async Task UserWithSuchEmailExistsAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var userRepository = new UsersRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => userRepository.UserWithSuchEmailExistsAsync(null));
        }

        [Fact]
        public async Task GetUserByEmailAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var userRepository = new UsersRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => userRepository.GetUserByEmailAsync(null));
        }

        [Fact]
        public async Task InsertUserAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var userRepository = new UsersRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => userRepository.InsertUserAsync(null));
        }

        [Fact]
        public async Task DeleteUserByIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var userRepository = new UsersRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => userRepository.DeleteUserByIdAsync(null));
        }

        [Fact]
        public async Task DeleteUserAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var userRepository = new UsersRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => userRepository.DeleteUserAsync(null));
        }

        [Fact]
        public async Task UpdateUserAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var userRepository = new UsersRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => userRepository.UpdateUserAsync(null));
        }
    }
}
