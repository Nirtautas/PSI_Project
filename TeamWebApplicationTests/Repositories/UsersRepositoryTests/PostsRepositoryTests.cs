using TeamWebApplication.Models;
using TeamWebApplication.Repositories;
using TeamWebApplicationTests.DatabaseFixture;

namespace TeamWebApplicationTests.Repositories.UsersRepositoryTests
{
    public class PostsRepositoryTests : IClassFixture<DBFixture>
    {
        private readonly DBFixture _fixture;

        public PostsRepositoryTests(DBFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetPostsByCourseAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var postRepository = new PostsRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => postRepository.GetPostsByCourseAsync(null));
        }

        [Fact]
        public async Task GetPostByIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var postRepository = new PostsRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => postRepository.GetPostByIdAsync(null));
        }

        [Fact]
        public async Task InsertPostAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var postRepository = new PostsRepository(_fixture.Context);
            Post? post = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() => postRepository.InsertPostAsync(post));
        }

        [Fact]
        public async Task DeletePostByIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var postRepository = new PostsRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => postRepository.DeletePostByIdAsync(null));
        }

        [Fact]
        public async Task DeletePostAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var postRepository = new PostsRepository(_fixture.Context);
            Post? post = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() => postRepository.DeletePostAsync(post));
        }

        [Fact]
        public async Task UpdateTextPostAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var postRepository = new PostsRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => postRepository.UpdatePostAsync<TextPost>(null, null));
        }

        [Fact]
        public async Task UpdateFilePostAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            var postRepository = new PostsRepository(_fixture.Context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => postRepository.UpdatePostAsync<FilePost>(null, null));
        }
    }
}
