using System;

namespace AppStore.Domain.Users
{
    public interface IAuthenticationService
    {
        DateTime Expires { get; }
        string TokenName { get; }
        User User { get; }

        void Authenticate(string email, string password);
        string CreateToken();
        void ValidateAccess(string securityToken, string resource, string action, string method);
    }
}