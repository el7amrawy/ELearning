using System.Net.Mail;
using System.Net;
using ELearning.Core.Common;
using ELearning.Core.Helpers;
using ELearning.Core.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace ELearning.Core.Services
{
    public class MailService : IMailService
    {
        private readonly SmtpSettings _smtpSettings;

        public MailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task<ServiceResult> SendAsync(string toEmail, string subject, string body)
        {
            try
            {
                using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
                {
                    Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                    EnableSsl = true
                };
                await client.SendMailAsync(_smtpSettings.SenderEmail, toEmail, subject, body);

                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure(ex.Message);
            }
        }
    }
}