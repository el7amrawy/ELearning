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
        private readonly ILectureService _lectureService;

        public SectionService(IUnitOfWork unitOfWork, IMapper mapper, ILectureService lectureService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _lectureService = lectureService;
        }

        public async Task<ServiceResult> CreateAsync(SectionDto sectionDto)
        {
            var course = await _unitOfWork.Courses.GetItemAsync(c => c.Id == sectionDto.CourseId, ["Sections"]);

            if (course == null) return ServiceResult.Failure("course not found");

            var section = _mapper.Map<Section>(sectionDto);

            section.Order = course.Sections.Count > 0 ? course.Sections.Last().Order + 1 : 1;

            course.Sections.Add(section);

            if (await _unitOfWork.CompleteAsync() < 1)
                return ServiceResult.Failure("problem creating section");

            return ServiceResult.Success();
        }
        public async Task<ServiceResult> SwapOrder(int courseId, int sectionId1, int sectionId2)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    if (courseId == 0 || sectionId1 == 0 || sectionId2 == 0) return ServiceResult.Failure("invalid section");

                    var section1 = await _unitOfWork.Sections.GetItemAsync(s => s.Id == sectionId1 && s.CourseId == courseId);

                    if (section1 == null) return ServiceResult.Failure("first section not found");

                    var section2 = await _unitOfWork.Sections.GetItemAsync(s => s.Id == sectionId2 && s.CourseId == courseId);

                    if (section2 == null) return ServiceResult.Failure("second section not found");

                    var tempOrder = section1.Order;

                    section1.Order = -1;
                    await _unitOfWork.CompleteAsync();

                    section1.Order = section2.Order;
                    section2.Order = tempOrder;

                    await _unitOfWork.CompleteAsync();
                    await transaction.CommitAsync();

                    return ServiceResult.Success();
                
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult.Failure("problem swapping order");
                }
            } 

        }
        public async Task<ServiceResult> DeleteAsync(int courseId, int sectionId)
        {
            var section = await _unitOfWork.Sections.GetItemAsync(s => s.CourseId == courseId && sectionId == s.Id, ["Lectures"]);

            if (section == null) return ServiceResult.Failure("Section does not exist");

            var lecIds = section.Lectures.Select(section => section.Id).ToList();

            foreach (var id in lecIds)
            {
                await _lectureService.DeleteAsync(id, sectionId);
            }

            _unitOfWork.Sections.Delete(section);

            if (await _unitOfWork.CompleteAsync() < 1) return ServiceResult.Failure("problem deleting section");

            return ServiceResult.Success();
        }
    }
}