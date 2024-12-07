using ELearning.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ELearning.EF.Repositories
{
	public class BaseRepository<Model> : IBaseRepository<Model> where Model : class
	{
		private readonly AppDbContext _db;

		public BaseRepository(AppDbContext db)
		{
			_db = db;
		}

		public async Task<IEnumerable<Model>> GetAllAsync() =>await _db.Set<Model>().ToListAsync();
	}
}
