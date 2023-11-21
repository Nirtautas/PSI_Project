using Microsoft.EntityFrameworkCore;
using TeamWebApplication.Models.Enums;
using TeamWebApplication.Models;
using TeamWebApplication.Repositories;
using TeamWebApplication.Repositories.Interfaces;
using TeamWebApplicationTests.DatabaseFixture;

namespace TeamWebApplicationTests.Repositories.UsersRepositoryTests
{
    public class CommentsRepositoryTests : IClassFixture<DBFixture>
    {
        private readonly DBFixture _fixture;
        private readonly CommentsRepository _commentRepository;

        public CommentsRepositoryTests(DBFixture fixture)
        {
            _fixture = fixture;
            _fixture.RepopulateData();
            _commentRepository = new CommentsRepository(_fixture.Context);
        }

        [Fact]
        public async Task DeleteCommentByIdAsync_PassingCommentId_DeletesComment()
        {
            var id = 40000;
            Assert.Contains(_fixture.Context.Comments, t => t.CommentId == id);
            await _commentRepository.DeleteCommentByIdAsync(id);

            Assert.Equal(2, _fixture.Context.Comments.Count());
            Assert.DoesNotContain(_fixture.Context.Comments, t => t.CommentId == id);
        }

        [Fact]
        public async Task DeleteCommentAsync_PassingComment_DeletesComment()
        {
            var id = 40000;
            Assert.Contains(_fixture.Context.Comments, t => t.CommentId == id);
            var comment = await _fixture.Context.Comments.SingleOrDefaultAsync(t => t.CommentId == id);
            await _commentRepository.DeleteCommentAsync(comment);

            Assert.Equal(2, _fixture.Context.Comments.Count());
            Assert.DoesNotContain(_fixture.Context.Comments, t => t.CommentId == id);
        }

        [Fact]
        public async Task GetCommentByIdAsync_PassingCommentId_GettingComment()
        {
            var id = 40000;
            var comment = await _commentRepository.GetCommentByIdAsync(id);

            Assert.NotNull(comment);
            Assert.Equal(comment.CommentId, id);
        }

        [Fact]
        public async Task GetCommentsByCourseIdAsync_PassingCourseId_GettingCommentList()
        {
            var id = 10000;
            var commentList = await _commentRepository.GetCommentsByCourseIdAsync(id);

            Assert.Equal(2, commentList.Count());
            Assert.Contains(commentList, t => t.CommentId == 40000);
            Assert.Contains(commentList, t => t.CommentId == 40002);
        }

        [Fact]
        public async Task InsertCommentAsync_PassingComment_InsertsComment()
        {
            var id = 1;
            var comment = new Comment(1, 2, 2);
            await _commentRepository.InsertCommentAsync(comment);

            Assert.Equal(4, _fixture.Context.Comments.Count());
            Assert.Contains(_fixture.Context.Comments, t => t.CommentId == id);
        }

        [Fact]
        public async Task UpdateCommentAsync_PassingComment_UpdatesComment()
        {
            var id = 40000;
            var commentData = "TestComment";
            Assert.Contains(_fixture.Context.Comments, t => t.CommentId == id);
            await _commentRepository.UpdateCommentAsync(id, commentData);
            var comment = await _fixture.Context.Comments.SingleOrDefaultAsync(t => t.CommentId == id);

            Assert.NotNull(comment);
            Assert.Equal(comment.CommentId, id);
            Assert.Equal(comment.UserComment, commentData);
        }

        [Fact]
        public async Task DeleteCommentByIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _commentRepository.DeleteCommentByIdAsync(null));
        }

        [Fact]
        public async Task DeleteCommentAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _commentRepository.DeleteCommentAsync(null));
        }

        [Fact]
        public async Task GetCommentByIdAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _commentRepository.GetCommentByIdAsync(null));
        }

        [Fact]
        public async Task InsertCommentAsync_PassingNullValue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _commentRepository.InsertCommentAsync(null));
        }

        [Theory]
        [InlineData(1, null)]
        [InlineData(null, "a")]
        [InlineData(null, null)]
        public async Task UpdateCommentAsync_PassingNullValue_ThrowsArgumentNullException(int? commentId, string? comment)
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _commentRepository.UpdateCommentAsync(commentId, comment));
        }
    }
}
