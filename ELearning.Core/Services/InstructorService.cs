using ELearning.Core.Common;
using ELearning.Core.Interfaces;

namespace ELearning.Core.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InstructorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult> ValidateCourseAsync(int instructorId, int courseId)
        {
            var ids = await _unitOfWork.Courses.GetInstructorCoursesIdsAsync(instructorId);

            if (ids.Contains(courseId))
                return ServiceResult.Success();

            return ServiceResult.Failure("You don't have access to this course");
        }
    }
}