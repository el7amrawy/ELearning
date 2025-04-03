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
            if (courseId == 0) return ServiceResult.Failure("course does not exist");

            var ids = await _unitOfWork.Courses.GetInstructorCoursesIdsAsync(instructorId);

            if (ids.Contains(courseId))
                return ServiceResult.Success();

            return ServiceResult.Failure("You are not allowed to access this course");
        }
    }
}