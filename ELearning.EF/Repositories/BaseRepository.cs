using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ELearning.Core.Consts;
using ELearning.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ELearning.EF.Repositories
{
    public abstract class BaseRepository<Entity> : IBaseRepository<Entity> where Entity : class
	{
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public BaseRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<Entity> GetByIdAsync(int id) => await _db.Set<Entity>().FindAsync(id);
        public void Add(Entity entity) => _db.Add(entity);
        public void Update(Entity entity) => _db.Entry(entity).State = EntityState.Modified;
        public void Delete(Entity entity) => _db.Entry(entity).State = EntityState.Deleted;
        public async Task<int> CountAsync(Expression<Func<Entity, bool>> criteria = null, string[] includes = null)
        {
            var query = _db.Set<Entity>().AsQueryable();

            if (criteria != null)
                query = query.Where(criteria);

            if (includes != null)
                foreach (var item in includes)
                    query = query.Include(item);

            return await query.CountAsync();
        }
        public async Task AddRangeAsync(IEnumerable<Entity> entities) => await _db.Set<Entity>().AddRangeAsync(entities);
        public virtual async Task<IEnumerable<Entity>> GetAllAsync(Expression<Func<Entity, bool>> criteria = null, string[] includes = null, int pageNumber = 0, int pageSize = 0, Expression<Func<Entity, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            var query = _db.Set<Entity>().AsQueryable();

            if (criteria != null)
                query = query.Where(criteria);

            if (includes != null)
                foreach (var item in includes)
                    query = query.Include(item);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);

                else query = query.OrderByDescending(orderBy);
            }

            if (pageNumber > 0)
                query = query.Skip((pageNumber - 1) * pageSize);

            if (pageSize > 0)
                query = query.Take(pageSize);

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<Entity>> GetAllAsync() => await _db.Set<Entity>().ToListAsync();
        public Task<Entity> GetItemAsync(Expression<Func<Entity, bool>> criteria, string[] includes = null)
        {
            var query = _db.Set<Entity>().AsQueryable();

            if (includes != null)
                foreach (var item in includes)
                    query = query.Include(item);

            return query.FirstOrDefaultAsync(criteria);
        }
        public Task<Model> GetItemAsync<Model>(Expression<Func<Entity, bool>> criteria, string[] includes = null)
        {
            var query = _db.Set<Entity>().Where(criteria);

            if (includes != null)
                foreach (var item in includes)
                    query = query.Include(item);

            return query.ProjectTo<Model>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Model>> GetAllAsync<Model>(Expression<Func<Entity, bool>> criteria = null, string[] includes = null, int pageNumber = 0, int pageSize = 0, Expression<Func<Entity, object>> orderBy = null, string orderByDirection = "ASC")
        {
            var query = _db.Set<Entity>().AsQueryable();

            if (criteria != null)
                query = query.Where(criteria);

            if (includes != null)
                foreach (var item in includes)
                    query = query.Include(item);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);

                else query = query.OrderByDescending(orderBy);
            }

            if (pageNumber > 0)
                query = query.Skip((pageNumber - 1) * pageSize);

            if (pageSize > 0)
                query = query.Take(pageSize);

            return await query.ProjectTo<Model>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<Entity> GetByMaxAsync<Property>(Expression<Func<Entity, Property>> selector)
        {
            return await _db.Set<Entity>().OrderByDescending(selector).FirstOrDefaultAsync();
        }
    }
}