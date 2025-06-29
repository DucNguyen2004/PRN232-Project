using DTOs;

namespace PRN232Project.Utils
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception caught by middleware");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Default to 500
            int statusCode = StatusCodes.Status500InternalServerError;
            string title = "Internal Server Error";
            string message = "An unexpected error occurred.";
            string? innerMessage = exception.InnerException?.Message;

            if (exception is ProblemException pe)
            {
                statusCode = pe.StatusCode;
                title = pe.Title;
                message = pe.Message;
                innerMessage = pe.InnerException?.Message;
            }

            var errorResponse = new ApiResponseDto<object>
            {
                Status = "error",
                Data = null,
                Error = new ErrorDetails
                {
                    Title = title,
                    StatusCode = statusCode,
                    Message = message,
                    InnerMessage = innerMessage
                }
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
