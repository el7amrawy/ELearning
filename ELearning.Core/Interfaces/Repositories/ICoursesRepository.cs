using ELearning.Core.Models;

namespace ELearning.Core.Interfaces.Repositories
{
	public interface ICoursesRepository : IBaseRepository<Course>
	{
		public Task<IEnumerable<Course>> GetInstructorCourses(int instructorId, string[] includes = null);
	}
}
