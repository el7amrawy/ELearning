namespace ELearning.Core.Helpers
{
    public class SmtpSettings
    {
        public string Username { get; set; }
        public string SenderEmail { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string SenderName { get; set; }
        public string Password { get; set; }
    }
}