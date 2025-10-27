using Trading.Core.Exceptions;

namespace Trading.API.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException ex)
            {
                _logger.LogWarning(ex, "An API bad request has occurred.");

                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(
                    new ErrorHandlingResponse
                    {
                        ErrorCode = ex.ExceptionCode,
                        Message = ex.ExceptionCode.ToString(),
                        RequestId = RequestIdMiddleware.GetOrSetRequestIdFromRequest(context)
                    }
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An API error has occurred.");

                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(
                    new ErrorHandlingResponse
                    {
                        ErrorCode = ExceptionCode.UNKNOWN,
                        Message = $"A server error has occured. Please contact support providing the following request id: {RequestIdMiddleware.GetOrSetRequestIdFromRequest(context)}",
                        RequestId = RequestIdMiddleware.GetOrSetRequestIdFromRequest(context)
                    }
                );
            }
        }
    }

    public class ErrorHandlingResponse
    { 
        public ExceptionCode ErrorCode { get; set; }

        public required string Message { get; set; }

        public required string RequestId { get; set; }
    }
}
