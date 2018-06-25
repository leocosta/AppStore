using System;
using System.Collections;
using System.Linq;
using AppStore.Domain.Common;
using AppStore.Infra.CrossCutting.Encryption.Extension;
using AppStore.Infra.CrossCutting.Serialization;

namespace AppStore.Domain.Users
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        private readonly int _sessionTimeout = 60;
        private readonly string _tokenName = "AuthToken";
        private readonly string[] _publicResources = { "authentication.post", "users.post", "apps.get" };

        #endregion

        #region Properties

        public virtual User User { get; private set; }

        public DateTime Expires
        {
            get
            {
                return DateTime.Now.AddMinutes(_sessionTimeout);
            }
        }

        public string TokenName
        {
            get
            {
                return _tokenName;
            }
        }

        #endregion

        #region Constructors

        public AuthenticationService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        #endregion

        #region Authenticate

        public virtual void Authenticate(string email, string password)
        {
            User = _userRepository.Get(email);
            
            if (User == null)
                throw new UnauthorizedException("Invalid User.");

            User.ValidateAccess(password);
            _unitOfWork.Commit();
        }

        #endregion

        #region ValidateAccess

        public virtual void ValidateAccess(string securityToken, string resource, string action, string method)
        {
            var expectedResource = resource + (string.IsNullOrEmpty(action) ? "" : "." + action) + "." + method.ToLower();
            var isOptionsMethod = method == "OPTIONS";
            var isPublic = _publicResources.Contains(expectedResource) || isOptionsMethod;
            if (isPublic)
                return;

            if (securityToken == null)
                throw new UnauthorizedException("SecurityToken cannot be empty.");

            Hashtable sessionData;
            try
            {
                sessionData = DecryptSecurityToken(securityToken);
            }
            catch (FormatException)
            {
                throw new UnauthorizedException("Invalid SecurityToken.");
            }

            if ((DateTime)sessionData["Expires"] < DateTime.Now.ToUniversalTime())
                throw new UnauthorizedException("Session expired.");

            User = _userRepository.Get((int)sessionData["UserId"]);

            if (User == null)
                throw new UnauthorizedException("User not found.");
        }

        #endregion

        #region CreateToken

        public virtual string CreateToken()
        {
            if (User == null)
                return null;

            var sessionData = new Hashtable();

            sessionData["UserId"] = User.UserId;
            sessionData["Expires"] = Expires;

            string securityToken = EncryptSecurityToken(sessionData);

            return securityToken;
        }

        #endregion

        #region Criptography

        private static string EncryptSecurityToken(Hashtable securityData)
        {
            return securityData.Serialize().Encrypt();
        }

        private static Hashtable DecryptSecurityToken(string securityToken)
        {
            return securityToken.Decrypt().Deserialize<Hashtable>();
        }

        #endregion
    }
}