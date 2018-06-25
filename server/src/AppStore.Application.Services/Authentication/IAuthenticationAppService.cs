using System;
using AppStore.Application.DataContracts.Authentication;
using AppStore.Application.DataContracts.Users;

namespace AppStore.Application.Services.Authentication
{
    public interface IAuthenticationAppService
    {
        User User { get; }
        string TokenName { get; }
        DateTime? Expires { get; }

        AuthenticationResponse Authenticate(AuthenticationRequest request);
        void ValidateAccess(string securityToken, string resource, string action, string method);
        string CreateToken();
    }
}
