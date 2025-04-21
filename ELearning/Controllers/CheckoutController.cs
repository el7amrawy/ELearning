using AutoMapper;
using ELearning.Core.DTOs;
using ELearning.Core.Interfaces;
using ELearning.Core.Interfaces.Services;
using ELearning.Extensions;
using ELearning.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    [Authorize(Roles ="Student")]
    public class CheckoutController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;
        private readonly ICartService _cartService;

        public CheckoutController(IUnitOfWork unitOfWork, IEnrollmentService enrollmentService, IMapper mapper, IPaymentService paymentService, ICartService cartService)
        {
            _unitOfWork = unitOfWork;
            _enrollmentService = enrollmentService;
            _mapper = mapper;
            _paymentService = paymentService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _unitOfWork.Users.GetByIdAsync(User.GetUserId());

            var cart = await _unitOfWork.Carts.GetItemAsync<Cart_ViewModel>(c => c.UserId == user.Id);

            if (!cart.CartItems.Any())
            {
                TempData["Error"] = "cart is empty";
                return this.RedirectToPrevious();
            }

            int totalAmount = 0;
            List<CheckoutCourse> courses = new();

            foreach (var item in cart.CartItems)
            {
                totalAmount += Convert.ToInt32(item.Course.Price * 100);

                if (item.Course.Price > 0) courses.Add(_mapper.Map<CheckoutCourse>(item.Course));

                await _enrollmentService.EnrollUserAsync(user.Id, item.Course.Id);
            }

            await _cartService.ClearAsync(cart.Id);

            if (!courses.Any()) return RedirectToAction("MyCourses","User");

            var paymentRequest = new PaymentRequest
            {
                Amount = totalAmount,
                BillingEmail = user.Email,
                BillingFirstName = user.FirstName,
                BillingLastName = user.LastName,
                Courses = courses,
            };



            return View();
        }
    }
}
