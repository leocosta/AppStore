using System;
using AppStore.Application.DataContracts.Authentication;
using AppStore.Application.Services.Exceptions;
using AppStore.Application.Services.Users;
using AppStore.Domain.Users;
using UsersDataContract = AppStore.Application.DataContracts.Users;

namespace AppStore.Application.Services.Authentication
{
    public class AuthenticationAppService : IAuthenticationAppService
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationAppService(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public DateTime? Expires
        {
            get
            {
                return _authenticationService.Expires;
            }
        }

        public string TokenName
        {
            get
            {
                return _authenticationService.TokenName;
            }
        }

        public UsersDataContract.User User
        {
            get
            {
                return _authenticationService.User?.ToDataContract();
            }
        }

        public AuthenticationResponse Authenticate(AuthenticationRequest request)
        {
            var response = new AuthenticationResponse();
            try
            {
                _authenticationService.Authenticate(request.Credential.Email, request.Credential.Password);

                response.User = _authenticationService.User.ToDataContract();
                response.AuthToken = _authenticationService.CreateToken();
                response.Expires = _authenticationService.Expires;
                response.TokenName = _authenticationService.TokenName;
            }
            catch (UnauthorizedException ex)
            {
                response.Exception = new UnauthorizedOperationException(ex.Message);
            }
            catch (NotAllowedException ex)
            {
                response.Exception = new NotAllowedOperationException(ex.Message);
            }
            catch (Exception ex)
            {
                response.Exception = new InvalidResourceOperationException(ex.Message);
            }

            return response;
        }

        public string CreateToken() => _authenticationService.CreateToken();

        public void ValidateAccess(string securityToken, string resource, string action, string method)
        {
            try
            {
                _authenticationService.ValidateAccess(securityToken, resource, action, method);
            }
            catch (UnauthorizedException ex)
            {
                throw new UnauthorizedOperationException(ex.Message);
            }
            catch (NotAllowedException ex)
            {
                throw new NotAllowedOperationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new InvalidResourceOperationException(ex.Message);
            }
        }
    }
}
