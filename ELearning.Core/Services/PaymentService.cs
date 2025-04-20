using ELearning.Core.Common;
using ELearning.Core.DTOs;
using ELearning.Core.Helpers;
using ELearning.Core.Interfaces;
using ELearning.Core.Interfaces.Services;
using Microsoft.Extensions.Options;

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
        
        public async Task<ServiceResult> ProcessFreeCourseAsync(int courseId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<string>> ProcessPaymentAsync(PaymentRequest request)
        {
            throw new NotImplementedException();
        }

        
    }
}