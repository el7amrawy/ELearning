using AutoMapper;
using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;

namespace ELearning.EF.Repositories
{
    public class CartsRepository : BaseRepository<Cart>, ICartsRepository
    {
        private readonly AppDbContext _context;
        public CartsRepository(AppDbContext context, IMapper mapper) : base(context, mapper) { 
            _context = context;
        }
        public void Clear(int cartId)
        {
            _context.RemoveRange(_context.CartItems.Where(c => c.CartId == cartId));
        }
    }
}