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
            CreateMap<CourseViewModelDto, CourseViewModel>();
            CreateMap<CourseViewModel, CourseViewModelDto>();
        }
    }
}
