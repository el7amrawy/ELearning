using ELearning.Core.Interfaces;
using ELearning.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ELearning.EF
{
    public class Seed
    {
        private readonly IUnitOfWork _unitOfOfWork;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public Seed(IUnitOfWork unitOfWork, RoleManager<IdentityRole<int>> roleManager, UserManager<AppUser> userManager)
        {
            _unitOfOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        private async Task SeedLanguages()
        {
            if (await _unitOfOfWork.Languages.CountAsync() > 0)
                return;

            await _unitOfOfWork.Languages.AddRangeAsync(
            [
                new Language { Name="English"},
                new Language { Name="Arabic"}
            ]);
        }
        private async Task SeedLevels()
        {
            if (await _unitOfOfWork.Levels.CountAsync() > 0)
                return;

            await _unitOfOfWork.Levels.AddRangeAsync(new List<Level>
            {
                new Level{Name="Beginner"},
                new Level{Name="Intermediate"},
                new Level{Name="Advanced"}
            });
        }
        private async Task SeedCourseStatus()
        {
            if (await _unitOfOfWork.CoursesStatus.CountAsync() > 0)
                    return;

            await _unitOfOfWork.CoursesStatus.AddRangeAsync(
            [
                new CourseStatus{Name="Published"},
                new CourseStatus{Name="Draft"},
                new CourseStatus{Name="Canceled"},
            ]);
        }
        public async Task SeedAsync()
        {
            await SeedCourseStatus();
            await SeedLanguages();
            await SeedLevels();

            await _unitOfOfWork.CompleteAsync();

            await SeedRoles();
            await SeedAdmins();
        }
        private async Task SeedRoles()
        {
            if (await _roleManager.Roles.AnyAsync()) return;

            await _roleManager.CreateAsync(new IdentityRole<int> { Name = "Admin", ConcurrencyStamp = Guid.NewGuid().ToString() });

            await _roleManager.CreateAsync(new IdentityRole<int> { Name = "Instructor", ConcurrencyStamp = Guid.NewGuid().ToString() });

            await _roleManager.CreateAsync(new IdentityRole<int> { Name = "Student", ConcurrencyStamp = Guid.NewGuid().ToString() });
        }
        private async Task SeedAdmins()
        {
            if ((await _userManager.GetUsersInRoleAsync("Admin")).Count > 0)
                return;

            var admin = new AppUser
            {
                FirstName = "Admin",
                LastName = "Jr",
                UserName = "admin",
                Email = "admin@mail.com",
                CreatedAt = DateTime.UtcNow,
            };
            await _userManager.CreateAsync(admin, "Pas$w0rd");
            await _userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}