using System;
using System.Linq;
using System.Net.Http;

namespace AppStore.API.Helpers
{
    public static class HttpRequestMessageExtensions
    {
        public static string GetValue(this HttpRequestMessage request, string key)
        {
            if (key == null)
               throw new ArgumentNullException(nameof(key));

            Func<HttpRequestMessage, string, string> getValueFromQuerystring = (r, n) =>
                r.RequestUri?.ParseQueryString()?[n];

            Func<HttpRequestMessage, string, string> getValueFromHeader = (r, n) =>
                r.Headers.Contains(n) ? request.Headers.GetValues(n).FirstOrDefault() : null;

            Func<HttpRequestMessage, string, string> getValueFromCookie = (r, n) =>
                r.Headers.GetCookies(n).FirstOrDefault()?[n]?.Value;

            return (getValueFromQuerystring(request, key) ?? getValueFromHeader(request, key) ?? getValueFromCookie(request, key))?.Trim();
        }
    }
}