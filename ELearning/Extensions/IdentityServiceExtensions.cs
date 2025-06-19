using ELearning.Core.Models;
using ELearning.EF;
using Microsoft.AspNetCore.Identity;

namespace ELearning.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentity<AppUser, IdentityRole<int>>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/SignIn";
                options.Events.OnRedirectToLogin += context => {
                    var area = context.Request.RouteValues["area"]?.ToString();

                    if (area == "Dashboard")
                    {
                        var loginUrl = $"/{area}/Account/SignIn";
                        //var returnUrl = context.Request.Path + context.Request.QueryString;

                        //context.Response.Redirect($"{loginUrl}?ReturnUrl={Uri.EscapeDataString(returnUrl)}");
                        context.Response.Redirect(loginUrl);
                    }
                    return Task.CompletedTask;
                };
                options.AccessDeniedPath = "/Error/AccessDenied";
            });

            return services;
        }
    }
}