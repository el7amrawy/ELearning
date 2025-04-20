using ELearning.Core.Common;
using ELearning.Core.DTOs;
using System.Net.Http.Json;
using ELearning.Core.Helpers;
using ELearning.Core.Interfaces;
using ELearning.Core.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace ELearning.Core.Services
{
    public class PaymobPaymentProcessor : IPaymobPaymentProcessor
    {
        private readonly HttpClient _client;
        private readonly PaymobSettings _settings;
        private readonly IUnitOfWork _unitOfWork;
        public PaymobPaymentProcessor(HttpClient client, IOptions<PaymobSettings> options, IUnitOfWork unitOfWork)
        {
            _client = client;
            _settings = options.Value;
            _unitOfWork = unitOfWork;
        }
        public async Task<ServiceResult<string>> CreateOrderAsync(PaymentRequest request)
        {
            var content = new
            {
                amount = request.Amount,
                currency = "EGP",
                payment_methods = _settings.PaymentMethods,
                items = request.Courses.Select(c => new { Name = c.Title, Amount = c.Price, Description = c.SubTitle, Quantity = 1 }),
                billing_data = new
                {
                    apartment = "dumy",
                    first_name = request.BillingFirstName,
                    last_name = request.BillingLastName,
                    street = "dumy",
                    building = "dumy",
                    phone_number = "dummy",
                    city = "dumy",
                    country = "dumy",
                    email = request.BillingEmail,
                    floor = "dumy",
                    state = "dumy"
                },
                extras = new
                {
                    ee = 22
                },
                special_reference = Guid.NewGuid(),
                notification_url = _settings.NotificationUrl,
                redirection_url = _settings.RedirectionUrl
            };

            var response = await _client.PostAsJsonAsync("/intention", content);

            if (!response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<dynamic>();
                return ServiceResult.Failure<string>(res?.details ?? await response.Content.ReadAsStringAsync());
            }

            var result = await response.Content.ReadFromJsonAsync<OrderResponse>();

            return ServiceResult.Success($"https://accept.paymob.com/unifiedcheckout/?publicKey={_settings.PublicKey}&clientSecret={result.Client_secret}");
        }

        private class OrderResponse
        {
            public string Client_secret { get; set; }
        }
    }
}