using AppStore.API.Helpers;
using AppStore.Application.Services.Authentication;
using AppStore.Application.Services.Exceptions;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AppStore.API.Handlers
{
    public class TokenValidatorHandler : DelegatingHandler
    {
        private readonly IAuthenticationAppService _authenticationAppService;

        public TokenValidatorHandler(IAuthenticationAppService authenticationAppService)
        {
            _authenticationAppService = authenticationAppService;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var resource = ((string)request.GetRouteData().Values["controller"]).ToLower();
            var action = request.GetRouteData().Values.ContainsKey("action") ? ((string)request.GetRouteData().Values["action"]).ToLower() : null;
            var method = request.Method.ToString().ToUpper();

            var authToken = request.GetValue(_authenticationAppService.TokenName);
            try
            {
                _authenticationAppService.ValidateAccess(authToken, resource, action, method);
            }
            catch (UnauthorizedOperationException ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message);
            }

            SaveContext(request, _authenticationAppService);

            var response = await base.SendAsync(request, cancellationToken);

            if(!IsLoggingOut(resource, method))
                ManageSecurityToken(_authenticationAppService, response, authToken);

            return response;
        }
        
        private static void ManageSecurityToken(IAuthenticationAppService authenticationAppService, 
            HttpResponseMessage response, string authToken)
        {
            if (string.IsNullOrWhiteSpace(authToken))
            {
                try
                {
                    authToken = response.Headers.GetValues(authenticationAppService.TokenName)?.FirstOrDefault();
                }
                catch { }
            }

            if (!string.IsNullOrWhiteSpace(authToken))
            {
                authToken = authenticationAppService.CreateToken();
                if (!string.IsNullOrWhiteSpace(authToken))
                {
                    response.SetCookie(authenticationAppService.TokenName, 
                        authToken, authenticationAppService.Expires);
                }
            }
        }

        private static bool IsLoggingOut(string resource, string method) =>
            resource.Equals("authentication", StringComparison.InvariantCultureIgnoreCase) && 
            method.Equals("delete", StringComparison.InvariantCultureIgnoreCase);

        private static void SaveContext(HttpRequestMessage request, 
            IAuthenticationAppService authenticationAppService) =>
            request.Properties.Add("User", authenticationAppService.User);
    }
}