using AutoMapper;
using ELearning.Core.Interfaces;
using ELearning.Core.Models;
using ELearning.Extensions;
using ELearning.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    [Authorize(Roles ="Student")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPhotoService _photoService;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _photoService = photoService;
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
        [HttpPost]
        public async Task<IActionResult> UploadPhoto(IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                var user = await _unitOfWork.Users.GetItemAsync(u => u.Id == User.GetUserId(), ["Image"]);
                
                if (user.Image != null)
                {
                    await _photoService.DeletePhotoAsync(user.Image.PublicId);
                    _unitOfWork.Images.Delete(user.Image);
                }

                var res = await _photoService.AddPhotoAsync(ImageFile, 500, 500);
                if (res.Error != null)
                {
                    TempData["Error"] = "Problem adding image";
                    return this.RedirectToPrevious();
                }

                user.Image = new Image { CreatedAt = DateTime.Now, PublicId = res.PublicId, URL = res.SecureUrl.AbsoluteUri };

                _unitOfWork.Users.Update(user);

                if (await _unitOfWork.CompleteAsync() > 0)
                {
                    TempData["Success"] = "Added photo Successfully";
                    Response.Cookies.Append("ProfileImage", user.Image.URL, new CookieOptions { Expires = DateTime.Now.AddDays(10) });
                }
                else
                    TempData["Error"] = "Problem saving photo";
            }
            else
            {
                TempData["Error"] = "Invalid Input";
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult BecomeInstructor() => View();
        [HttpPost] 
        public async Task<IActionResult> BecomeInstructor(int id)
        {
            var user = await _userManager.FindByIdAsync(User.GetUserId().ToString());

            if(await _userManager.IsInRoleAsync(user, "Instructor"))
            {
                TempData["Error"] = "You are already an instructor";
                return this.RedirectToPrevious();
            }

            await _userManager.AddToRoleAsync(user, "Instructor");

            TempData["Success"] = "You are now an instructor";
            return this.RedirectToPrevious();
        }
    }
}
