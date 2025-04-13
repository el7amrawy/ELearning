using System.Linq.Expressions;
using ELearning.Core.Consts;
using ELearning.Core.Models;

namespace ELearning.Core.Interfaces.Repositories
{
	public interface ICoursesRepository : IBaseRepository<Course>
	{
        public Task<IEnumerable<Model>> GetInstructorCoursesAsync<Model>(int instructorId, string[] includes = null, Expression<Func<Course, bool>> criteria = null, int quantity = 0, Expression<Func<Course, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        public Task<IEnumerable<int>> GetInstructorCoursesIdsAsync(int instructorId);
    }
}