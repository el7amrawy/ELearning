using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;

namespace ELearning.EF.Repositories
{
    public class LevelsRepository(AppDbContext context):BaseRepository<Level>(context),ILevelsRepository
    {
    }
}
