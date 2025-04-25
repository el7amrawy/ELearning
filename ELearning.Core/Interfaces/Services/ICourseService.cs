using ELearning.Core.Common;

namespace ELearning.Core.Interfaces.Services
{
    public interface ICourseService
    {
        public Task<ServiceResult> ValidateCourseOwnerAsync(int instructorId, int courseId);
        public Task<ServiceResult<double>> UpdateCourseDurationAsync(int courseId);
        public Task<ServiceResult> ValidateCoursePayment(int courseId, int userId);
    }
}