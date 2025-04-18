using AutoMapper;
using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;

namespace ELearning.EF.Repositories
{
    public class CartsRepository : BaseRepository<Cart>, ICartsRepository
    {
        public CartsRepository(IMapper mapper, AppDbContext context) : base(context, mapper) { }
    }
}