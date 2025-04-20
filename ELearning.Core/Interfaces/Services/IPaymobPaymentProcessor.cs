using ELearning.Core.Common;
using ELearning.Core.DTOs;

namespace ELearning.Core.Interfaces.Services
{
    public interface IPaymobPaymentProcessor
    {
        public Task<ServiceResult<string>> CreateOrderAsync(PaymentRequest request);
    }
}