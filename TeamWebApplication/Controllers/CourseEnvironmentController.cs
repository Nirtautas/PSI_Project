using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Data;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers
{
    public class CourseEnvironmentController : Controller
    {
        private readonly ICourseContainer _courseContainer;
        private readonly IPostContainer _postContainer;

        public CourseEnvironmentController(ICourseContainer courseContainer, IPostContainer postContainer)
        {
            _courseContainer = courseContainer;
            _postContainer = postContainer;
        }

        public IActionResult Index(int courseId)
        {
            _courseContainer.currentCourseId = courseId;
            IEnumerable<Post> coursePosts = (
                from post in _postContainer.postList
                where post.courseId == courseId
                select post
            );
            return View(coursePosts);
        }
    }
}
