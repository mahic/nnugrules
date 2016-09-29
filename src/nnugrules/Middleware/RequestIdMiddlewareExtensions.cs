using System;
using Microsoft.AspNetCore.Builder;

namespace nnugrules.Middleware
{
    public static class RequestIdMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestId(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestIdMiddleware>();
        }
    }
}
