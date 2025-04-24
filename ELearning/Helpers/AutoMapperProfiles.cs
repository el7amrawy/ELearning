using AutoMapper;
using ELearning.Core.DTOs;
using ELearning.Core.Enums;
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
            CreateMap<AppUser, CourseDetailsInstructor_ViewModel>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image.URL))
                .ForMember(dest => dest.CourseCount, opt => opt.MapFrom(src => src.Courses.Count(c => c.StatusId == (int)CourseStatusEnum.Published)));

            CreateMap<Course, CourseCard_ViewModel>()
                .ForMember(dest => dest.LevelName, opt => opt.MapFrom(dest => dest.Level.Name))
                .ForMember(dest => dest.Instructor, opt => opt.MapFrom(dest => dest.Instructors.FirstOrDefault()));

            CreateMap<Category, Category_ViewModel>();
            CreateMap<Course, CourseDetails_ViewModel>()
                .ForMember(dest => dest.Sections, opt => opt.MapFrom(src => src.Sections.OrderBy(l => l.Order)))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image.URL))
                .ForMember(dest => dest.Instructor, opt => opt.MapFrom(src => src.Instructors.FirstOrDefault()));
            CreateMap<Section, Section_ViewModel>()
                .ForMember(dest => dest.Lectures, opt => opt.MapFrom(src => src.Lectures.OrderBy(l => l.Order)));
            CreateMap<Lecture, Lecture_ViewModel>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Video.Duration));

            CreateMap<Course, CartCourse_ViewModel>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image.URL));
            CreateMap<CartItem, CartItem_ViewModel>();
            CreateMap<Cart, Cart_ViewModel>()
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.CartItems.Sum(c => c.Course.Price)));
            CreateMap<CartCourse_ViewModel, CheckoutCourse>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.SubTitle))
               .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Price * 100));

            CreateMap<Enrollment, Enrollment_ViewModel>();

            CreateMap<Course, LearnCourse_ViewModel>()
                .ForMember(dest => dest.Sections, opt => opt.MapFrom(src => src.Sections.OrderBy(l => l.Order)))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image.URL))
                .ForMember(dest => dest.Instructor, opt => opt.MapFrom(dest => dest.Instructors.FirstOrDefault()));
            CreateMap<Section, LearnSection_ViewModel>()
                .ForMember(dest => dest.Lectures, opt => opt.MapFrom(src => src.Lectures.OrderBy(l => l.Order)));
            CreateMap<Lecture, LearnLecture_ViewModel>();
        }
    }
}