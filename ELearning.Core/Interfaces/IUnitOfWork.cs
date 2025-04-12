using ELearning.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace ELearning.Core.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		public ICoursesRepository Courses { get; }
		public ICourseStatusRepository CoursesStatus { get; }
		public ILanguagesRepository Languages { get; }
		public ILevelsRepository Levels { get; }
		public IUsersRepository Users { get; }
		public IImagesRepository Images { get; }
		public ICategoriesRepository Categories { get; }
		public ISectionsRepository Sections { get; }
		public ILecturesRepository Lectures { get; }
		public IVideosRepository Videos { get; }
		public Task<int> CompleteAsync();
		public Task<IDbContextTransaction> BeginTransactionAsync();
    }
}