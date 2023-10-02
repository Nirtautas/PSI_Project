using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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
            _userContainer.currentCourseId = courseId;
            IEnumerable<Post> coursePosts = (
                from post in _postContainer.PostList
                where post.CourseId == courseId
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

        public IActionResult TeacherIndex(int courseId)
        {
            _userContainer.currentCourseId = courseId;
            IEnumerable<Post> coursePosts = (
                from post in _postContainer.PostList
                where post.CourseId == courseId
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

        public IActionResult TeacherVisitorIndex(int courseId)
        {
            _userContainer.currentCourseId = courseId;
            IEnumerable<Post> coursePosts = (
                from post in _postContainer.PostList
                where post.CourseId == courseId
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
            if (_userContainer.loggedInUserRole == Role.Student)
                return RedirectToAction("Index", new { courseId });
            return RedirectToAction("TeacherIndex", new { courseId });
        }

        [HttpPost]
        public IActionResult EditComment(int courseId, int commentId, String userComment)
        {
            Comment originalComment = _commentContainer.CommentList.SingleOrDefault(comment => comment.CommentId == commentId);
            originalComment.UserComment = userComment;
            originalComment.CommentCreationTime = DateTime.Now;
            _commentContainer.WriteComments();
            if (_userContainer.loggedInUserRole == Role.Student)
                return RedirectToAction("Index", new { courseId });
            return RedirectToAction("TeacherIndex", new { courseId });
        }

        [HttpPost]
        public IActionResult Delete(int courseId, int commentId)
        {
            Comment comment = _commentContainer.CommentList.SingleOrDefault(comment => comment.CommentId == commentId);
            _commentContainer.DeleteComment(comment);
            if (_userContainer.loggedInUserRole == Role.Student)
                return RedirectToAction("Index", new { courseId });
            return RedirectToAction("TeacherIndex", new { courseId });
        }

        public IActionResult CreateTextPost()
        {
            Post post = new TextPost();
            return View(post);
        }

        public IActionResult CreateLinkPost()
        {
            Post post = new LinkPost();
            return View(post);
        }

        [HttpPost]
        public IActionResult CreateTextPost(TextPost post, int courseId)
        {
            post.PostType = PostType.Text;
            _postContainer.CreatePost(post, _userContainer.currentCourseId);
            _postContainer.WritePosts();
            return RedirectToAction("TeacherIndex", new { courseId });
        }

        [HttpPost]
        public IActionResult CreateLinkPost(LinkPost post, int courseId)
        {
            post.PostType = PostType.Link;
            _postContainer.CreatePost(post, _userContainer.currentCourseId);
            _postContainer.WritePosts();
            return RedirectToAction("TeacherIndex", new { courseId });
        }

        public IActionResult EditTextPost(int postId)
        {
            TextPost? post = (TextPost)_postContainer.GetPost(postId);
            return View(post);
        }

        public IActionResult EditLinkPost(int postId)
        {
            LinkPost? post = (LinkPost)_postContainer.GetPost(postId);
            return View(post);
        }

        [HttpPost]
        public IActionResult EditTextPost(TextPost post, int courseId)
        {
            TextPost? originalPost = (TextPost)_postContainer.GetPost(post.PostId);
            originalPost.Name = post.Name;
            originalPost.IsVisible = post.IsVisible;
            originalPost.CreationDate = DateTime.Now;
            originalPost.PostType = post.PostType;
            originalPost.TextContent = post.TextContent;
            _postContainer.WritePosts();
            return RedirectToAction("TeacherIndex", new { courseId });
        }

        [HttpPost]
        public IActionResult EditLinkPost(LinkPost post, int courseId)
        {
            LinkPost? originalPost = (LinkPost)_postContainer.GetPost(post.PostId);
            originalPost.Name = post.Name;
            originalPost.IsVisible = post.IsVisible;
            originalPost.CreationDate = DateTime.Now;
            originalPost.PostType = post.PostType;
            originalPost.LinkContent = post.LinkContent;
            _postContainer.WritePosts();
            return RedirectToAction("TeacherIndex", new { courseId });
        }

        public IActionResult DeleteTextPost(int postId)
        {
            TextPost? post = (TextPost)_postContainer.GetPost(postId);
            return View(post);
        }

        public IActionResult DeleteLinkPost(int postId)
        {
            LinkPost? post = (LinkPost)_postContainer.GetPost(postId);
            return View(post);
        }

        [HttpPost]
        public IActionResult DeleteTextPost(TextPost post, int courseId)
        {
            TextPost originalPost = (TextPost)_postContainer.GetPost(post.PostId);
            _postContainer.DeletePost(originalPost);
            return RedirectToAction("TeacherIndex", new { courseId });
        }

        [HttpPost]
        public IActionResult DeleteLinkPost(LinkPost post, int courseId)
        {
            Post originalPost = (LinkPost)_postContainer.GetPost(post.PostId);
            _postContainer.DeletePost(originalPost);
            return RedirectToAction("TeacherIndex", new { courseId });
        }
    }
}