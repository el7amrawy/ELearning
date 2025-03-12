using System.Linq.Expressions;

namespace ELearning.Core.Interfaces.Repositories
{
	public interface IBaseRepository<Entity> where Entity : class
	{
		public Task<Entity> GetByIdAsync(int id);
        public void Add(Entity entity);
		public void Update(Entity entity);
		public void Delete(Entity entity);
		public Task<IEnumerable<Entity>> GetAllAsync(Expression<Func<Entity, bool>> criteria = null, string[] includes = null, int pageNumber = 0, int pageSize = 0);
		public Task<Entity> GetItemAsync(Expression<Func<Entity, bool>> criteria, string[] includes = null);
		public Task<int> CountAsync(Expression<Func<Entity, bool>> criteria = null);
    }
}
