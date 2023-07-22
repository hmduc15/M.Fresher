using MISA.WebFresher042023.Demo.Core.Enum;
using MISA.WebFresher042023.Demo.Core.MISAException;
using MISA.WebFresher042023.Demo.Core.Resources;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace MISA.WebFresher042023.Demo.Middleware
{
    /// <summary>
    /// Function xử lý lỗi ở Middleware
    /// </summary>
    /// Author: HMDUC (16/06/2023)
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context )
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Hàm xử lys lỗi khi FE gọi API , và trả về lỗi cho FE
        /// </summary>
        /// <param name="context">Http context</param>
        /// <param name="exception">Kiểu lỗi</param>
        /// Author: HMDUC (16/06/2023)
        #region HandleException
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            context.Response.ContentType = "application/json";

            //Lỗi không tìm thấy
            if (exception is NotFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;

                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = ((NotFoundException)exception).ErrorCode,
                        UserMsg = exception.Message,
                        DevMsg = ResourceVN.Error_Validate_Dev,
                    }.ToString() ?? ""
                    );
            }
            //Lỗi validate dữ liệu 
            else if (exception is ValidateException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = ((ValidateException)exception).ErrorCode,
                        DevMsg = exception.Message,
                        DataError = ((ValidateException)exception).DataError,
                    }.ToString() ?? ""
                    );
            }
            // Lỗi liên quan đến Server
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = context.Response.StatusCode,
                        UserMsg = ResourceVN.Error_Exception,
                        DevMsg = exception.Message,
                        TraceId = context.TraceIdentifier
                    }.ToString() ?? ""
                    );
            }
        } 
        #endregion
    }
}
