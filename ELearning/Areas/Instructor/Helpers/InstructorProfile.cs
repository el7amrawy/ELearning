using AutoMapper;
using ELearning.Areas.Instructor.ViewModels;
using ELearning.Core.DTOs;
using ELearning.Core.Enums;
using ELearning.Core.Models;

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
            CreateMap<Course, Course_ViewModel>()
                .ForMember(dest => dest.StudentsNumber, opt => opt.MapFrom(src => src.Enrollments.Count));
            CreateMap<EditCourse_ViewModel, Course>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.ImageId, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ReverseMap();
            CreateMap<AppUser, Instructor_ViewModel>();
            CreateMap<AppUser, InstructorHomeIndex_ViewModel>()
                .ForMember(dest => dest.CoursesCount, opt => opt.MapFrom(src => src.Courses.Count));

            CreateMap<Course, SectionCourse_ViewModel>();

            CreateMap<Section, Section_ViewModel>();
            CreateMap<Section, SectionWithLectureCount_ViewModel>()
                .ForMember(dest => dest.LectureCount, opt => opt.MapFrom(src => src.Lectures.Count));
            CreateMap<Section, SectionWithCourse_ViewModel>();

            CreateMap<Lecture, LectureList_ViewModel>();
            CreateMap<Lecture, EditLecture_ViewModel>();

            CreateMap<EditSection_ViewModel, Section>().ForMember(dest => dest.CourseId, opt => opt.Ignore()).ReverseMap();
            /* DTOs & Views */
            CreateMap<CreateSection_ViewModel, SectionDto>().ReverseMap();
            CreateMap<CreateLecture_ViewModel, CreateLectureDto>();
            CreateMap<EditLecture_ViewModel, EditLectureDto>();
        }
    }
}