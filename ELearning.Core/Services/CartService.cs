using ELearning.Core.Common;
using ELearning.Core.Interfaces;
using ELearning.Core.Interfaces.Services;
using ELearning.Core.Models;

namespace ELearning.Core.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ServiceResult> CreateAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetItemAsync(u => u.Id == userId, ["Cart"]);

            if (user.Cart != null) return ServiceResult.Failure("user already has a cart!");

            var timespan = DateTime.UtcNow;
            user.Cart = new Cart { CreatedAt = timespan, UpdatedAt = timespan };

            if (await _unitOfWork.CompleteAsync() < 1) return ServiceResult.Failure("problem creating cart");

            return ServiceResult.Success();
        }
        public async Task<ServiceResult> AddCourseAsync(int courseId, int userId)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var cart = await _unitOfWork.Carts.GetItemAsync(c => c.UserId == userId, ["CartItems"]);
                if (cart == null) return ServiceResult.Failure("cart does not exist");

                var course = await _unitOfWork.Courses.GetByIdAsync(courseId);
                if (course == null) return ServiceResult.Failure("course does not exist");

                if (cart.CartItems.FirstOrDefault(c => c.CourseId == courseId) != null) return ServiceResult.Failure("The course is already in the cart");

                cart.CartItems.Add(new CartItem { CreatedAt = DateTime.UtcNow, Course = course });
                cart.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.CompleteAsync();
                await transaction.CommitAsync();

                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ServiceResult.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult> DeleteCourseAsync(int courseId, int userId)
        {
            var cart = await _unitOfWork.Carts.GetItemAsync(c => c.UserId == userId, ["CartItems"]);

            if (cart == null) return ServiceResult.Failure("Cart does not exist");

            if (!cart.CartItems.Any()) return ServiceResult.Failure("Cart is empty");

            var cartItem = cart.CartItems.FirstOrDefault(x => x.CourseId == courseId);

            if (cartItem == null) return ServiceResult.Failure("the course is not in the cart");

            cart.CartItems.Remove(cartItem);

            if (await _unitOfWork.CompleteAsync() < 1) return ServiceResult.Failure("problem removing course from cart");

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> ClearAsync(int cartId)
        {
            _unitOfWork.Carts.Clear(cartId);

            if (await _unitOfWork.CompleteAsync() < 1) return ServiceResult.Failure("failed to clear cart");

            return ServiceResult.Success();
        }
    }
}