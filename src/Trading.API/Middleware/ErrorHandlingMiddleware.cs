using FluentValidation;
using Trading.Core.Exceptions;
using Trading.Core.Models;

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
            catch (Exception ex)
            {
                var errorHandlingResponse = new ErrorHandlingResponse
                {
                    ErrorCode = ErrorCode.UNKNOWN,
                    Message = $"A server error has occured. Please contact support providing the following request id: {RequestIdMiddleware.GetOrSetRequestIdFromRequest(context)}",
                    RequestId = RequestIdMiddleware.GetOrSetRequestIdFromRequest(context)
                };

                var validationException = ex as ValidationException;
                var badRequestException = ex as BadRequestException;

                if (validationException != null)
                {
                    _logger.LogWarning(ex, "An API bad request has occurred.");
                    context.Response.StatusCode = 400;
                    errorHandlingResponse.ErrorCode = ErrorCode.VALIDATION;
                    errorHandlingResponse.Message = string.Concat(validationException.Errors.Select(x => x.ErrorMessage + Environment.NewLine));
                }
                else if (badRequestException != null)
                {
                    _logger.LogWarning(ex, "An API bad request has occurred.");
                    context.Response.StatusCode = 400;
                    errorHandlingResponse.ErrorCode = badRequestException.ErrorCode;
                    errorHandlingResponse.Message = badRequestException.ErrorCode.ToString();
                }
                else
                {
                    _logger.LogError(ex, "An API error has occurred.");
                    context.Response.StatusCode = 500;
                }

                await context.Response.WriteAsJsonAsync(errorHandlingResponse);
            }
        }
    }

    public class ErrorHandlingResponse
    { 
        public ErrorCode ErrorCode { get; set; }

        public required string Message { get; set; }

        public required string RequestId { get; set; }
    }
}
