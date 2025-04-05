using AutoMapper;
using ELearning.Core.Common;
using ELearning.Core.DTOs;
using ELearning.Core.Interfaces;
using ELearning.Core.Interfaces.Services;
using ELearning.Core.Models;

namespace ELearning.Core.Services
{
    public class SectionService : ISectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SectionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResult> CreateAsync(SectionDto sectionDto)
        {
            var course = await _unitOfWork.Courses.GetItemAsync(c => c.Id == sectionDto.CourseId, ["Sections"]);

            if (course == null) return ServiceResult.Failure("course not found");

            var section = _mapper.Map<Section>(sectionDto);

            var sectionsCount = course.Sections.Count();

            section.Order = sectionsCount + 1;

            course.Sections.Add(section);

            if (await _unitOfWork.CompleteAsync() < 1)
                return ServiceResult.Failure("problem creating section");

            return ServiceResult.Success();
        }
    }
}