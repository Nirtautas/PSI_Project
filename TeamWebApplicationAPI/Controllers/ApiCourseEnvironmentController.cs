﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TeamWebApplicationAPI.Data.ExceptionLogger;
using TeamWebApplicationAPI.Data.Exceptions;
using TeamWebApplicationAPI.Data.ExtensionMethods;
using TeamWebApplicationAPI.Models;
using TeamWebApplicationAPI.Models.Enums;
using TeamWebApplicationAPI.Repositories.Interfaces;

namespace TeamWebApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/ApiCourseEnvironment")]
    public class ApiCourseEnvironmentController : ControllerBase
    {
        private readonly IDataLogger _logger;
        private readonly IMapper _mapper;
        private readonly IUsersRepository _usersRepository;
        private readonly IPostsRepository _postsRepository;
        private readonly ICommentsRepository _commentsRepository;
        private readonly ICourseUsersRepository _courseUsersRepository;

        public ApiCourseEnvironmentController(IDataLogger logger, IMapper mapper,
            IUsersRepository usersRepository, IPostsRepository postsRepository, ICommentsRepository commentsRepository, ICourseUsersRepository courseUsersRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _usersRepository = usersRepository;
            _postsRepository = postsRepository;
            _commentsRepository = commentsRepository;
            _courseUsersRepository = courseUsersRepository;
        }

        [HttpGet("ApiIndex")]
        public async Task<IActionResult> ApiIndex([FromQuery] int courseId, [FromQuery] int? loggedInUserId)
        {
            try
            {
                var coursePosts = await _postsRepository.GetPostsByCourseAsync(courseId);

                var courseComments = await _commentsRepository.GetCommentsByCourseIdAsync(courseId);
                var currentUser = await _usersRepository.GetUserByIdAsync(loggedInUserId);
                var courseUserData = await _courseUsersRepository.GetRelationsByUserIdAsync(loggedInUserId);

                var viewModel = new CourseAndComment
                {
                    TextPostData = new List<TextPost>(),
                    FilePostData = new List<FilePost>(),
                    CommentData = courseComments,
                    Comment = new Comment(courseId),
                    LoggedInUser = (int)loggedInUserId,
                    User = currentUser,
                    CourseUserData = courseUserData
                };
                foreach (var post in coursePosts)
                {
                    if (post.PostType == PostType.Text)
                        viewModel.TextPostData.Add((TextPost)post);
                    else if (post.PostType == PostType.File)
                        viewModel.FilePostData.Add((FilePost)post);
                }
                var viewModelDto = _mapper.Map<CourseAndCommentDto>(viewModel);
                return Ok(viewModelDto);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Unauthorized();
            }
        }

        [HttpPost("ApiAddComment")]
        public async Task<IActionResult> ApiAddComment([FromBody] CommentDto commentDto, [FromQuery] int courseId, [FromQuery] int? loggedInUserId)
        {
            try
            {
                var user = await _usersRepository.GetUserByIdAsync(loggedInUserId);

                var comment = _mapper.Map<Comment>(commentDto);
                comment.CourseId = courseId;
                comment.UserId = user.UserId;
                comment.CommentatorName = user.Name;
                comment.CommentatorSurname = user.Surname;

                await _commentsRepository.InsertCommentAsync(comment);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Unauthorized();
            }
        }

        [HttpPut("ApiEditComment")]
        public async Task<IActionResult> ApiEditComment([FromQuery] int courseId, [FromQuery] int commentId, [FromBody] string userComment)
        {
            try
            {
                await _commentsRepository.UpdateCommentAsync(commentId, userComment);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Unauthorized();
            }
        }

        [HttpDelete("ApiDelete")]
        public async Task<IActionResult> ApiDelete(int commentId)
        {
            try
            {
                var comment = await _commentsRepository.GetCommentByIdAsync(commentId);
                await _commentsRepository.DeleteCommentAsync(comment);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Unauthorized();
            }
        }

        [HttpGet("ApiCreateTextPost")]
        public IActionResult CreateTextPost([FromQuery] int? currentCourseId)
        {
            try
            {
                TextPost post = new();
                post.CourseId = (int)currentCourseId;
                var map = _mapper.Map<TextPostDto>(post);
                return Ok(post);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Unauthorized();
            }
        }

        [HttpPost("ApiCreateTextPost")]
        public async Task<IActionResult> ApiCreateTextPost([FromBody] TextPostDto postDto, [FromQuery] int courseId)
        {
            try
            {
                var post = _mapper.Map<TextPost>(postDto);
                post.PostType = PostType.Text;
                post.CourseId = courseId;
                await _postsRepository.InsertPostAsync(post);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Unauthorized();
            }
        }

        [HttpGet("ApiEditTextPost")]
        public async Task<IActionResult> ApiEditTextPost([FromQuery] int postId)
        {
            try
            {
                var post = (TextPost?)await _postsRepository.GetPostByIdAsync(postId);
                var map = _mapper.Map<TextPostDto>(post);
                return Ok(map);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Unauthorized();
            }
        }

        [HttpPut("ApiEditTextPost")]
        public async Task<IActionResult> ApiEditTextPost([FromBody] TextPostDto postDto)
        {
            try
            {
                var post = _mapper.Map<TextPost>(postDto);
                var originalPost = (TextPost?)await _postsRepository.GetPostByIdAsync(post.PostId);
                if (originalPost != null && (originalPost.TextContent != post.TextContent || originalPost.Name != post.Name))
                    await _postsRepository.UpdateAndSaveDelegate(originalPost, post);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Unauthorized();
            }
        }

        [HttpGet("ApiDeleteTextPost")]
        public async Task<IActionResult> ApiDeleteTextPost([FromQuery] int postId)
        {
            try
            {
                var post = (TextPost?)await _postsRepository.GetPostByIdAsync(postId);
                var map = _mapper.Map<TextPostDto>(post);
                return Ok(map);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Unauthorized();
            }
        }

        [HttpDelete("ApiDeleteTextPost")]
        public async Task<IActionResult> ApiDeleteTextPostDel([FromQuery] int postId)
        {
            try
            {
                var post = (TextPost?)await _postsRepository.GetPostByIdAsync(postId);
                await _postsRepository.DeletePostAsync(post);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Unauthorized();
            }
        }

        [HttpGet("ApiCreateFilePost")]
        public IActionResult ApiCreateFilePost([FromQuery] int? currentCourseId)
        {
            try
            {
                FilePost post = new();
                post.CourseId = (int)currentCourseId;
                return Ok(post);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Unauthorized();
            }
        }

        [HttpGet("ApiEditFilePost")]
        public async Task<IActionResult> ApiEditFilePost([FromQuery] int? postId)
        {
            try
            {
                var post = (FilePost?)await _postsRepository.GetPostByIdAsync(postId);
                var map = _mapper.Map<FilePostDto>(post);
                return Ok(map);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return Unauthorized();
            }
        }

        [HttpGet("ApiDeleteFilePost")]
        public async Task<IActionResult> ApiDeleteFilePost([FromQuery] int? postId)
        {
                try
                {
                    var post = (FilePost?)await _postsRepository.GetPostByIdAsync(postId);
                    var map = _mapper.Map<FilePostDto>(post);
                    return Ok(map);
                }
                catch (Exception ex)
                {
                    _logger.Log(ex);
                    return Unauthorized(ex);
                }
        }
    }
}
