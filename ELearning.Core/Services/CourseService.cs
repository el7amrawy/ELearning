using ELearning.Core.Common;
using ELearning.Core.Interfaces;
using ELearning.Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                var duration = await _unitOfWork.Courses.UpdateCourseDurationAsync(courseId);

                return ServiceResult.Success(duration);
            }
            catch (DbUpdateException ex)
            {
                return ServiceResult.Failure<double>(ex.Message);
            }
            catch (Exception)
            {

                return ServiceResult.Failure<double>("failed to update course duration");
            }

        }
    }
}