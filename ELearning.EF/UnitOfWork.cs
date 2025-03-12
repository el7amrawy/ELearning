using ELearning.Core.Interfaces;
using ELearning.Core.Interfaces.Repositories;
using ELearning.EF.Repositories;

namespace ELearning.EF
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _db;
		public ICoursesRepository Courses {  get; private set; }
		public UnitOfWork(AppDbContext db)
		{
			_db = db;
			Courses = new CoursesRepository(db);
		}
		public async Task<int> CompleteAsync() => await _db.SaveChangesAsync();
		public void Dispose() => _db.Dispose();
	}
}
