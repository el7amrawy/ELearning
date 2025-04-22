namespace ELearning.Core.Helpers
{
    public class PaymobSettings
    {
        public string APIKey { get; set; }
        public string PublicKey { get; set; }
        public string SecretKey { get; set; }
        public string MerchantId { get; set; }
        public string IframeId { get; set; }
        public string BaseUrl { get; set; }
        public int[] PaymentMethods { get; set; }
        public string RedirectionUrl { get; set; }
        public string NotificationUrl { get; set; }
    }
}