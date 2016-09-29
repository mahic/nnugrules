using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace nnugrules.Middleware
{
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<RequestCultureOptions> _options;

        public RequestCultureMiddleware(RequestDelegate next, IOptions<RequestCultureOptions> options)
        {
            _next = next;
            _options = options;
        }

        public Task Invoke(HttpContext httpContext)
        {
            CultureInfo requestCulture = null;

            var cultureQuery = httpContext.Request.Query["culture"];
            if (!string.IsNullOrWhiteSpace(cultureQuery))
            {
                requestCulture = new CultureInfo(cultureQuery);
            }
            else
            {
                requestCulture = _options.Value.DefaultCulture;
            }

            if (requestCulture != null)
            {
                CultureInfo.CurrentCulture = requestCulture;
                CultureInfo.CurrentUICulture = requestCulture;
            }

            return _next(httpContext);
        }
    }
}
