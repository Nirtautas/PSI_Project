using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Data;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICourseContainer _courseContainer;
        private readonly ICommentContainer _commentContainer;

        public CommentController(ICommentContainer commentContainer, ICourseContainer courseContainer)
        {
            _commentContainer = commentContainer;
            _courseContainer = courseContainer;
        }

        public IActionResult Index(int courseId)
        { 
            _courseContainer.currentCourseId = courseId;
            IEnumerable<Comment> courseComments = (
                from comment in _commentContainer.CommentList
                where comment.CourseId == courseId
                orderby comment.CommentCreationTime descending
                select comment
            );
            return View(courseComments);
        }
    }
}