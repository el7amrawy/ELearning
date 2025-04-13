using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ELearning.Core.Consts;
using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ELearning.EF.Repositories
{
    public class CoursesRepository : BaseRepository<Course>, ICoursesRepository
	{
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CoursesRepository(AppDbContext db, IMapper mapper) : base(db, mapper)
        {
            _context = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Model>> GetInstructorCoursesAsync<Model>(int instructorId, string[] includes = null, Expression<Func<Course, bool>> criteria = null, Expression<Func<Course, object>> orderBy = null, string orderByDirection = OrderBy.Ascending, int pageNumber = 0, int pageSize = 0)
        {
            var query = _context.Users.Where(i => i.Id == instructorId).SelectMany(i => i.Courses);

            if (criteria != null)
                query = query.Where(criteria);

            if (includes != null)
                foreach (var item in includes)
                    query = query.Include(item);

            if(orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            if (pageNumber > 0)
                query = query.Skip((pageNumber - 1) * pageSize);

            if (pageSize > 0)
                query = query.Take(pageSize);

            return await query.ProjectTo<Model>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IEnumerable<Model>> GetInstructorCoursesAsync<Model>(int instructorId, string[] includes = null)
        {
            var query = _context.Users.Where(i => i.Id == instructorId).SelectMany(i => i.Courses);

            if (includes != null)
                foreach (var item in includes)
                    query = query.Include(item);

            query.Select(c => c.Id);

            return await query.ProjectTo<Model>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IEnumerable<int>> GetInstructorCoursesIdsAsync(int instructorId)
        {
            var query = _context.Users.Where(i => i.Id == instructorId).SelectMany(i => i.Courses);

            return await query.Select(u => u.Id).ToListAsync();
        }

        public async Task<int> GetInstructorCoursesCountAsync(int instructorId, Expression<Func<Course, bool>> criteria = null)
        {
            var query = _context.Users.Where(i => i.Id == instructorId).SelectMany(i => i.Courses);

            if (criteria != null)
                query = query.Where(criteria);

            return await query.CountAsync();
        }
    }
}