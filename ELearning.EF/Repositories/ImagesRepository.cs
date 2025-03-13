using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;

namespace ELearning.EF.Repositories
{
    internal class ImagesRepository : BaseRepository<Image>, IImagesRepository
    {
        public ImagesRepository(AppDbContext db) : base(db) { }
    }
}
