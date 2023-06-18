using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace MVCCore.Middleware
{
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate next;

        public RequestCultureMiddleware(RequestDelegate next)
        {
            this.next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            var currentLang = context.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];
            var browserLang = context.Request.Headers["Accept-Language"].ToString()[..2];
            if (string.IsNullOrEmpty(currentLang))
            {
                var culture = string.Empty;
                switch (browserLang)
                {
                    case "ar":
                        culture = "ar-EG";
                        break;
                    case "de":
                        culture = "de-DE";
                        break;
                    default:
                        culture = "en-US";
                        break;
                }

                var requestCulture = new RequestCulture(culture, culture);
                context.Features.Set<IRequestCultureFeature>(new RequestCultureFeature(requestCulture, null));


                CultureInfo.CurrentCulture = new CultureInfo(culture);
                CultureInfo.CurrentUICulture = new CultureInfo(culture);

            }
            await next(context);
        }

    }
}
