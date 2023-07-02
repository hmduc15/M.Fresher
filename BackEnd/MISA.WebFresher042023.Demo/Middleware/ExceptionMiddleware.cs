using MISA.WebFresher042023.Demo.Core.Enum;
using MISA.WebFresher042023.Demo.Core.MISAException;
using MISA.WebFresher042023.Demo.Core.Resources;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace MISA.WebFresher042023.Demo.Middleware
{
    /// <summary>
    /// Function handle Exception Middleware
    /// </summary>
    /// Author: HMDUC (16/06/2023)
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
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

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            context.Response.ContentType = "application/json";

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
    }
}
