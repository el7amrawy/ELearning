using ELearning.Core.Common;

namespace ELearning.Core.Interfaces.Services
{
    public interface IEnrollmentService
    {
        public Task<ServiceResult> EnrollUserAsync(int userId, int courseId);
        public Task<bool> IsUserEnrolledAsync(int userId, int courseId);
    }
}