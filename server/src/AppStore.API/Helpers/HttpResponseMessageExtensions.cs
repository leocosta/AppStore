using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace AppStore.API.Helpers
{
    public static class HttpResponseMessageExtensions
    {
        public static void SetCookie(this HttpResponseMessage response, string cookieName, string cookieValue, DateTime? expirationDate)
        {
            var httpCookies = (System.Web.Configuration.HttpCookiesSection)ConfigurationManager
                .GetSection("system.web/httpCookies");

            var domain = getHostName(response);

            var cookieHeaderValue = new CookieHeaderValue(cookieName, cookieValue)
            {
                Domain = domain,
                HttpOnly = httpCookies?.HttpOnlyCookies ?? false,
                Secure = HttpContext.Current?.Request.IsSecureConnection ?? false,
                Path = "/",
                Expires = expirationDate,
            };

            response.Headers.AddCookies(new CookieHeaderValue[] { cookieHeaderValue });

            if (response.Headers.Contains(cookieName))
                response.Headers.Remove(cookieName);

            response.Headers.Add(cookieName, cookieValue);
        }

        private static string getHostName(HttpResponseMessage response) =>
            response?.RequestMessage?.Headers.Host;
    }
}