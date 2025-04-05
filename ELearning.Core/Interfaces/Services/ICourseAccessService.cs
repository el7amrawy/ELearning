using ELearning.Core.Common;

namespace ELearning.Core.Interfaces.Services
{
    public interface ICourseAccessService
    {
        public Task<ServiceResult> ValidateCourseOwnerAsync(int instructorId, int courseId);
    }
}