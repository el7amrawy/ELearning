using ELearning.Core.Interfaces.Repositories;

namespace ELearning.Core.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		public ICoursesRepository Courses { get; }
		public Task<int> CompleteAsync();
	}
}
