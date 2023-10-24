using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Data.ExceptionLogger;
using TeamWebApplication.Data.Exceptions;
using TeamWebApplication.Data.ExtensionMethods;
using TeamWebApplication.Models;
using TeamWebApplication.Models.Enums;

namespace TeamWebApplication.Controllers
{
    public class CourseEnvironmentController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly IExceptionLogger _logger;

        public CourseEnvironmentController(ApplicationDBContext db, IExceptionLogger logger)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index(int courseId)
        {
            try
            {
                int? loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
                HttpContext.Session.SetInt32("CurrentCourseId", courseId);
                _db.SaveChanges();
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

                User currentUser = _db.Users.Find(loggedInUserId);

                var viewModel = new CourseAndComment
                {
                    PostData = coursePosts,
                    CommentData = courseComments,
                    comment = comment1,
                    LoggedInUser = (int)loggedInUserId,
                    User = currentUser
                };
                return View(viewModel);
            }
            catch (SessionCredentialException ex)
            {
                _logger.Log(ex);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex1)
            {
                _logger.Log(ex1);
                throw;
            }
        }

        public IActionResult TeacherVisitorIndex(int courseId)
        {
            try
            {
                int? loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
                HttpContext.Session.GetInt32Ex("LoggedInUserId");
                HttpContext.Session.SetInt32("CurrentCourseId", courseId);
                _db.SaveChanges();
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
                User currentUser = _db.Users.Find(loggedInUserId);

                var viewModel = new CourseAndComment
                {
                    PostData = coursePosts,
                    CommentData = courseComments,
                    comment = comment1,
                    LoggedInUser = (int)loggedInUserId,
                    User = currentUser
                };
                return View(viewModel);
            }
            catch (SessionCredentialException ex)
            {
                _logger.Log(ex);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex1)
            {
                _logger.Log(ex1);
                throw;
            }
        }

        [HttpPost]
        public IActionResult AddComment(int courseId, Comment comment)
        {
            try
            {
                int? loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
                var user = _db.Users.Find(loggedInUserId);

                comment.CourseId = courseId;
                comment.UserId = user.UserId;
                comment.CommentatorName = user.Name;
                comment.CommentatorSurname = user.Surname;

                _db.Comments.Add(comment);
                _db.SaveChanges();
                return RedirectToAction("Index", new { courseId });
            }
            catch (SessionCredentialException ex)
            {
                _logger.Log(ex);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex1)
            {
                _logger.Log(ex1);
                throw;
            }
        }

        [HttpPost]
        public IActionResult EditComment(int courseId, int commentId, String userComment)
        {
            try
            {
                var originalComment = _db.Comments.Find(commentId);

                originalComment.UserComment = userComment;
                originalComment.CreationTime = DateTime.Now;

                _db.Comments.Update(originalComment);
                _db.SaveChanges();
                return RedirectToAction("Index", new { courseId });
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }

        [HttpPost]
        public IActionResult Delete(int courseId, int commentId)
        {
            try
            {
                var comment = _db.Comments.Find(commentId);

                _db.Remove(comment);
                _db.SaveChanges();
                return RedirectToAction("Index", new { courseId });
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }

        public IActionResult CreateTextPost()
        {
            try
            {
                HttpContext.Session.GetInt32Ex("LoggedInUserId");
                int? currentCourseId = HttpContext.Session.GetInt32Ex("CurrentCourseId");
                Post post = new TextPost();
                post.CourseId = (int)currentCourseId;
                return View(post);
            }
            catch (SessionCredentialException ex)
            {
                _logger.Log(ex);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult CreateTextPost(TextPost post, int courseId)
        {
            try
            {
                post.PostType = PostType.Text;
                post.CourseId = courseId;
                post.TextContent = LinkValidation.ValidateAndReplaceLinks(post.TextContent);

                _db.Posts.Add(post);
                _db.SaveChanges();
                return RedirectToAction("Index", new { courseId });
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }

        public IActionResult EditTextPost(int postId)
        {
            try
            {
                HttpContext.Session.GetInt32Ex("LoggedInUserId");
                HttpContext.Session.GetInt32Ex("CurrentCourseId");
                TextPost? post = (TextPost?)_db.Posts.Find(postId);
                return View(post);
            }
            catch (SessionCredentialException ex)
            {
                _logger.Log(ex);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult EditTextPost(TextPost post, int courseId)
        {
            try
            {
                TextPost? originalPost = (TextPost?)_db.Posts.Find(post.PostId);

                originalPost.Name = post.Name;
                originalPost.IsVisible = post.IsVisible;
                originalPost.CreationDate = DateTime.Now;
                originalPost.PostType = post.PostType;
                originalPost.TextContent = LinkValidation.ValidateAndReplaceLinks(post.TextContent);

                _db.Posts.Update(originalPost);
                _db.SaveChanges();
                return RedirectToAction("Index", new { courseId });
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }

        public IActionResult DeleteTextPost(int postId)
        {
            try
            {
                HttpContext.Session.GetInt32Ex("LoggedInUserId");
                HttpContext.Session.GetInt32Ex("CurrentCourseId");
                var post = _db.Posts.Find(postId);
                return View(post);
            }
            catch (SessionCredentialException ex)
            {
                _logger.Log(ex);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult DeleteTextPost(TextPost post, int courseId)
        {
            try
            {
                TextPost? originalPost = (TextPost?)_db.Posts.Find(post.PostId);
                _db.Posts.Remove(originalPost);
                _db.SaveChanges();
                return RedirectToAction("Index", new { courseId });
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }
    }
}