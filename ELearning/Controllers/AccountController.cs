using ELearning.Core.Models;
using ELearning.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    public class AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) : Controller
    {
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly UserManager<AppUser> _userManager = userManager;
        [TempData]
        public string Success { get; set; }
        [HttpGet]
        public IActionResult SignUp() => View();
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUp_ViewModel model) {
            if (ModelState.IsValid)
            {
                var newUser = new AppUser { FirstName = model.FirstName, LastName = model.LastName,UserName=model.Username ,Email = model.Email ,CreatedAt=DateTime.Now};
                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    await _signInManager.SignInAsync(user, new AuthenticationProperties { ExpiresUtc = DateTime.Now.AddDays(10), IsPersistent = true });
                    TempData["Success"] = "User Created Successfully";
                    return RedirectToAction("Index", "Home");
                }
                TempData["Error"] = "Couldn't create user";
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
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                        throw new Exception("Wrong email or password!!");

                    await _signInManager.SignInAsync(user, new AuthenticationProperties { IsPersistent=true, ExpiresUtc=DateTime.Now.AddDays(10)});
                    
                    Success = $"User {user.UserName} signed in successfully";
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    TempData["Error"]=ex.Message;
                }
            }
            else
            {
                TempData["Error"] = "Invalid Input!";
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult LogOut()
        {
            if (Request.Cookies.ContainsKey(".AspNetCore.Identity.Application"))
                Response.Cookies.Delete(".AspNetCore.Identity.Application");
            return RedirectToAction("Index", "Home");
        }
    }
}
