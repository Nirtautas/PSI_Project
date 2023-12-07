using AutoMapper;
using TeamWebApplicationAPI.Models;

namespace TeamWebApplicationAPI.Data.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Course, CourseDto>();
            CreateMap<CourseDto, Course>();
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();

            CreateMap<TextPost, TextPostDto>();
            CreateMap<TextPostDto, TextPost>();

            CreateMap<FilePostDto, FilePost>();
            CreateMap<FilePost, FilePostDto>();

            CreateMap<CourseUser, CourseUserDto>();
            CreateMap<CourseUserDto, CourseUser>();
            CreateMap<CourseAndCommentDto, CourseAndComment>();
            CreateMap<CourseAndComment, CourseAndCommentDto>();
            CreateMap<CourseViewModelDto, CourseViewModel>();
            CreateMap<CourseViewModel, CourseViewModelDto>();
        }
    }
}
