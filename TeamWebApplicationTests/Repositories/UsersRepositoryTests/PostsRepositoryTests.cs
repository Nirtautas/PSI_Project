using System.Linq;
using TeamWebApplication.Models;
using TeamWebApplication.Models.Enums;
using TeamWebApplication.Repositories;
using TeamWebApplicationTests.DatabaseFixture;

namespace TeamWebApplicationTests.Repositories.UsersRepositoryTests
{
    public class PostsRepositoryTests : IClassFixture<DBFixture>
    {
        private readonly DBFixture _fixture;
        private readonly PostsRepository _postRepository;

        public PostsRepositoryTests(DBFixture fixture)
        {
            _fixture = fixture;
            _fixture.RepopulateData();
            _postRepository = new PostsRepository(_fixture.Context);
        }

        [Fact]
        public async Task GetPostsByCourseAsync_PassingCourseId_ReturnsPostListByCourseId()
        {
            var id = 10000;
            var postList = await _postRepository.GetPostsByCourseAsync(id);

            Assert.Equal(2, postList.Count());
            Assert.Contains(postList, t => t.PostId == 30000 && t.GetType() == typeof(TextPost));
            Assert.Contains(postList, t => t.PostId == 30003 && t.GetType() == typeof(FilePost));
        }

        [Theory]
        [InlineData(30000, typeof(TextPost))]
        [InlineData(30003, typeof(FilePost))]
        public async Task GetPostByIdAsync_PassingPostId_ReturnsPostsOfDifferentType(int postId, Type postType)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);

            Assert.NotNull(post);
            Assert.Equal(postId, post.PostId);
            Assert.Equal(postType, post.GetType());
        }

        [Theory]
        [InlineData(typeof(TextPost))]
        [InlineData(typeof(FilePost))]
        public async Task InsertPostAsync_PassingPost_InsertsPost(Type postType)
        {
            Post post;
            var id = 1;
            if (postType == typeof(TextPost))
                post = new TextPost(id, 1);
            else
                post = new FilePost(id, 1);
            await _postRepository.InsertPostAsync(post);

            Assert.Equal(5, _fixture.Context.Posts.Count());
            Assert.Contains(_fixture.Context.Posts, t => t.PostId == id && t.GetType() == postType);
        }

        [Theory]
        [InlineData(30000)] //TextPost
        [InlineData(30003)] //FilePost
        public async Task DeletePostByIdAsync_PassingPostId_DeletesPost(int postId)
        {
            await _postRepository.DeletePostByIdAsync(postId);

            Assert.Equal(3, _fixture.Context.Posts.Count());
            Assert.DoesNotContain(_fixture.Context.Posts, t => t.PostId == postId);
        }

        [Theory]
        [InlineData(30000)] //TextPost
        [InlineData(30003)] //FilePost
        public async Task DeletePostAsync_PassingPost_DeletesPost(int postId)
        {
            Assert.Contains(_fixture.Context.Posts, t => t.PostId == postId);
            var post = _fixture.Context.Posts.FirstOrDefault(t => t.PostId == postId);
            await _postRepository.DeletePostAsync(post);

            Assert.Equal(3, _fixture.Context.Posts.Count());
            Assert.DoesNotContain(_fixture.Context.Posts, t => t.PostId == postId);
        }

        [Fact]
        public async Task UpdateTextPostAsync_PassingTextPost_UpdatesTextPost()
        {
            var id = 30000;
            Assert.Contains(_fixture.Context.Posts, t => t.PostId == id);
            var postToUpdate = new TextPost(id, 1, "TestName", true, "TestContent");
            var post = (TextPost?)_fixture.Context.Posts.FirstOrDefault(t => t.PostId == id);
            Assert.NotNull(post);
            await _postRepository.UpdatePostAsync(post, postToUpdate);

            Assert.Equal(post.PostId, postToUpdate.PostId);
            Assert.Equal(post.Name, postToUpdate.Name);
            Assert.Equal(post.IsVisible, postToUpdate.IsVisible);
            Assert.Equal(post.TextContent, postToUpdate.TextContent);
        }

        [Fact]
        public async Task UpdateFilePostAsync_PassingFilePost_UpdatesFilePost()
        {
            var id = 30003;
            Assert.Contains(_fixture.Context.Posts, t => t.PostId == id);
            var postToUpdate = new FilePost(id, 1, "TestName", true, "TestName");
            var post = (FilePost?)_fixture.Context.Posts.FirstOrDefault(t => t.PostId == id);
            Assert.NotNull(post);
            await _postRepository.UpdatePostAsync(post, postToUpdate);

            Assert.Equal(post.PostId, postToUpdate.PostId);
            Assert.Equal(post.Name, postToUpdate.Name);
            Assert.Equal(post.IsVisible, postToUpdate.IsVisible);
            Assert.Equal(post.FileName, postToUpdate.FileName);
        }

        [Fact]
        public async Task GetPostsByCourseAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _postRepository.GetPostsByCourseAsync(null));
        }

        [Fact]
        public async Task GetPostByIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _postRepository.GetPostByIdAsync(null));
        }

        [Fact]
        public async Task InsertPostAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            Post? post = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() => _postRepository.InsertPostAsync(post));
        }

        [Fact]
        public async Task DeletePostByIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _postRepository.DeletePostByIdAsync(null));
        }

        [Fact]
        public async Task DeletePostAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            Post? post = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() => _postRepository.DeletePostAsync(post));
        }

        [Fact]
        public async Task UpdateTextPostAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _postRepository.UpdatePostAsync<TextPost>(null, null));
        }

        [Fact]
        public async Task UpdateFilePostAsync_PassingNullValue_ThrowsArgumentNullException()
        {

            await Assert.ThrowsAsync<ArgumentNullException>(() => _postRepository.UpdatePostAsync<FilePost>(null, null));
        }
    }
}
