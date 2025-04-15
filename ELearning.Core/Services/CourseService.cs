using ELearning.Core.Common;
using ELearning.Core.Interfaces;
using ELearning.Core.Interfaces.Services;

namespace ELearning.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
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
        public async Task<ServiceResult<double>> UpdateCourseDurationAsync(int courseId)
        {
            var duration = await _unitOfWork.Courses.UpdateCourseDurationAsync(courseId);

            if (await _unitOfWork.CompleteAsync() < 1) return ServiceResult.Failure<double>("failed to update course duration");

            return ServiceResult.Success(duration);
        }
    }
}