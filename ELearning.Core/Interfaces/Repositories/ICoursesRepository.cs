using ELearning.Core.Models;

namespace ELearning.Core.Interfaces.Repositories
{
	public interface ICoursesRepository : IBaseRepository<Course>
	{
		public Task<IEnumerable<Model>> GetInstructorCourses<Model>(int instructorId, string[] includes = null);
	}
}
