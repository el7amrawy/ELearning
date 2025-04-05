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
    public class ServiceResult<TResult> where TResult : class
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public TResult Result { get; set; }
        public ServiceResult(bool isSuccess, string errorMessage = null, TResult result = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Result = result;
        }
        public static ServiceResult<TResult> Success(TResult result) => new(true, result: result);
        public static ServiceResult<TResult> Failure(string errorMessage) => new(false, errorMessage);
    }
}