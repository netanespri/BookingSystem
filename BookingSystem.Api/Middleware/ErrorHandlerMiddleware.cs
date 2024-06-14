using BookingSystem.Application.Responses;
using System.Net;
using System.Text.Json;

namespace BookingSystem.Api.Middleware
{
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
                response.StatusCode = error switch
                {
                    FluentValidation.ValidationException  => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError,// Unhandled error
                };

                var errorContent = GetErrorContent(error);               

                await response.WriteAsync(errorContent);
            }
        } 
        
        private static string GetErrorContent(Exception error)
        {
            var errorMessage = (error is FluentValidation.ValidationException)
                                    ? error.Message
                                    : "Server error";
            var responseModel = new Response<string>
            (
                data: string.Empty,
                succeeded: false,
                message: errorMessage
            );

            var content = JsonSerializer.Serialize(responseModel);

            return content;
        }
    }
}