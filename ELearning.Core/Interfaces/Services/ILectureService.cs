using ELearning.Core.Common;
using ELearning.Core.DTOs;

namespace ELearning.Core.Interfaces.Services
{
    public interface ILectureService
    {
        public Task<ServiceResult> CreateAsync(CreateLectureDto lectureDto, string instructorName, int courseId);
    }
}