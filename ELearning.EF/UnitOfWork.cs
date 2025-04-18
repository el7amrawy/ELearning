using AutoMapper;
using ELearning.Core.Interfaces;
using ELearning.Core.Interfaces.Repositories;
using ELearning.EF.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace ELearning.EF
{
    public class UnitOfWork : IUnitOfWork
	{
        private readonly AppDbContext _db;
		private readonly IMapper _mapper;
        public UnitOfWork(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public ICoursesRepository Courses => new CoursesRepository(_db, _mapper);
        public ICourseStatusRepository CoursesStatus => new CourseStatusRepository(_db, _mapper);
        public ILanguagesRepository Languages => new LanguagesRepository(_db, _mapper);
        public ILevelsRepository Levels => new LevelsRepository(_db, _mapper);
        public IUsersRepository Users => new UsersRepository(_db, _mapper);
        public IImagesRepository Images => new ImagesRepository(_db, _mapper);
        public ICategoriesRepository Categories => new CategoriesRepository(_db, _mapper);
        public ISectionsRepository Sections => new SectionsRepository(_db, _mapper);
        public ILecturesRepository Lectures => new LecturesRepository(_db, _mapper);
        public IVideosRepository Videos => new VideosRepository(_db, _mapper);
        public ICartsRepository Carts => new CartsRepository(_db, _mapper);
        public async Task<int> CompleteAsync() => await _db.SaveChangesAsync();
		public void Dispose() => _db.Dispose();
        public async Task<IDbContextTransaction> BeginTransactionAsync() => await _db.Database.BeginTransactionAsync();
    }
}