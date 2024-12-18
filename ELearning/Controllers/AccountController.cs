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

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUp_ViewModel model,int x) {
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
        public IActionResult LogOut()
        {
            if (Request.Cookies.ContainsKey(".AspNetCore.Identity.Application"))
                Response.Cookies.Delete(".AspNetCore.Identity.Application");
            return RedirectToAction("Index", "Home");
        }
    }
}
