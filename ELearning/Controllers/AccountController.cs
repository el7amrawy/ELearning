using ELearning.Core.Models;
using ELearning.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    public class AccountController(SignInManager<User> signInManager, UserManager<User> userManager) : Controller
    {
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly UserManager<User> _userManager = userManager;

        [TempData]
        public string Error { get; set; }
        [TempData]
        public string Success { get; set; }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUp_ViewModel model) {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(new User { FirstName = model.FirstName, LastName = model.LastName, Email = model.Email }, model.Password);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    await _signInManager.SignInAsync(user, new AuthenticationProperties { ExpiresUtc = DateTime.Now.AddDays(10), IsPersistent = true });
                    Success = "User Created Successfully";
                    return RedirectToAction("Index", "Home");
                }
                Error = "Couldn't Create User";
            }
            else
            {
                Error = "Wrong Info";
            }
            return View();
        }
    }
}
