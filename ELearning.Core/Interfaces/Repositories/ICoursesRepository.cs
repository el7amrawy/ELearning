using System.Linq.Expressions;
using ELearning.Core.Models;

namespace ELearning.Core.Interfaces.Repositories
{
	public interface ICoursesRepository : IBaseRepository<Course>
	{
		public Task<IEnumerable<Model>> GetInstructorCoursesAsync<Model>(int instructorId, string[] includes = null, Expression<Func<Course, bool>> criteria = null);
        public Task<IEnumerable<int>> GetInstructorCoursesIdsAsync(int instructorId);
    }
}