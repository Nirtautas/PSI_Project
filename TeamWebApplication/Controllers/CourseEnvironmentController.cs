using Microsoft.AspNetCore.Mvc;
using TeamWebApplicationAPI.Data.ExtensionMethods;
using TeamWebApplicationAPI.Models;
using Newtonsoft.Json;
using AutoMapper;
using System.Diagnostics;
using TeamWebApplicationAPI.Data.Exceptions;
using System.Text;
using System.ComponentModel.Design;
using TeamWebApplicationAPI.Models.Enums;
using Microsoft.Extensions.Hosting;
using System.Collections.Specialized;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.StaticFiles;

namespace TeamWebApplication.Controllers
{
    public class CourseEnvironmentController : Controller
    {
        private readonly IMapper _mapper;

        public CourseEnvironmentController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int courseId)
        {
            int? loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
            HttpContext.Session.SetInt32("CurrentCourseId", courseId);

            var http = new HttpClient();
            var response = await http.GetAsync($"https://localhost:7107/api/ApiCourseEnvironment/ApiIndex?courseId={courseId}&loggedInUserId={loggedInUserId}");
            if (response.IsSuccessStatusCode)
            {
                var viewModelDto = JsonConvert.DeserializeObject<CourseAndCommentDto>(await response.Content.ReadAsStringAsync());
                var viewModel = _mapper.Map<CourseAndComment>(viewModelDto);
                //If you figure out a better way to do this I will literally buy you drinks
                //I dare you to map this fat ass model with abstract Post list
                //int hoursOfLifeWasted = 4;
                foreach (var tpost in viewModel.TextPostData)
                    viewModel.PostData.Add(tpost);
                foreach (var fpost in viewModel.FilePostData)
                    viewModel.PostData.Add(fpost);
                return View(viewModel);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int courseId, Comment comment)
        {
            int? loggedInUserId = HttpContext.Session.GetInt32Ex("LoggedInUserId");
            var commentDto = _mapper.Map<CommentDto>(comment);
            var http = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(commentDto), Encoding.UTF8, "application/json");
            var response = await http.PostAsync($"https://localhost:7107/api/ApiCourseEnvironment/ApiAddComment?courseId={courseId}&loggedInUserId={loggedInUserId}", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index", new { courseId });
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> EditComment(int courseId, int commentId, string userComment)
        {
            var http = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(userComment), Encoding.UTF8, "application/json");
            var response = await http.PutAsync($"https://localhost:7107/api/ApiCourseEnvironment/ApiEditComment?courseId={courseId}&commentId={commentId}", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index", new { courseId });
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int courseId, int commentId)
        {
                var http = new HttpClient();
                var response = await http.DeleteAsync($"https://localhost:7107/api/ApiCourseEnvironment/ApiDelete?commentId={commentId}");
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index", new { courseId });
                else
                    return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> CreateTextPost()
        {
            HttpContext.Session.GetInt32Ex("LoggedInUserId");
            int? currentCourseId = HttpContext.Session.GetInt32Ex("CurrentCourseId");
            var http = new HttpClient();
            var response = await http.GetAsync($"https://localhost:7107/api/ApiCourseEnvironment/ApiCreateTextPost?currentCourseId={currentCourseId}");
            if (response.IsSuccessStatusCode)
            {
                var post = JsonConvert.DeserializeObject<TextPostDto>(await response.Content.ReadAsStringAsync());
                var demapped = _mapper.Map<TextPost>(post);
                return View(demapped);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> CreateTextPost(TextPost post, int courseId)
        {
            var postDto = _mapper.Map<TextPostDto>(post);
            var http = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
            var response = await http.PostAsync($"https://localhost:7107/api/ApiCourseEnvironment/ApiCreateTextPost?courseId={courseId}", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index", new { courseId });
            else
                return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> EditTextPost(int postId)
        {
            HttpContext.Session.GetInt32Ex("LoggedInUserId");
            HttpContext.Session.GetInt32Ex("CurrentCourseId");
            var http = new HttpClient();
            var response = await http.GetAsync($"https://localhost:7107/api/ApiCourseEnvironment/ApiEditTextPost?postId={postId}");
            if (response.IsSuccessStatusCode)
            {
                var post = JsonConvert.DeserializeObject<TextPostDto>(await response.Content.ReadAsStringAsync());
                var demapped = _mapper.Map<TextPost>(post);
                return View(demapped);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> EditTextPost(TextPost post, int courseId)
        {
            var postDto = _mapper.Map<TextPostDto>(post);
            var http = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
            var response = await http.PutAsync($"https://localhost:7107/api/ApiCourseEnvironment/ApiEditTextPost", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index", new { courseId });
            else
                return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> DeleteTextPost(int postId)
        {
            HttpContext.Session.GetInt32Ex("LoggedInUserId");
            HttpContext.Session.GetInt32Ex("CurrentCourseId");
            var http = new HttpClient();
            var response = await http.GetAsync($"https://localhost:7107/api/ApiCourseEnvironment/ApiDeleteTextPost?postId={postId}");
            if (response.IsSuccessStatusCode)
            {
                var post = JsonConvert.DeserializeObject<TextPostDto>(await response.Content.ReadAsStringAsync());
                var demapped = _mapper.Map<TextPost>(post);
                return View(demapped);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTextPost(TextPost post, int courseId)
        {
                var http = new HttpClient();
                var response = await http.DeleteAsync($"https://localhost:7107/api/ApiCourseEnvironment/ApiDeleteTextPost?postId={post.PostId}");
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index", new { courseId });
                else
                    return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> CreateFilePost()
        {
            HttpContext.Session.GetInt32Ex("LoggedInUserId");
            int? currentCourseId = HttpContext.Session.GetInt32Ex("CurrentCourseId");
            var http = new HttpClient();
            var response = await http.GetAsync($"https://localhost:7107/api/ApiCourseEnvironment/ApiCreateFilePost?currentCourseId={currentCourseId}");
            if (response.IsSuccessStatusCode)
            {
                var post = JsonConvert.DeserializeObject<FilePostDto>(await response.Content.ReadAsStringAsync());
                var demapped = _mapper.Map<FilePost>(post);
                return View(demapped);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> CreateFilePost(int courseId, IFormFile file, FilePost post)
        {
            string? fileName, filePath = null;

            if (file != null && file.Length > 0)
            {
                fileName = Path.GetFileName(file.FileName);
                filePath = Path.Combine(@"..\TeamWebApplication\wwwroot\uploads", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Task fileCopy = file.CopyToAsync(stream);
                    post.PostType = PostType.File;
                    post.CreationDate = DateTime.Now;
                    post.FileName = fileName;
                    await fileCopy;
                }
            }

            var postDto = _mapper.Map<FilePostDto>(post);
            var http = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
            var response = await http.PostAsync($"https://localhost:7107/api/ApiCourseEnvironment/ApiCreateFilePost", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index", new { courseId });
            else
            {
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> EditFilePost(int postId)
        {
            HttpContext.Session.GetInt32Ex("LoggedInUserId");
            HttpContext.Session.GetInt32Ex("CurrentCourseId");
            var http = new HttpClient();
            var response = await http.GetAsync($"https://localhost:7107/api/ApiCourseEnvironment/ApiEditFilePost?postId={postId}");
            if (response.IsSuccessStatusCode)
            {
                var post = JsonConvert.DeserializeObject<FilePostDto>(await response.Content.ReadAsStringAsync());
                var demapped = _mapper.Map<FilePost>(post);
                return View(demapped);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> EditFilePost(FilePost post, int courseId, IFormFile file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(@"..\TeamWebApplication\wwwroot\uploads", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                Task fileCopy = file.CopyToAsync(stream);
                post.FileName = fileName;
                await fileCopy;
            }

            var postDto = _mapper.Map<FilePostDto>(post);
            var http = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
            var response = await http.PostAsync($"https://localhost:7107/api/ApiCourseEnvironment/ApiEditFilePost", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index", new { courseId });
            else
            {
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> DeleteFilePost(int postId)
        {
            HttpContext.Session.GetInt32Ex("LoggedInUserId");
            HttpContext.Session.GetInt32Ex("CurrentCourseId");
            var http = new HttpClient();
            var response = await http.GetAsync($"https://localhost:7107/api/ApiCourseEnvironment/ApiDeleteFilePost?postId={postId}");
            if (response.IsSuccessStatusCode)
            {
                var post = JsonConvert.DeserializeObject<FilePostDto>(await response.Content.ReadAsStringAsync());
                var demapped = _mapper.Map<FilePost>(post);
                return View(demapped);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFilePost(FilePost post, int courseId)
        {
            var http = new HttpClient();
            var response = await http.DeleteAsync($"https://localhost:7107/api/ApiCourseEnvironment/ApiDeleteFilePost?postId={post.PostId}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index", new { courseId });
            else
                return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> DownloadFile(IFormFile file, FilePost post)
        {
            var http = new HttpClient();
            var response = await http.GetAsync($"https://localhost:7107/api/ApiCourseEnvironment/ApiDownloadFile?postId={post.PostId}");
            if (response.IsSuccessStatusCode)
            {
                var fileDto = JsonConvert.DeserializeObject<FileDto>(await response.Content.ReadAsStringAsync());
                return File(fileDto.Data, fileDto.FileType, fileDto.FileName);
            }
            else
                return RedirectToAction("Error", "Home");
        }
    }
}