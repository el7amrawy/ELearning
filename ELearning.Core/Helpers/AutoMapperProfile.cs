using AutoMapper;
using ELearning.Core.DTOs;
using ELearning.Core.Models;

namespace ELearning.Core.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {

            CreateMap<SectionDto, Section>();
            CreateMap<CreateLectureDto, Lecture>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<Course, CheckoutCourse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.SubTitle))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Price * 100));
        }
    }
}