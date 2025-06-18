using ELearning.Core.Helpers;
using ELearning.Core.Interfaces.Services;
using ELearning.Core.Services;

namespace ELearning.Extensions
{
    public static class MailServiceExtensions
    {
        public static IServiceCollection AddMailService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<SmtpSettings>(config.GetSection("Smtp"));

            services.AddScoped<IMailService, MailService>();

            return services;
        }
    }
}