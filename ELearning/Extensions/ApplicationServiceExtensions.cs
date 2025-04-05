using ELearning.Core.Helpers;
using ELearning.Core.Interfaces;
using ELearning.Core.Interfaces.Services;
using ELearning.Core.Services;
using ELearning.EF;
using ELearning.Filters;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("name=ConnectionStrings:default");
            });

            services.AddControllersWithViews();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<CloudinarySettings>(config.GetSection("Cloudinary"));
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<ICourseAccessService, CourseAccessService>();
            services.AddScoped<ISectionService, SectionService>();

            // Filters
            services.AddScoped<CourseOwnerFilter>();

            return services;
        }
    }
}