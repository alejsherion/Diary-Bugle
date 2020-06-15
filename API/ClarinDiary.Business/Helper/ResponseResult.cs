namespace ClarinDiary.Business.Helper
{
    public class ResponseResult<T> where T: class
    {
        protected ResponseResult(bool isSuccessful, T result, string message)
        {
            IsSuccessful = isSuccessful;
            Result = result;
            Message = message;
        }

        public bool IsSuccessful { get; protected set; }
        public string Message { get; protected set; }
        public T Result { get; protected set; }

        public static ResponseResult<T> Success() => new ResponseResult<T>(true, null, "");
        public static ResponseResult<T> Success(T result) => new ResponseResult<T>(true, result, "");
        public static ResponseResult<T> Error(string errorMessage) => new ResponseResult<T>(false, null, errorMessage);
    }
}
