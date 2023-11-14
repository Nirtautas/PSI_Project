using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Data.ExceptionLogger;
using TeamWebApplication.Data.Exceptions;
using TeamWebApplication.Data.ExtensionMethods;
using TeamWebApplication.Models;
using TeamWebApplication.Models.Enums;
using TeamWebApplication.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TeamWebApplication.Controllers
{
    public class CourseEnvironmentController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPostsRepository _postsRepository;
        private readonly ICommentsRepository _commentsRepository;
        private readonly IDataLogger _logger;

        public CourseEnvironmentController(IDataLogger logger, IUsersRepository usersRepository,
            IPostsRepository postsRepository, ICommentsRepository commentsRepository)
        {
            _logger = logger;
            _usersRepository = usersRepository;
            _postsRepository = postsRepository;
            _commentsRepository = commentsRepository;
        }

        public async Task<IActionResult> Index(int courseId)
        {
            try
            {
                var loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
                HttpContext.Session.SetInt32("CurrentCourseId", courseId);

                var coursePosts = await _postsRepository.GetPostsByCourseAsync(courseId);

                Comment comment1 = new();
                comment1.CourseId = courseId;
                var courseComments = await _commentsRepository.GetCommentsByCourseIdAsync(courseId);
                var currentUser = await _usersRepository.GetUserByIdAsync(loggedInUserId);

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

        public async Task<IActionResult> TeacherVisitorIndex(int courseId)
        {
            try
            {
                var loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
                HttpContext.Session.SetInt32("CurrentCourseId", courseId);

                var coursePosts = await _postsRepository.GetPostsByCourseAsync(courseId);

                Comment comment1 = new();
                comment1.CourseId = courseId;
                var courseComments = await _commentsRepository.GetCommentsByCourseIdAsync(courseId);
                var currentUser = await _usersRepository.GetUserByIdAsync(loggedInUserId);

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
        public async Task<IActionResult> AddComment(int courseId, Comment comment)
        {
            try
            {
                var loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
                var user = await _usersRepository.GetUserByIdAsync(loggedInUserId);

                comment.CourseId = courseId;
                comment.UserId = user.UserId;
                comment.CommentatorName = user.Name;
                comment.CommentatorSurname = user.Surname;

                await _commentsRepository.InsertCommentAsync(comment);
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
        public async Task<IActionResult> EditComment(int courseId, int commentId, String userComment)
        {
            try
            {
                var originalComment = await _commentsRepository.GetCommentByIdAsync(commentId);
                if (originalComment != null)
                {
                    if (originalComment.UserComment != userComment)
                    {
                        originalComment.CreationTime = DateTime.Now;
                    }
                    originalComment.UserComment = userComment;
                    await _commentsRepository.UpdateCommentAsync(originalComment);
                }
                return RedirectToAction("Index", new { courseId });
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int courseId, int commentId)
        {
            try
            {
                var comment = await _commentsRepository.GetCommentByIdAsync(commentId);
                await _commentsRepository.DeleteCommentAsync(comment);
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
                var currentCourseId = HttpContext.Session.GetInt32Ex("CurrentCourseId");
                Post post = new TextPost();
                post.CourseId = currentCourseId;
                return View(post);
            }
            catch (SessionCredentialException ex)
            {
                _logger.Log(ex);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTextPost(TextPost post, int courseId)
        {
            try
            {
                post.PostType = PostType.Text;
                post.CourseId = courseId;
                await _postsRepository.InsertPostAsync(post);

                return RedirectToAction("Index", new { courseId });
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }

        public async Task<IActionResult> EditTextPost(int postId)
        {
            try
            {
                HttpContext.Session.GetInt32Ex("LoggedInUserId");
                HttpContext.Session.GetInt32Ex("CurrentCourseId");
                var post = (TextPost?) await _postsRepository.GetPostByIdAsync(postId);
                return View(post);
            }
            catch (SessionCredentialException ex)
            {
                _logger.Log(ex);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditTextPost(TextPost post, int courseId)
        {
            try
            {
                var originalPost = (TextPost?) await _postsRepository.GetPostByIdAsync(post.PostId);
                if (originalPost.TextContent != post.TextContent || originalPost.Name != post.Name)
                {
                    originalPost.CreationDate = DateTime.Now;
                }
                originalPost.Name = post.Name;
                originalPost.IsVisible = post.IsVisible;
                originalPost.PostType = post.PostType;
                originalPost.TextContent = post.TextContent;

                await _postsRepository.UpdateTextPostAsync(originalPost);
                return RedirectToAction("Index", new { courseId });
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }

        public async Task<IActionResult> DeleteTextPost(int postId)
        {
            try
            {
                HttpContext.Session.GetInt32Ex("LoggedInUserId");
                HttpContext.Session.GetInt32Ex("CurrentCourseId");
                var post = await _postsRepository.GetPostByIdAsync(postId);
                return View(post);
            }
            catch (SessionCredentialException ex)
            {
                _logger.Log(ex);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTextPost(TextPost post, int courseId)
        {
            try
            {
                var originalPost = (TextPost?) await _postsRepository.GetPostByIdAsync(post.PostId);
                await _postsRepository.DeletePostAsync(originalPost);
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
                var currentCourseId = HttpContext.Session.GetInt32Ex("CurrentCourseId");
                Post post = new FilePost();
                post.CourseId = currentCourseId;
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
                        Task fileCopy = file.CopyToAsync(stream);

                        post.PostType = PostType.File;
                        post.CreationDate = DateTime.Now;

                        // change 'fileName' to 'uniqueFileName' when unique file name recognition is implemented
                        post.FileName = fileName;

                        await fileCopy;
                        await _postsRepository.InsertPostAsync(post);
                    }
                }
                return RedirectToAction("Index", new { courseId });
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }

        public async Task<IActionResult> DownloadFile(IFormFile file, FilePost post)
        {
            try
            {
                var originalPost = (FilePost?) await _postsRepository.GetPostByIdAsync(post.PostId);
                var fileName = originalPost.FileName;

                var filePath = Path.Combine("wwwroot/uploads", fileName);

                var provider = new FileExtensionContentTypeProvider();
                provider.TryGetContentType(filePath, out string? contentType);
                var bytes = await System.IO.File.ReadAllBytesAsync(filePath);

                return File(bytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }

        public async Task<IActionResult> DeleteFilePost(int postId)
        {
            try
            {
                HttpContext.Session.GetInt32Ex("LoggedInUserId");
                HttpContext.Session.GetInt32Ex("CurrentCourseId");
                var post = await _postsRepository.GetPostByIdAsync(postId);
                return View(post);
            }
            catch (SessionCredentialException ex)
            {
                _logger.Log(ex);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFilePost(FilePost post, int courseId)
        {
            try
            {
                var originalPost = (FilePost?) await _postsRepository.GetPostByIdAsync(post.PostId);
                var filePath = Path.Combine("wwwroot/uploads", originalPost.FileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                await _postsRepository.DeletePostAsync(post);
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