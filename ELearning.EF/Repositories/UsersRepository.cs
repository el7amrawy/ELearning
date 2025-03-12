using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;

namespace ELearning.EF.Repositories
{
    public class UsersRepository(AppDbContext context):BaseRepository<AppUser>(context),IUsersRepository
    {
    }
}
