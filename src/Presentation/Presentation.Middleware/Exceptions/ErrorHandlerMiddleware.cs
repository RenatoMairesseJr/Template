using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Presentation.Middleware.Exceptions
{
    /// <summary>
    /// Attribute for Exception Handling
    /// </summary>
    /// 
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                string message;

                switch (error)
                {
                    case BadRequestException:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        message = error.Message;
                        break;
                    case NotFoundException:
                        // not found error
                        message = HttpStatusCode.NotFound.ToString();
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        message = error.Message;
                        break;
                    default:
                        // unhandled error
                        message = HttpStatusCode.InternalServerError.ToString();
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }


                ///TODO Format custom message error
                ///save to LOG TOOL: such as SPLUNK / New Relic / etc
                var minException = new MinException
                {
                    User = context.Response.HttpContext?.User?.Identity?.Name ?? string.Empty,
                    Method = context.Response.HttpContext?.Request?.Method ?? "",
                    Path = context.Response.HttpContext?.Request?.Path.ToString() ?? "",
                    QueryString = context.Response.HttpContext?.Request?.QueryString.ToString() ?? "",
                    Body = context.Response.HttpContext?.Request?.Body.ToString() ?? "",
                    Message = error.Message,
                    Exception = error.InnerException != null ? "\n\nInnerException Trace:\n\n" + error.InnerException?.ToString() : ""
                };
                //logger.Error(minException);


                ///TODO Format return message
                var result = JsonSerializer.Serialize(new
                {
                    context.Response.StatusCode,
                    message,
                    //StackTrace = error.InnerException != null ? "\n\nInnerException Trace:\n\n" + error.InnerException?.ToString() : ""
                });
                await response.WriteAsync(result);
            }
        }
    }
}
