using ELearning.Core.Interfaces.Repositories;

namespace ELearning.Core.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		public ICoursesRepository Courses { get; }
		public ICourseStatusRepository CoursesStatus { get; }
		public ILanguagesRepository Languages { get; }
		public ILevelsRepository Levels { get; }
		public Task<int> CompleteAsync();
	}
}
