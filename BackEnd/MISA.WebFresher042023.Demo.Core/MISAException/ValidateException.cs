namespace MISA.WebFresher042023.Demo.Core.MISAException
{
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
