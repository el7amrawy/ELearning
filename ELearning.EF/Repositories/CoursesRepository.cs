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

        public async Task<double> UpdateCourseDurationAsync(int courseId)
        {
            var sectionsDurations = await _context.Sections.Where(s => s.CourseId == courseId)
                .Select(s => new { SectionId = s.Id, Duration = s.Lectures.Sum(l => l.Video.Duration) }).ToListAsync();

            foreach (var section in sectionsDurations)
            {
                await _context.Sections.Where(s => s.Id == section.SectionId)
                     .ExecuteUpdateAsync(setters => setters.SetProperty(s => s.Duration, section.Duration));
            }

            var courseTotalDuration = sectionsDurations.Sum(s => s.Duration);

            await _context.Courses.Where(c => c.Id == courseId)
                .ExecuteUpdateAsync(setters => setters.SetProperty(c => c.Duration, courseTotalDuration));

            return courseTotalDuration;
        }
        public async Task<IEnumerable<Model>> SearchAndFilterAsync<Model>(
            string? search = null,
            int categoryId = 0,
            decimal maxPrice = 0,
            decimal minPrice = 0,
            double duration = 0,
            int pageNumber = 0,
            int pageSize = 0,
            string? orderByDirection = OrderBy.Ascending,
            string[]? includes = null,
            Expression<Func<Course, bool>>? criteria = null)
        {
            var query = _context.Courses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();
                query = query.Where(c => c.Title.ToLower().Contains(search) || (c.Description.ToLower() != null && c.Description.ToLower().Contains(search)));
            }
                       
            if (categoryId > 0)
                query = query.Where(c => c.CategoryId == categoryId);

            if (maxPrice > 0)
                query = query.Where(c => c.Price <= maxPrice);

            if (minPrice > 0)
                query = query.Where(c => c.Price >= minPrice);

            if (duration > 0)
                query = query.Where(c => c.Duration <= duration);

            if (orderByDirection == OrderBy.Descending)
                query = query.OrderDescending();

            if (includes != null)
                foreach (var item in includes)
                    query = query.Include(item);

            if (criteria != null)
                query = query.Where(criteria);

            if (pageNumber > 0)
                query = query.Skip((pageNumber - 1) * pageSize);

            if (pageSize > 0)
                query = query.Take(pageSize);

            return await query.ProjectTo<Model>(_mapper.ConfigurationProvider).ToListAsync();
        }
        
        public async Task<IEnumerable<Course>> SearchAndFilterAsync(
            string? search = null,
            int categoryId = 0,
            decimal maxPrice = 0,
            decimal minPrice = 0,
            double duration = 0,
            int pageNumber = 0,
            int pageSize = 0,
            string? orderByDirection = OrderBy.Ascending,
            string[]? includes = null,
            Expression<Func<Course, bool>>? criteria = null)
        {
            var query = _context.Courses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();
                query = query.Where(c => c.Title.ToLower().Contains(search) || (c.Description.ToLower() != null && c.Description.ToLower().Contains(search)));
            }

            if (categoryId > 0)
                query = query.Where(c => c.CategoryId == categoryId);

            if (maxPrice > 0)
                query = query.Where(c => c.Price <= maxPrice);

            if (minPrice > 0)
                query = query.Where(c => c.Price >= maxPrice);

            if (duration > 0)
                query = query.Where(c => c.Duration <= duration);

            if (orderByDirection == OrderBy.Descending)
                query = query.OrderDescending();

            if (includes != null)
                foreach (var item in includes)
                    query = query.Include(item);

            if (criteria != null)
                query = query.Where(criteria);

            if (pageNumber > 0)
                query = query.Skip((pageNumber - 1) * pageSize);

            if (pageSize > 0)
                query = query.Take(pageSize);

            return await query.ToListAsync();
        }
    }
}