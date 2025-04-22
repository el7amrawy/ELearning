using ELearning.Core.Common;
using ELearning.Core.DTOs;
using System.Net.Http.Json;
using ELearning.Core.Helpers;
using ELearning.Core.Interfaces;
using ELearning.Core.Interfaces.Services;
using Microsoft.Extensions.Options;
using System.Text;

namespace ELearning.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _client;
        private readonly PaymobSettings _settings;
        private readonly IUnitOfWork _unitOfWork;
        public PaymentService(HttpClient client, IOptions<PaymobSettings> options, IUnitOfWork unitOfWork)
        {
            _client = client;
            _settings = options.Value;
            _unitOfWork = unitOfWork;
        }
        public async Task<ServiceResult<string>> CreateOrderAsync(PaymentRequest request)
        {
            var refrence = new StringBuilder().Append(request.UserId + "_");

            foreach (var course in request.Courses)
            {
                refrence.Append(course.Id + "_");
            }

            refrence.Append(new Random().Next(999));


            var content = new
            {
                amount = request.Amount,
                currency = "EGP",
                payment_methods = _settings.PaymentMethods,
                items = request.Courses,
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
                special_reference = refrence.ToString(),
                notification_url = _settings.NotificationUrl,
                redirection_url = _settings.RedirectionUrl
            };

            var response = await _client.PostAsJsonAsync("intention", content);

            if (!response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<OrderErrorResponse>();
                return ServiceResult.Failure<string>(res.Details ?? "problem creating payment order");
            }

            var result = await response.Content.ReadFromJsonAsync<OrderResponse>();

            return ServiceResult.Success($"https://accept.paymob.com/unifiedcheckout/?publicKey={_settings.PublicKey}&clientSecret={result.Client_secret}");
        }

        private class OrderErrorResponse
        {
            public string Details { get; set; }
        }

        private class OrderResponse
        {
            public string Client_secret { get; set; }
        }
    }
}