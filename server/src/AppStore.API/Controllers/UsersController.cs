using System.Net;
using System.Net.Http;
using AppStore.API.Filters;
using AppStore.Application.DataContracts.Users;
using AppStore.Application.Services.Users;
using AppStore.API.Helpers;
using System.Web.Http;

namespace AppStore.API.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserAppService _userAppService;

        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        // GET api/users/5
        public HttpResponseMessage Get(int id)
        {
            var response = _userAppService.Get(id);

            return Request.BuildResponse(HttpStatusCode.OK, response);
        }

        // POST api/users
        [HttpPost]
        [RequestValidate]
        public HttpResponseMessage Post(ValidatableUser user)
        {
            var request = new CreateUserRequest()
            {
                User = user  
            };
            var response = _userAppService.Create(request);

            return Request.BuildResponse(HttpStatusCode.OK, response);
        }

       // GET api/users/5/creditcards
       [HttpGet]
        public HttpResponseMessage CreditCards(int parentId)
        {
            var response = _userAppService.GetCreditCards(parentId);
            
            return Request.BuildResponse(HttpStatusCode.OK, response);
        }
    }
}
