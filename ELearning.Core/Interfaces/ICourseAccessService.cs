using ELearning.Core.Common;

namespace ELearning.Core.Interfaces
{
    public interface ICourseAccessService
    {
        public Task<ServiceResult> ValidateCourseOwnerAsync(int instructorId, int courseId);
    }
}