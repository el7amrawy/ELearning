using AutoMapper;
using ELearning.Areas.Instructor.ViewModels;
using ELearning.Core.DTOs;
using ELearning.Core.Enums;
using ELearning.Core.Models;
using ELearning.ViewModels;

namespace ELearning.Areas.Instructor.Helpers
{
    public class InstructorProfile : Profile
    {
        public InstructorProfile()
        {

            /* Instructor Area */
            CreateMap<CreateCourse_ViewModel, Course>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(_ => CourseStatusEnum.Draft));
            CreateMap<Course, Course_ViewModel>();
            CreateMap<EditCourse_ViewModel, Course>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.ImageId, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<AppUser, Instructor_ViewModel>();
            CreateMap<Course, SectionCourse_ViewModel>();

            CreateMap<Section, Section_ViewModel>();
            CreateMap<Section, SectionWithLectureCount_ViewModel>()
                .ForMember(dest => dest.LectureCount, opt => opt.MapFrom(src => src.Lectures.Count));
            CreateMap<Section, SectionWithCourse_ViewModel>();

            CreateMap<Lecture, LectureList_ViewModel>();

            /* DTOs & Views */
            CreateMap<CreateSection_ViewModel, SectionDto>().ReverseMap();
            CreateMap<CreateLecture_ViewModel, CreateLectureDto>();
        }
    }
}