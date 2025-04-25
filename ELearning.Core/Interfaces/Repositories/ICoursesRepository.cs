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
        public Task<IEnumerable<Model>> SearchAndFilterAsync<Model>(
            string? search = null,
            int categoryId = 0,
            decimal maxPrice = 0,
            decimal minPrice = 0,
            double duration = 0,
            int pageNumber = 0,
            int pageSize = 0,
            string? orderByDirection = OrderBy.Ascending,
            string[]? includes = null,
            Expression<Func<Course, bool>>? criteria = null);
        public Task<IEnumerable<Course>> SearchAndFilterAsync(
           string? search = null,
           int categoryId = 0,
           decimal maxPrice = 0,
           decimal minPrice = 0,
           double duration = 0,
           int pageNumber = 0,
           int pageSize = 0,
           string? orderByDirection = OrderBy.Ascending,
           string[]? includes = null,
           Expression<Func<Course, bool>>? criteria = null);
    }
}