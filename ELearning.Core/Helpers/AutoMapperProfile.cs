using AutoMapper;
using ELearning.Core.DTOs;
using ELearning.Core.Models;

namespace ELearning.Core.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {

            CreateMap<SectionDto, Section>();
        }
    }
}
