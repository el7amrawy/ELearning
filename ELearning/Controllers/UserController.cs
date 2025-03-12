using AutoMapper;
using ELearning.Core.Interfaces;
using ELearning.Core.Models;
using ELearning.Extensions;
using ELearning.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _unitOfWork.Users.GetItemAsync(u => u.Id == User.GetUserId(), ["Image"]);
            return View(_mapper.Map<UserProfile_ViewModel>(user));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserProfile_ViewModel model)
        {
            if (ModelState.IsValid) {
                var user = await _unitOfWork.Users.GetByIdAsync(User.GetUserId());

                _mapper.Map(model, user);

                var res = await _userManager.UpdateAsync(user);
                if (res.Succeeded)
                {
                    TempData["Success"] = "User updated successfully";
                    return RedirectToAction("Index");
                }
                else TempData["Error"] = "Failed to update user!";
            }
            else
            {
                TempData["Error"] = "Invalid data";
            }
            return View("~/Views/User/Index.cshtml", _mapper.Map<UserProfile_ViewModel>(model));
        }
    }
}
