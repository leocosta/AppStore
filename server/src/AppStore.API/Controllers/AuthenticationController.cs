using AppStore.API.Filters;
using AppStore.API.Helpers;
using AppStore.Application.DataContracts.Authentication;
using AppStore.Application.DataContracts.Users;
using AppStore.Application.Services.Authentication;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppStore.API.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationAppService _authenticationAppService;

        public AuthenticationController(IAuthenticationAppService authenticacaoAppService)
        {
            _authenticationAppService = authenticacaoAppService;
        }

        // POST api/authentication
        [HttpPost]
        [RequestValidate]
        public HttpResponseMessage Authenticate(Credential credential)
        {
            var request = new AuthenticationRequest()
            {
                Credential = credential
            };

            var authResponse = _authenticationAppService.Authenticate(request);

            var response = Request.BuildResponse(HttpStatusCode.OK, authResponse);

            response.SetCookie(authResponse.TokenName,
                authResponse.AuthToken,
                authResponse.Expires);

            return response;
        }

        [HttpGet]
        public HttpResponseMessage Me()
        {
            var response = new GetUserResponse
            {
                User = this.UserSession
            };

            return Request.BuildResponse(HttpStatusCode.OK, response);
        }

        [HttpDelete]
        public HttpResponseMessage Lougout() {

            var response = Request.BuildResponse(HttpStatusCode.Accepted);

            response.SetCookie(_authenticationAppService.TokenName, 
                string.Empty, DateTime.Now.AddDays(-1));

            return response;
        }

    }
}