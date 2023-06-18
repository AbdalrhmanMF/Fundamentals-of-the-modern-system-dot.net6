using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace MVCCore.Middleware
{
    public static class RequestCultureMiddlewareExtentions
    {
        public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestCultureMiddleware>();
        }


    }
}
