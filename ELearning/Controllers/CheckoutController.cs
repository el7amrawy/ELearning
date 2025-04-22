using AutoMapper;
using ELearning.Core.DTOs;
using ELearning.Core.Enums;
using ELearning.Core.Interfaces;
using ELearning.Core.Interfaces.Services;
using ELearning.Core.Models;
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

            var enrollments = await _unitOfWork.Enrollments.GetAllAsync(e => e.StudentId == User.GetUserId());
            var paidCoursesIds = enrollments.Where(e => e.IsPaid).Select(e => e.CourseId).ToList();

            if (!cart.CartItems.Any())
            {
                TempData["Error"] = "cart is empty";
                return this.RedirectToPrevious();
            }

            int totalAmount = 0;
            List<CheckoutCourse> courses = new();

            foreach (var item in cart.CartItems)
            {
                if (paidCoursesIds.Contains(item.Course.Id)) continue;

                totalAmount += Convert.ToInt32(item.Course.Price * 100);

                if (item.Course.Price > 0) courses.Add(_mapper.Map<CheckoutCourse>(item.Course));

                await _enrollmentService.EnrollUserAsync(user.Id, item.Course.Id);
            }

            await _cartService.ClearAsync(cart.Id);

            if (!courses.Any()) return RedirectToAction("MyCourses","User");

            var paymentRequest = new PaymentRequest
            {
                UserId=User.GetUserId(),
                Amount = totalAmount,
                BillingEmail = user.Email,
                BillingFirstName = user.FirstName,
                BillingLastName = user.LastName,
                Courses = courses,
            };

            var res = await _paymentService.CreateOrderAsync(paymentRequest);

            if(!res.IsSuccess)
            {
                TempData["Error"] = res.ErrorMessage;
                return this.RedirectToPrevious();
            }

            return Redirect(res.Result);
        }
        // add a filter to validate if the request from paymob (using hmac)
        public async Task<IActionResult> CallBack(bool success, string merchant_order_id, DateTime created_at, string currency)
        {
            if(!success)
            {
                TempData["Error"] = "unsuccessfull payment";
                return RedirectToAction("MyCourses", "User");
            }

            var ids = merchant_order_id.Split("_").ToList();
            var userId = int.Parse(ids[0]);

            ids.RemoveAt(ids.Count - 1);
            ids.RemoveAt(0);

            var user = await _unitOfWork.Users.GetByIdAsync(userId);

            foreach (var id in ids)
            {
                var enrollment = await _unitOfWork.Enrollments
                    .GetItemAsync(e => e.CourseId.ToString() == id && userId == e.StudentId, ["Course"]);

                if (enrollment.IsPaid) continue;

                enrollment.Payment = new Payment
                {
                    Amount = enrollment.Course.Price,
                    BillingEmail = user.Email,
                    BillingFirstName = user.FirstName,
                    BillingLastName = user.LastName,
                    PaymentDate = created_at,
                    Currency = currency,
                    StatusId = (int)PaymentStatusEnum.Completed,
                    TransactionId = merchant_order_id,
                };

                enrollment.IsPaid = success;
            }

            if (await _unitOfWork.CompleteAsync() < 1)
                TempData["Error"] = "problem finalizing payment";
            else
                TempData["Success"] = "successfull payment";

            return RedirectToAction("MyCourses", "User");
        }
    }
}
