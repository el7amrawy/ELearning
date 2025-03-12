using System.Linq.Expressions;
using ELearning.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ELearning.EF.Repositories
{
	public class BaseRepository<Entity> : IBaseRepository<Entity> where Entity : class
	{
		private readonly AppDbContext _db;
        public BaseRepository(AppDbContext db) => _db = db;
        public async Task<Entity> GetByIdAsync(int id) => await _db.Set<Entity>().FindAsync(id);
        public void Add(Entity entity) => _db.Entry(entity).State = EntityState.Added;
        public void Update(Entity entity) => _db.Entry(entity).State = EntityState.Modified;
        public void Delete(Entity entity) => _db.Entry(entity).State = EntityState.Deleted;
        public virtual async Task<IEnumerable<Entity>> GetAllAsync(Expression<Func<Entity, bool>> criteria = null, string[] includes = null, int pageNumber = 0, int pageSize = 0)
        {
            var query = _db.Set<Entity>().AsQueryable();

            if (criteria != null)
                query = query.Where(criteria);

            if (includes != null)
                foreach (var item in includes)
                    query = query.Include(item);

            if (pageNumber > 0)
                query = query.Skip((pageNumber - 1) * pageSize);

            if (pageSize > 0)
                query = query.Take(pageSize);

            return await query.ToListAsync();
        }

        public Task<Entity> GetItemAsync(Expression<Func<Entity, bool>> criteria, string[] includes = null)
        {
            var query = _db.Set<Entity>().AsQueryable();

            if (includes != null)
                foreach (var item in includes)
                    query = query.Include(item);

            return query.FirstOrDefaultAsync(criteria);
        }
        public async Task<int> CountAsync(Expression<Func<Entity, bool>> criteria = null)
        {
            var query = _db.Set<Entity>().AsQueryable();

            if (criteria != null)
                query = query.Where(criteria);

            return await query.CountAsync();
        }
    }
}
