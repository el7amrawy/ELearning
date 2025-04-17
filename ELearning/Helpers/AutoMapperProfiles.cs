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

            CreateMap<Category, Category_ViewModel>();
            CreateMap<Course, CourseDetails_ViewModel>()
                .ForMember(dest => dest.Sections, opt => opt.MapFrom(src => src.Sections.OrderBy(l => l.Order)))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image.URL));
            CreateMap<Section, Section_ViewModel>()
                .ForMember(dest => dest.Lectures, opt => opt.MapFrom(src => src.Lectures.OrderBy(l => l.Order)));
            CreateMap<Lecture, Lecture_ViewModel>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Video.Duration));
        }
    }
}