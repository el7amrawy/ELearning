using ELearning.Core.Interfaces;
using ELearning.Core.Models;
using ELearning.EF;
using ELearning.Extensions;
using Microsoft.AspNetCore.Identity;

namespace ELearning
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddApplicationServices(builder.Configuration);

            builder.Services.AddIdentityService(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error/ServerError");
                app.MapFallbackToController("Handle404", "Error");
            }

            app.UseRouting();

            app.UseAuthorization();
       
            app.MapStaticAssets();

            app.MapControllerRoute(
            name: "Dashboard",
            pattern: "{area}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute("home", "{action=index}", new { controller = "home" });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            var scope = app.Services.CreateScope();
            var uow = scope.ServiceProvider.GetService<IUnitOfWork>();
            var rm = scope.ServiceProvider.GetService<RoleManager<IdentityRole<int>>>();
            var um = scope.ServiceProvider.GetService<UserManager<AppUser>>();

            var seed = new Seed(uow, rm, um);
            await seed.SeedAsync();

            app.Run();
        }
    }
}