using AutoMapper;
using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;

namespace ELearning.EF.Repositories
{
    public class SectionsRepository : BaseRepository<Section>, ISectionsRepository
    {
        public SectionsRepository(AppDbContext context, IMapper mapper) : base(context, mapper) { }
    }
}