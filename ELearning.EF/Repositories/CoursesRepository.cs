using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;

namespace ELearning.EF.Repositories
{
	public class CoursesRepository : BaseRepository<Course> ,ICoursesRepository
	{
		public CoursesRepository(AppDbContext db) : base(db) { }
	}
}
