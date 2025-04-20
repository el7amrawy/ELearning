using ELearning.Core.Helpers;
using ELearning.Core.Interfaces.Services;
using ELearning.Core.Services;
using Microsoft.Extensions.Options;

namespace ELearning.Extensions
{
    public static class PaymentServiceExtensions
    {
        public static IServiceCollection AddPaymentService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<PaymobSettings>(config.GetSection("Paymob"));

            services.AddHttpClient<IPaymobPaymentProcessor, PaymobPaymentProcessor>((serviceProvider, client) =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<PaymobSettings>>().Value;

                client.BaseAddress = new Uri(settings.BaseUrl);
                client.DefaultRequestHeaders.Add("Authorization", $"Token {settings.SecretKey}");
            });

            return services;
        }
    }
}