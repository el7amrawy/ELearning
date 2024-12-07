using ELearning.Core.Interfaces;
using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;
using ELearning.EF;
using ELearning.EF.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ELearning
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConfigureServices(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("name=ConnectionStrings:dev");
            });

			services.AddControllersWithViews();

            services.AddIdentity<User, IdentityRole<int>>().AddEntityFrameworkStores<AppDbContext>();
            services.AddScoped<ICoursesRepository,CoursesRepository>();

            services.AddScoped<IUnitOfWork,UnitOfWork>();
        }
    }
}
