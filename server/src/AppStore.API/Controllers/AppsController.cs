using AppStore.API.Helpers;
using AppStore.Application.Services.Apps;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppStore.API.Controllers
{
    public class AppsController : ApiController
    {
        private readonly IAppAppService _appAppService;

        public AppsController(IAppAppService appAppService)
        {
            _appAppService = appAppService;
        }

        // GET api/apps
        public HttpResponseMessage Get()
        {
            var response = _appAppService.GetAll();

            return Request.BuildResponse(HttpStatusCode.OK, response);
        }

        // GET api/apps/5
        public HttpResponseMessage Get(int id) {
            var response = _appAppService.Get(id);

            return Request.BuildResponse(HttpStatusCode.OK, response);
        }
    }
}
