using System.Net;

namespace Project3Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _environment;

        public ExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment environment)
        {
            _next = next;
            _environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            if (_environment.IsDevelopment())
            {
                await _next(context);
            }
            else
            {
                try
                {
                    await _next(context);
                }
                catch (Exception exception)
                {
                    await HandleException(context, exception);
                }
            }
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            string errorMessage = $"Internal Server Error: Exception: {exception.Message}";
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(errorMessage);
        }
    }
}
