using AutoMapper;
using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;

namespace ELearning.EF.Repositories
{
    public class CheckoutsRepository : BaseRepository<Checkout>, ICheckoutsRepository
    {
        public CheckoutsRepository(AppDbContext context, IMapper mapper) : base(context, mapper) { }
    }
}