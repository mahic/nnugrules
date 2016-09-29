using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace nnugrules.Middleware
{
    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestCultureMiddleware>();
        }

        public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder builder, RequestCultureOptions options)
        {
            return builder.UseMiddleware<RequestCultureMiddleware>(Options.Create(options));
        }
    }
}