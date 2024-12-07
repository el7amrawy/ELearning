using ELearning.Core.Interfaces;
using ELearning.Core.Interfaces.Repositories;

namespace ELearning.EF
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _db;

		public UnitOfWork(AppDbContext db, ICoursesRepository courses)
		{
			_db = db;
			Courses = courses;
		}

		public ICoursesRepository Courses {  get; private set; }

		public async Task<int> CompleteAsync()=>await _db.SaveChangesAsync();
		public void Dispose()
		{
			_db.Dispose();
		}
	}
}
