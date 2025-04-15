using System.Linq.Expressions;
using ELearning.Core.Consts;
using ELearning.Core.Models;

namespace ELearning.Core.Interfaces.Repositories
{
	public interface ICoursesRepository : IBaseRepository<Course>
	{
        public Task<IEnumerable<Model>> GetInstructorCoursesAsync<Model>(int instructorId, string[] includes = null, Expression<Func<Course, bool>> criteria = null, Expression<Func<Course, object>> orderBy = null, string orderByDirection = OrderBy.Ascending, int pageNumber = 0, int pageSize = 0);
        public Task<IEnumerable<int>> GetInstructorCoursesIdsAsync(int instructorId);
        public Task<int> GetInstructorCoursesCountAsync(int instructorId, Expression<Func<Course, bool>> criteria = null);
        public Task<double> UpdateCourseDurationAsync(int courseId);
    }
}