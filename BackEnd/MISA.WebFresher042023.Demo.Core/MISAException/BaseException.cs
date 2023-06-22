using System.Text.Json;

namespace MISA.WebFresher042023.Demo.Core.MISAException
{
    public class BaseException
    {
        public BaseException()
        {
        }

        public BaseException(Exception exception, object ex)
        {
        }

        public int ErrorCode { get; set; }
        public string? DevMsg { get; set; }
        public string? UserMsg { get; set; }
        public string? TraceId { get; set; }
        public object? DataError { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
