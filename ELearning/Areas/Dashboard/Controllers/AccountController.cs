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
        private readonly IPhotoService _photoService;
        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMapper mapper, IUnitOfWork unitOfWork, IPhotoService photoService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _photoService = photoService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _unitOfWork.Users.GetItemAsync(u => u.Id == User.GetUserId(), ["Image"]);
            return View(_mapper.Map<AdminProfile_ViewModel>(user));
        }
        [HttpPost]
        public async Task<IActionResult> Index(AdminProfile_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _unitOfWork.Users.GetItemAsync(u => u.Id == User.GetUserId(), ["Image"]);
                _mapper.Map(model,user);

                if (model.Photo != null)
                {
                    if(user.Image != null)
                    {
                        await _photoService.DeletePhotoAsync(user.Image.PublicId);
                        _unitOfWork.Images.Delete(user.Image);
                    }

                    var res = await _photoService.AddPhotoAsync(model.Photo, 1024, 1024);

                    if (res.Error == null)
                        user.Image = new Image
                        {
                            CreatedAt = DateTime.UtcNow,
                            PublicId = res.PublicId,
                            URL = res.SecureUrl.AbsoluteUri
                        };

                    await _unitOfWork.CompleteAsync();

                    if (user.Image != null)
                        Response.Cookies.Append("ProfileImage", user.Image.URL, new CookieOptions { Expires = DateTime.Now.AddDays(7) });
                }
                TempData["Success"] = "Profile was updated successfully";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Settings() => View();
        [HttpPost]
        public async Task<IActionResult> Settings(ResetPassword_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(User.GetUserId().ToString());

                if(!await _userManager.CheckPasswordAsync(user, model.OldPassword))
                {
                    ModelState.AddModelError("OldPassword", "wrong password");
                    return View(model);
                }

                var res = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                if (!res.Succeeded)
                    ModelState.AddModelError("NewPassword", "Couldn't update password");
                else
                {
                    TempData["Success"] = "Password was updated successfully";
                    RedirectToAction("Settings");
                }
            }
            return View(model);
        }
        [HttpGet, AllowAnonymous]
        public IActionResult SignIn() => View();
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> SignIn(SignIn_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _unitOfWork.Users.
                       GetItemAsync(u => u.NormalizedEmail == model.Email.ToUpper(), ["Image"]);

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

                if (user.Image != null)
                    Response.Cookies.Append("ProfileImage", user.Image.URL, new CookieOptions { Expires = DateTime.Now.AddDays(7) });

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

    }
}