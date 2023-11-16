using TeamWebApplication.Repositories;
using TeamWebApplicationTests.DatabaseFixture;

namespace TeamWebApplicationTests.Repositories.UsersRepositoryTests
{
    public class CommentsRepositoryTests : IClassFixture<DBFixture>
    {
        private readonly DBFixture _fixture;

        public CommentsRepositoryTests(DBFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task DeleteCommentByIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var commentRepository = new CommentsRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => commentRepository.DeleteCommentByIdAsync(null));
        }

        [Fact]
        public async Task DeleteCommentAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var commentRepository = new CommentsRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => commentRepository.DeleteCommentAsync(null));
        }

        [Fact]
        public async Task GetCommentByIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var commentRepository = new CommentsRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => commentRepository.GetCommentByIdAsync(null));
        }

        [Fact]
        public async Task InsertCommentAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var commentRepository = new CommentsRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => commentRepository.InsertCommentAsync(null));
        }

        [Fact]
        public async Task UpdateCommentAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var commentRepository = new CommentsRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => commentRepository.UpdateCommentAsync(null));
        }
    }
}
