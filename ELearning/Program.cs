using ELearning.Core.Interfaces;
using ELearning.EF;
using ELearning.Extensions;

namespace ELearning
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddApplicationServices(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseRouting();

            app.UseAuthorization();
       
            app.MapStaticAssets();

            app.MapControllerRoute("home", "{action=index}", new { controller = "home" });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            var scope = app.Services.CreateScope();
            var uow = scope.ServiceProvider.GetService<IUnitOfWork>();

            var seed = new Seed(uow);
            await seed.SeedAsync();

            app.Run();
        }
    }
}
