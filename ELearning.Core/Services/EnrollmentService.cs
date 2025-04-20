using ELearning.Core.Common;
using ELearning.Core.Interfaces;
using ELearning.Core.Interfaces.Services;
using ELearning.Core.Models;

namespace ELearning.Core.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnrollmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult> EnrollUserAsync(int userId, int courseId)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(courseId);

            if (course == null) return ServiceResult.Failure("course does not exist");


            if (await IsUserEnrolledAsync(userId, courseId)) return ServiceResult.Failure("You are already enrolled in this course");

            _unitOfWork.Enrollments.Add(new Enrollment
            {
                CourseId = courseId,
                StudentId = userId,
                EnrollmentDate = DateTime.UtcNow,
                IsPaid = course.Price == 0
            });

            if (await _unitOfWork.CompleteAsync() < 1) return ServiceResult.Failure("problem enrolling into course");

            return ServiceResult.Success();
        }

        public async Task<bool> IsUserEnrolledAsync(int userId, int courseId)
        {
            return await _unitOfWork.Enrollments.ExistsAsync(e => e.StudentId == userId && e.CourseId == courseId);
        }
    }
}