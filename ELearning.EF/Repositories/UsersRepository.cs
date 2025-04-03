using AutoMapper;
using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;

namespace ELearning.EF.Repositories
{
    public class UsersRepository(AppDbContext context, IMapper mapper):BaseRepository<AppUser>(context, mapper),IUsersRepository
    {
    }
}