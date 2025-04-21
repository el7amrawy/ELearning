using ELearning.Core.Models;

namespace ELearning.Core.Interfaces.Repositories
{
    public interface ICartsRepository : IBaseRepository<Cart>
    {
        public void Clear(int cartId);
    }
}