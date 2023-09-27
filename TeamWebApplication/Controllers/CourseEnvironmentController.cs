using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Data;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers
{
    public class CourseEnvironmentController : Controller
    {
        private readonly IPostContainer _postContainer;
        private readonly ICommentContainer _commentContainer;
        private readonly IUserContainer _userContainer;

        public CourseEnvironmentController(IPostContainer postContainer, ICommentContainer commentContainer, IUserContainer userContainer)
        {
            _postContainer = postContainer;
            _commentContainer = commentContainer;
            _userContainer = userContainer;
        }

        public IActionResult Index(int courseId)
        {
            IEnumerable<Post> coursePosts = (
                from post in _postContainer.postList
                where post.courseId == courseId
                select post
            ).ToList();

            Comment comment1 = new();
            IEnumerable<Comment> courseComments = (
                from comment in _commentContainer.CommentList
                where comment.CourseId == courseId
                orderby comment.CommentCreationTime descending
                select comment
            ).ToList();
            comment1.CourseId = courseId;
            int loggedInUser = _userContainer.loggedInUserId;

            var viewModel = new CourseAndComment
            {
                PostData = coursePosts,
                CommentData = courseComments,
                comment = comment1,
                LoggedInUser = loggedInUser
            };
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult AddComment(int courseId, Comment comment)
        {
            _commentContainer.CreateComment(comment, courseId, _userContainer.loggedInUserId, _userContainer);
            return RedirectToAction("Index", new { courseId });
        }

        [HttpPost]
        public IActionResult EditComment(int courseId, int commentId, String userComment)
        {
            Console.WriteLine(courseId);
            Console.WriteLine(commentId);
            Console.WriteLine(userComment);
            Comment originalComment = _commentContainer.CommentList.SingleOrDefault(comment => comment.CommentId == commentId);
            originalComment.UserComment = userComment;
            originalComment.CommentCreationTime = DateTime.Now;
            _commentContainer.WriteComments();
            return RedirectToAction("Index", new { courseId });
        }

        [HttpPost]
        public IActionResult Delete(int courseId, int commentId)
        {
            Comment comment = _commentContainer.CommentList.SingleOrDefault(comment => comment.CommentId == commentId);
            _commentContainer.DeleteComment(comment);
            return RedirectToAction("Index", new { courseId });
        }
    }
}