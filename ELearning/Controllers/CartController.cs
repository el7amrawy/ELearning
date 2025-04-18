using ELearning.Core.Interfaces;
using ELearning.Core.Interfaces.Services;
using ELearning.Extensions;
using ELearning.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    [Authorize(Roles = "Student")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartService _cartService;
        public CartController(IUnitOfWork unitOfWork, ICartService cartService)
        {
            _unitOfWork = unitOfWork;
            _cartService = cartService;
        }
        public async Task<IActionResult> Index() => View(await _unitOfWork.Carts.GetItemAsync<Cart_ViewModel>(c => c.UserId == User.GetUserId()));
        [HttpGet]
        public async Task<IActionResult> Add(int id) {
            var res = await _cartService.AddCourseAsync(id, User.GetUserId());

            if (!res.IsSuccess) TempData["Error"] = res.ErrorMessage;
            else TempData["Success"] = "Coures has been added to cart successfully";

            return this.RedirectToPrevious();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _cartService.DeleteCourseAsync(id, User.GetUserId());

            if (!res.IsSuccess) TempData["Error"] = res.ErrorMessage;

            return this.RedirectToPrevious();
        }
    }
}