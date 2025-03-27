using AutoMapper;
using ELearning.Core.Interfaces;
using ELearning.Core.Models;
using ELearning.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult SignUp() => View();
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUp_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new Student { FirstName = model.FirstName, LastName = model.LastName, UserName = model.Username, Email = model.Email, CreatedAt = DateTime.Now };
                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, "Student");
                    await _signInManager.SignInAsync(newUser, new AuthenticationProperties { ExpiresUtc = DateTime.Now.AddDays(10), IsPersistent = true });
                    TempData["Success"] = "User Created Successfully";
                    return RedirectToAction("Index", "Home");
                }
                foreach (var item in result.Errors)
                {
                    TempData["Error"] += item.Description + " ";
                }
            }
            else
            {
                TempData["Error"] = "Invalid Data";
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult SignIn() => View();
        [HttpPost]
        public async Task<IActionResult> SignIn(SignIn_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var user = await _userManager.FindByEmailAsync(model.Email);
                    var user = await _unitOfWork.Users.
                        GetItemAsync(u => u.NormalizedEmail == model.Email.ToUpper(), ["Image"]);

                    if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                        throw new Exception("Wrong email or password!!");

                    await _signInManager.SignInAsync(user, new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.Now.AddDays(10) });
                    //await _signInManager.SignInWithClaimsAsync(user, new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.Now.AddDays(10) }, [new Claim("ProfileImage", user.Image?.URL)]);

                    if (user.Image != null)
                        Response.Cookies.Append("ProfileImage", user.Image.URL, new CookieOptions { Expires = DateTime.Now.AddDays(10) });

                    TempData["Success"] = $"User {user.UserName} signed in successfully";
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult LogOut()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
