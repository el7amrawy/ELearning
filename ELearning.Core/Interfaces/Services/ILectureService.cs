using ELearning.Core.Common;
using ELearning.Core.DTOs;

namespace ELearning.Core.Interfaces.Services
{
    public interface ILectureService
    {
        public Task<ServiceResult> CreateAsync(CreateLectureDto lectureDto, string instructorName, int courseId);
        public Task<ServiceResult> SwapOrderAsync(int sectionId, int lectureId1, int lectureId2);
        public Task<ServiceResult> DeleteAsync(int lectureId, int sectionId);
    }
}