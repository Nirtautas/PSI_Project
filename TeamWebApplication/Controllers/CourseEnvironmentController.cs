using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
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
				comment.CreationTime = DateTime.Now;

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

        public IActionResult CreateLinkPost()
        {
            try
            {
				HttpContext.Session.GetInt32Ex("LoggedInUserId");
				int? currentCourseId = HttpContext.Session.GetInt32Ex("CurrentCourseId");
				Post post = new LinkPost();
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
				post.CreationDate = DateTime.Now;

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

        [HttpPost]
        public IActionResult CreateLinkPost(LinkPost post, int courseId)
        {
            try
            {
				post.PostType = PostType.Link;
				post.CourseId = courseId;
				post.CreationDate = DateTime.Now;

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

        public IActionResult EditLinkPost(int postId)
        {

			try
			{
				HttpContext.Session.GetInt32Ex("LoggedInUserId");
				HttpContext.Session.GetInt32Ex("CurrentCourseId");
				LinkPost? post = (LinkPost?)_db.Posts.Find(postId);
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
				originalPost.TextContent = post.TextContent;

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

        [HttpPost]
        public IActionResult EditLinkPost(LinkPost post, int courseId)
        {
            try
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

        public IActionResult DeleteLinkPost(int postId)
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

		[HttpPost]
		public IActionResult DeleteLinkPost(LinkPost post, int courseId)
		{
			try
			{
				Post? originalPost = (LinkPost?)_db.Posts.Find(post.PostId);
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

        public IActionResult CreateFilePost()
        {
            try
            {
                HttpContext.Session.GetInt32Ex("LoggedInUserId");
                int? currentCourseId = HttpContext.Session.GetInt32Ex("CurrentCourseId");
                Post post = new FilePost();
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
        public async Task<IActionResult> CreateFilePost(int courseId, IFormFile file, FilePost post)
        {
            try
            {
				if (file != null && file.Length > 0)
				{
					var fileName = Path.GetFileName(file.FileName);
                    var fileExtension = Path.GetExtension(fileName);

					// creates a new file name to avoid having several files with the same name
					// (NOT IMPLEMENTED)
                    var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

                    // change 'fileName' to 'uniqueFileName' when unique file name recognition is implemented
                    var filePath = Path.Combine("wwwroot/uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

					post.PostType = PostType.File;
					post.CreationDate = DateTime.Now;

                    // change 'fileName' to 'uniqueFileName' when unique file name recognition is implemented
                    post.FileName = fileName;

                    _db.Posts.Add(post);
                    _db.SaveChanges();

				}
				return RedirectToAction("Index", new { courseId });
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
				throw;
            }
        }

		[HttpGet]
        public async Task<IActionResult> DownloadFile(IFormFile file, FilePost post)
		{
            Post? originalPost = (FilePost?)_db.Posts.Find(post.PostId);

            string? fileName = _db.Posts
				.Where(p => p.PostId == originalPost.PostId)
				.OfType<FilePost>()
				.Select(filePost => filePost.FileName)
				.FirstOrDefault();

            var filePath = Path.Combine("wwwroot/uploads", fileName);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contenttype, Path.GetFileName(filePath));
		}

		public IActionResult DeleteFilePost(int postId)
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
		public IActionResult DeleteFilePost(FilePost post, int courseId)
		{
			try
			{
				Post? originalPost = (FilePost?)_db.Posts.Find(post.PostId);
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