using ELearning.Core.Common;

namespace ELearning.Core.Interfaces
{
    public interface IInstructorService
    {
        public Task<ServiceResult> ValidateCourseAsync(int instructorId, int courseId);
    }
}