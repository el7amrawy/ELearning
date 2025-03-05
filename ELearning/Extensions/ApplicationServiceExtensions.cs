using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Interfaces;
using ELearning.Core.Models;
using ELearning.EF.Repositories;
using ELearning.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("name=ConnectionStrings:dev");
            });

            services.AddControllersWithViews();

            services.AddIdentity<AppUser, IdentityRole<int>>().AddEntityFrameworkStores<AppDbContext>();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/SignIn";
            });

            services.AddScoped<ICoursesRepository, CoursesRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
