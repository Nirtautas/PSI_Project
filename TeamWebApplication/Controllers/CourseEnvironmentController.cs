using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Npgsql.PostgresTypes;
using TeamWebApplication.Data;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers
{
    public class CourseEnvironmentController : Controller
    {
        private readonly IUserContainer _userContainer;
        private readonly ApplicationDBContext _db;

        public CourseEnvironmentController(IUserContainer userContainer, ApplicationDBContext db)
        {
            _db = db;
            _userContainer = userContainer;
        }

        public IActionResult Index(int courseId)
        {
			_userContainer.currentCourseId = courseId;
			IEnumerable<Post> coursePosts = (
                from post in _db.Posts
                where post.CourseId == courseId
                select post
            ).ToList();

            Comment comment1 = new();
            IEnumerable<Comment> courseComments = (
                from comment in _db.Comments
                where comment.CourseId == courseId
                orderby comment.CreationTime descending
                select comment
            ).ToList();
            comment1.CourseId = courseId;
            int loggedInUser = _userContainer.loggedInUserId;

            User currentUser = _db.Users.Find(_userContainer.loggedInUserId);

            var viewModel = new CourseAndComment
            {
                PostData = coursePosts,
                CommentData = courseComments,
                comment = comment1,
                LoggedInUser = loggedInUser,
                User = currentUser
            };
            return View(viewModel);
        }

        public IActionResult TeacherVisitorIndex(int courseId)
        {
            _userContainer.currentCourseId = courseId;
            IEnumerable<Post> coursePosts = (
                from post in _db.Posts
                where post.CourseId == courseId
                select post
            ).ToList();

            Comment comment1 = new();
            IEnumerable<Comment> courseComments = (
                from comment in _db.Comments
                where comment.CourseId == courseId
                orderby comment.CreationTime descending
                select comment
            ).ToList();
            comment1.CourseId = courseId;
            int loggedInUser = _userContainer.loggedInUserId;

            User currentUser = _db.Users.Find(_userContainer.loggedInUserId);

            var viewModel = new CourseAndComment
            {
                PostData = coursePosts,
                CommentData = courseComments,
                comment = comment1,
                LoggedInUser = loggedInUser,
                User = currentUser
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddComment(int courseId, Comment comment)
        {
			var user = _db.Users.Find(_userContainer.loggedInUserId);

			comment.CourseId = courseId;
			comment.UserId = user.UserId;
			comment.CommentatorName = user.Name;
			comment.CommentatorSurname = user.Surname;
            comment.CreationTime = DateTime.Now;

			_db.Comments.Add(comment);
            _db.SaveChanges();
            return RedirectToAction("Index", new { courseId });
        }

        [HttpPost]
        public IActionResult EditComment(int courseId, int commentId, String userComment)
        {
            var originalComment = _db.Comments.Find(commentId);

            originalComment.UserComment = userComment;
            originalComment.CreationTime = DateTime.Now;

            _db.Comments.Update(originalComment);
            _db.SaveChanges();
            return RedirectToAction("Index", new { courseId });
        }

        [HttpPost]
        public IActionResult Delete(int courseId, int commentId)
        {
            var comment = _db.Comments.Find(commentId);

            _db.Remove(comment);
            _db.SaveChanges();
            return RedirectToAction("Index", new { courseId });
        }

        public IActionResult CreateTextPost()
        {
            Post post = new TextPost();
            post.CourseId = _userContainer.currentCourseId;
            return View(post);
        }

        public IActionResult CreateLinkPost()
        {
            Post post = new LinkPost();
            post.CourseId = _userContainer.currentCourseId;
            return View(post);
        }

        [HttpPost]
        public IActionResult CreateTextPost(TextPost post, int courseId)
        {
            post.PostType = PostType.Text;
			post.CourseId = courseId;
			post.CreationDate = DateTime.Now;
			
            _db.Posts.Add(post);
            _db.SaveChanges();
            return RedirectToAction("Index", new { courseId });
        }

        [HttpPost]
        public IActionResult CreateLinkPost(LinkPost post, int courseId)
        {
            post.PostType = PostType.Link;
			post.CourseId = courseId;
			post.CreationDate = DateTime.Now;

			_db.Posts.Add(post);
			_db.SaveChanges();
			return RedirectToAction("Index", new { courseId });
        }

        public IActionResult EditTextPost(int postId)
        {
            TextPost? post = (TextPost)_db.Posts.Find(postId);
            return View(post);
        }

        public IActionResult EditLinkPost(int postId)
        {
			LinkPost? post = (LinkPost)_db.Posts.Find(postId);
			return View(post);
        }

        [HttpPost]
        public IActionResult EditTextPost(TextPost post, int courseId)
        {
            TextPost? originalPost = (TextPost)_db.Posts.Find(post.PostId);

            originalPost.Name = post.Name;
            originalPost.IsVisible = post.IsVisible;
            originalPost.CreationDate = DateTime.Now;
            originalPost.PostType = post.PostType;
            originalPost.TextContent = post.TextContent;

            _db.Posts.Update(originalPost);
            _db.SaveChanges();
            return RedirectToAction("Index", new { courseId });
        }

        [HttpPost]
        public IActionResult EditLinkPost(LinkPost post, int courseId)
        {
            LinkPost? originalPost = (LinkPost)_db.Posts.Find(post.PostId);

            originalPost.Name = post.Name;
            originalPost.IsVisible = post.IsVisible;
            originalPost.CreationDate = DateTime.Now;
            originalPost.PostType = post.PostType;
            originalPost.LinkContent = post.LinkContent;

			_db.Posts.Update(originalPost);
			_db.SaveChanges();
			return RedirectToAction("Index", new { courseId });
        }

        public IActionResult DeleteTextPost(int postId)
        {
            var post = _db.Posts.Find(postId);
            return View(post);
        }

        public IActionResult DeleteLinkPost(int postId)
        {
			var post = _db.Posts.Find(postId);
			return View(post);
        }

        [HttpPost]
        public IActionResult DeleteTextPost(TextPost post, int courseId)
        {
			TextPost originalPost = (TextPost)_db.Posts.Find(post.PostId);
            _db.Posts.Remove(originalPost);
            _db.SaveChanges();
            return RedirectToAction("Index", new { courseId });
        }

		[HttpPost]
		public IActionResult DeleteLinkPost(LinkPost post, int courseId)
		{
			Post originalPost = (LinkPost)_db.Posts.Find(post.PostId);
			_db.Posts.Remove(originalPost);
			_db.SaveChanges();
			return RedirectToAction("Index", new { courseId });
        }
	}
}