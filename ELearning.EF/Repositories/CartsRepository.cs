using AutoMapper;
using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;

namespace ELearning.EF.Repositories
{
    public class CartsRepository : BaseRepository<Cart>, ICartsRepository
    {
        public CartsRepository(AppDbContext context, IMapper mapper) : base(context, mapper) { }
    }
}