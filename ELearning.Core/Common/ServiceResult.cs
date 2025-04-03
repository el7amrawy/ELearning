namespace ELearning.Core.Common
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public ServiceResult(bool isSuccess, string errorMessage = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }
        public static ServiceResult Success() => new(true);
        public static ServiceResult Failure(string errorMessage) => new(false, errorMessage);
    }
}