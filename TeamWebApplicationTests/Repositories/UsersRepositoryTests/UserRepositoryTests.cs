using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Models;
using TeamWebApplication.Repositories;

namespace TeamWebApplicationTests.Repositories.UsersRepositoryTests
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task GetUserByIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase("Test")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            var context = new ApplicationDBContext(options);
            var userRepository = new UsersRepository(context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => userRepository.GetUserByIdAsync(null));
        }

        [Fact]
        public async Task GetUserByIdAsync_PassingValidId_ReturnsUserWithCorrectId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase("Test")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            var context = new ApplicationDBContext(options);
            var userRepository = new UsersRepository(context);

            var user1 = new User(1); var user2 = new User(2); var user3 = new User(3);
            context.Users.Add(user1); context.Users.Add(user2); context.Users.Add(user3);
            context.SaveChanges();
            
            var gotUser = await userRepository.GetUserByIdAsync(2);
            Assert.Equal(2, gotUser.UserId);
        }
    }
}
