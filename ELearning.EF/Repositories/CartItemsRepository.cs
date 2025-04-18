using AutoMapper;
using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;

namespace ELearning.EF.Repositories
{
    public class CartItemsRepository : BaseRepository<CartItem>, ICartItemsRepository
    {
        public CartItemsRepository(AppDbContext context, IMapper mapper) : base(context, mapper) { }
    }
}