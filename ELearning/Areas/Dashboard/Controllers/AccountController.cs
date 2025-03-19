using AutoMapper;
using ELearning.Areas.Dashboard.ViewModels;
using ELearning.Core.Interfaces;
using ELearning.Core.Models;
using ELearning.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Areas.Dashboard.Controllers
{
    public class AccountController : BaseDashboardController
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _unitOfWork.Users.GetItemAsync(u => u.Id == User.GetUserId(), ["Image"]);
            return View(_mapper.Map<AdminProfile_ViewModel>(user));
        }
        [HttpGet]
        public IActionResult Settings() => View();
        [HttpGet, AllowAnonymous]
        public IActionResult SignIn() => View();
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> SignIn(SignIn_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("Email", "user is not found");
                    return View(model);
                }

                if (!await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    ModelState.AddModelError("Email", "user is not an admin");
                    return View(model);
                }

                if (!await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    ModelState.AddModelError("Password", "wrong password");
                    return View(model);
                }

                var properties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7)
                };

                await _signInManager.SignInAsync(user, properties);

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

    }
}
