using System.Text.Json;

namespace MISA.WebFresher042023.Demo.Core.MISAException
{
    /// <summary>
    /// Base xử lý lỗi
    /// </summary>
    public class BaseException
    {
        public BaseException()
        {
        }

        public BaseException(Exception exception, object ex)
        {
        }

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Message cho Dev
        /// </summary>
        public string? DevMsg { get; set; }

        /// <summary>
        /// Message cho User
        /// </summary>
        public string? UserMsg { get; set; }

        /// <summary>
        /// TraceId của lỗi
        /// </summary>
        public string? TraceId { get; set; }

        /// <summary>
        /// Object lỗi
        /// </summary>
        public object? DataError { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
