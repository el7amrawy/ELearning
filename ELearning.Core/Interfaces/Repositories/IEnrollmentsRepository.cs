using System.Linq.Expressions;
using ELearning.Core.Models;

namespace ELearning.Core.Interfaces.Repositories
{
    public interface IEnrollmentsRepository : IBaseRepository<Enrollment>
    {
        public Task<bool> ExistsAsync(Expression<Func<Enrollment, bool>> predicate);
    }
}