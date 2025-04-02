using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ELearning.EF.Repositories
{
	public class CoursesRepository : BaseRepository<Course> ,ICoursesRepository
	{
        private readonly AppDbContext _context;
        public CoursesRepository(AppDbContext db) : base(db)
        {
            _context = db;
        }
        public async Task<IEnumerable<Course>> GetInstructorCourses(int instructorId, string[] includes = null)
        {
            var istructor = _context.Users.Where(i => i.Id == instructorId);

            var query = istructor.SelectMany(i => i.Courses);

            if (includes != null)
                foreach (var item in includes)
                    query = query.Include(item);

            return await query.ToListAsync();
        }
    }
}
