using AppStore.Application.DataContracts;
using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace AppStore.API.Helpers
{
    /// <summary>
    /// Extension methods for HttpResponse
    /// </summary>
    public static class HttpResponseBuilder
    {

        /// <summary>
        /// Build a HttpResponseMessage based on the HttpRequest message and the operation response that 
        /// an Application service provided
        /// </summary>
        /// <param name="requestMessage">The HttpRequestMessage that came with the HTTP request to the web API</param>
        /// <param name="baseResponse">The populated response that an Application Service, e.g. ICustomerService generated</param>
        /// <returns>A HttpResponseMessage that can be sent to the requesting client</returns>
        public static HttpResponseMessage BuildResponse(this HttpRequestMessage requestMessage, HttpStatusCode statusCode = HttpStatusCode.OK, BaseResponse baseResponse = null)
        {
            if (baseResponse?.Exception != null)
            {
                statusCode = baseResponse.Exception.ToHttpStatusCode();
                HttpResponseMessage message = new HttpResponseMessage(statusCode);
                message.Content = new StringContent(baseResponse.Exception.Message);
                throw new HttpResponseException(message);
            }
            return requestMessage.CreateResponse(statusCode, baseResponse);
        }

        public static HttpResponseMessage BuildResponse<T>(this HttpRequestMessage requestMessage, 
            HttpStatusCode statusCode = HttpStatusCode.OK, BaseResponse baseResponse = null, 
            Expression<Func<T, object>> selector = null)
        {
            if (baseResponse.Exception != null)
            {
                statusCode = baseResponse.Exception.ToHttpStatusCode();
                HttpResponseMessage message = new HttpResponseMessage(statusCode);
                message.Content = new StringContent(baseResponse.Exception.Message);
                throw new HttpResponseException(message);
            }

            var prop = (PropertyInfo)((MemberExpression)selector.Body).Member;
            var value = prop.GetValue(baseResponse);

            return requestMessage.CreateResponse(statusCode, value);
        }
    }
}