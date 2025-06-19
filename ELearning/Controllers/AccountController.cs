using AutoMapper;
using ELearning.Core.Interfaces;
using ELearning.Core.Interfaces.Services;
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
        private readonly ICartService _cartService;
        private readonly IMailService _mailService;
        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IMapper mapper, ICartService cartService, IMailService mailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _cartService = cartService;
            _mailService = mailService;
        }
        [HttpGet]
        public IActionResult SignUp() => View();
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUp_ViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var newUser = new AppUser { FirstName = model.FirstName, LastName = model.LastName, UserName = model.Username, Email = model.Email, CreatedAt = DateTime.Now };
            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "Student");
                await _signInManager.SignInAsync(newUser, new AuthenticationProperties { ExpiresUtc = DateTime.Now.AddDays(10), IsPersistent = true });
                TempData["Success"] = "User Created Successfully";

                var cartRes = await _cartService.CreateAsync(newUser.Id);

                if (!cartRes.IsSuccess) TempData["Error"] += "," + cartRes.ErrorMessage;

                return RedirectToAction("Index", "Home");
            }

            if (result.Errors.Count() == 1)
                TempData["Error"] = result.Errors.First().Description;
            else
                foreach (var item in result.Errors)
                {
                    TempData["Error"] += item.Description;
                    if (item != result.Errors.Last())
                        TempData["Error"] += ",";
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
        [HttpGet]
        public IActionResult ForgotPassword() => View();
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPassword_ViewModel model) {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData["Error"] = "user doesn't exist";
                return View(model);
            }

            var otp = new Random().Next(100000, 999999).ToString();

            await _userManager
                .SetAuthenticationTokenAsync(user, "PasswordReset", "OTP", $"{otp}:{DateTime.UtcNow.AddMinutes(10).Ticks}");

            var mailRes = 
                await _mailService.SendAsync(user.Email, "Password Reset OTP", $"Your OTP is: {otp}. It expires in 10 minutes.");

            if (!mailRes.IsSuccess)
            {
                TempData["Error"] = "Failed to send OTP to your email.";
                return View(model);
            } 
            
            TempData["Success"] = "OTP sent to your email.";
            return RedirectToAction("VerifyOtp", new { model.Email });
        }
        public IActionResult VerifyOtp(string email) => View(new VerifyOTP_ViewModel { Email = email });
        [HttpPost]
        public async Task<IActionResult> VerifyOtp(VerifyOTP_ViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData["Error"] = "Invalid request.";
                return View(model);
            }

            var storedOtpData = await _userManager.GetAuthenticationTokenAsync(user, "PasswordReset", "OTP");
            if (string.IsNullOrEmpty(storedOtpData))
            {
                TempData["Error"] = "OTP not found or expired.";
                return View(model);
            }

            var parts = storedOtpData.Split(':');
            var storedOtp = parts[0];
            var expiry = long.Parse(parts[1]);

            if (DateTime.UtcNow.Ticks > expiry)
            {
                TempData["Error"] = "OTP expired.";
                return View(model);
            }

            if (storedOtp != model.OTP)
            {
                TempData["Error"] = "Invalid OTP.";
                return View(model);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            await _userManager.RemoveAuthenticationTokenAsync(user, "PasswordReset", "OTP");

            return RedirectToAction("ResetPassword", new { Token = token, model.Email });
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token) =>
            View(new ResetPassword_ViewModel { Email = email, Token = token });
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPassword_ViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData["Error"] = "Invalid request.";
                return View(model);
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (!result.Succeeded)
            {
                if (result.Errors.Count() == 1)
                    TempData["Error"] = result.Errors.First().Description;
                else
                    foreach (var item in result.Errors)
                    {
                        TempData["Error"] += item.Description;
                        if (item != result.Errors.Last())
                            TempData["Error"] += ",";
                    }
                return View(model);
            }

            TempData["Success"] = "Password reset successful.";
            return RedirectToAction("SignIn");
        }
    }
}