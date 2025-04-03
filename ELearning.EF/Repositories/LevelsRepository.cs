using AutoMapper;
using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;

namespace ELearning.EF.Repositories
{
    public class LevelsRepository(AppDbContext context,IMapper mapper):BaseRepository<Level>(context, mapper),ILevelsRepository
    {
    }
}
