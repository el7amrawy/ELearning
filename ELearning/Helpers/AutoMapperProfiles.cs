using AutoMapper;
using ELearning.Core.Models;
using ELearning.ViewModels;
using ELearning.Areas.Dashboard.ViewModels;
using ELearning.Core.Enums;
using ELearning.Areas.Instructor.ViewModels;
using ELearning.Core.DTOs;

namespace ELearning.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, UserProfile_ViewModel>();
            CreateMap<EditUserProfile_ViewModel, AppUser>();
            CreateMap<EditUserProfile_ViewModel, UserProfile_ViewModel>();

            /* Dashboard Area */
            CreateMap<AppUser, AdminProfile_ViewModel>();
            CreateMap<AdminProfile_ViewModel, AppUser>().ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<Category_ViewModel, Category>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
            CreateMap<EditCategory_ViewModel, Category>().ReverseMap();

            /* Instructor Area */
            CreateMap<CreateCourse_ViewModel, Course>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(_ => CourseStatusEnum.Draft));
            CreateMap<Course, Course_ViewModel>();
            CreateMap<EditCourse_ViewModel, Course>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<AppUser, Instructor_ViewModel>();
            CreateMap<Course, SectionCourse_ViewModel>();

            /* DTOs & Views */
            CreateMap<CreateSection_ViewModel,SectionDto>().ReverseMap();
        }
    }
}