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
            //_courseContainer.currentCourseId = courseId;
            Comment comment1 = new();
            IEnumerable<Post> coursePosts = (
                from post in _postContainer.postList
                where post.courseId == courseId
                select post
            ).ToList();

            IEnumerable<Comment> courseComments = (
                from comment in _commentContainer.CommentList
                where comment.CourseId == courseId
                orderby comment.CommentCreationTime descending
                select comment
            ).ToList();
            comment1.CourseId = courseId;
            var viewModel = new CourseAndComment
            {
                PostData = coursePosts,
                CommentData = courseComments,
                comment = comment1
            };
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult AddComment(int courseId,Comment comment)
        {
            _commentContainer.CreateComment(comment,courseId, _userContainer.loggedInUserId, _userContainer);

            return RedirectToAction("Index", new { courseId });
        }
    }
}