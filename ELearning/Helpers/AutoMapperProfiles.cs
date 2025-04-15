using AutoMapper;
using ELearning.Core.Models;
using ELearning.ViewModels;

namespace ELearning.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, UserProfile_ViewModel>();
            CreateMap<EditUserProfile_ViewModel, AppUser>();
            CreateMap<EditUserProfile_ViewModel, UserProfile_ViewModel>();
            CreateMap<AppUser, CourseCardInstructor_ViewModel>();

            CreateMap<Course, CourseCard_ViewModel>()
                .ForMember(dest => dest.LevelName, opt => opt.MapFrom(dest => dest.Level.Name))
                .ForMember(dest => dest.Instructor, opt => opt.MapFrom(dest => dest.Instructors.FirstOrDefault()));
        }
    }
}