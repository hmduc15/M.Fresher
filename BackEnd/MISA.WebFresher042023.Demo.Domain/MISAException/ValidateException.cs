namespace MISA.WebFresher042023.Demo.Domain.MISAException
{
    /// <summary>
    /// Lỗi dữ liệu được truyền lên từ FE
    /// </summary>
    public class ValidateException : Exception
    {
        public int ErrorCode { get; set; }
        public object? DataError { get; set; }

        public ValidateException()
        {
        }

        public ValidateException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public ValidateException(object? dataError, int errorCode)
        {
            DataError = dataError;
            ErrorCode = errorCode;
        }

    }
}
