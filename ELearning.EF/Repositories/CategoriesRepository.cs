using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;

namespace ELearning.EF.Repositories
{
    public class CategoriesRepository:BaseRepository<Category>,ICategoriesRepository
    {
        public CategoriesRepository(AppDbContext context):base(context) { }
    }
}