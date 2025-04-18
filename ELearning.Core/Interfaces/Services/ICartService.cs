using ELearning.Core.Common;

namespace ELearning.Core.Interfaces.Services
{
    public interface ICartService
    {
        public Task<ServiceResult> CreateAsync(int userId);
        public Task<ServiceResult> AddCourseAsync(int courseId, int userId);
        public Task<ServiceResult> DeleteCourseAsync(int courseId, int userId);
    }
}