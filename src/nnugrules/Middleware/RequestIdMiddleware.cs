using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using nnugrules.Services;

namespace nnugrules.Middleware
{
    public class RequestIdMiddleware
    {
        private readonly ILogger<RequestIdMiddleware> _logger;
        private readonly RequestDelegate _next;
        private readonly IRequestId _requestId;

        public RequestIdMiddleware(RequestDelegate next, IRequestId requestId, ILogger<RequestIdMiddleware> logger)
        {
            _next = next;
            _requestId = requestId;
            _logger = logger;
        }

        public Task Invoke(HttpContext context, IRequestId requestId)
        {
            _logger.LogInformation($"Request {requestId.Id} executing.");

            return _next(context);
        }
    }
}