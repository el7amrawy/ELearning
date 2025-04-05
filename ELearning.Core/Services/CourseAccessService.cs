using ELearning.Core.Common;
using ELearning.Core.Interfaces;
using ELearning.Core.Interfaces.Services;

namespace ELearning.Core.Services
{
    public class CourseAccessService : ICourseAccessService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseAccessService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult> ValidateCourseOwnerAsync(int instructorId, int courseId)
        {
            if (courseId == 0) return ServiceResult.Failure("course does not exist");

            var ids = await _unitOfWork.Courses.GetInstructorCoursesIdsAsync(instructorId);

            if (ids.Contains(courseId))
                return ServiceResult.Success();

            return ServiceResult.Failure("You are not allowed to access this course");
        }
    }
}