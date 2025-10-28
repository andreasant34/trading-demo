
using Microsoft.Extensions.Primitives;

namespace Trading.API.Middleware
{
    public class RequestIdMiddleware : IMiddleware
    {
        public const string RequestIdHeaderKey = "X-Request-Id";
        private readonly ILogger _logger;

        public RequestIdMiddleware(ILogger<RequestIdMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var requestId = GetOrSetRequestIdFromRequest(context);

            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["RequestId"] = requestId
            }))
            {
                await next(context);
            }
        }

        public static string GetOrSetRequestIdFromRequest(HttpContext context)
        {
            _ = context.Response.Headers.TryGetValue(RequestIdHeaderKey, out var requestIdHeader);

            var requestId = requestIdHeader.FirstOrDefault() ?? Guid.NewGuid().ToString();

            context.Response.Headers.Append(RequestIdHeaderKey, requestId);
            return requestId;
        }
    }
}
