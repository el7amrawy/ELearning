using ELearning.Core.Common;

namespace ELearning.Core.Interfaces.Services
{
    public interface IMailService
    {
        public Task<ServiceResult> SendAsync(string toEmail, string subject, string body);
        //public void Send();
    }
}