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
        public static ServiceResult<TResult> Success<TResult>(TResult result) => new ServiceResult<TResult>(true, result);
        public static ServiceResult<TResult> Failure<TResult>(string errorMessage) => new ServiceResult<TResult>(false, default, errorMessage);
    }
    public class ServiceResult<TResult>
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public TResult Result { get; set; }
        public ServiceResult(bool isSuccess, TResult result, string errorMessage = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Result = result;
        }
        public static ServiceResult<TResult> Success(TResult result) => new(true, result: result);
        public static ServiceResult<TResult> Failure(string errorMessage) => new(false, default, errorMessage);
    }
}