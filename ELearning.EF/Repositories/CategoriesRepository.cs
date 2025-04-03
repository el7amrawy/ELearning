using AutoMapper;
using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;

namespace ELearning.EF.Repositories
{
    public class CategoriesRepository:BaseRepository<Category>,ICategoriesRepository
    {
        public CategoriesRepository(AppDbContext context, IMapper mapper):base(context, mapper) { }
    }
}