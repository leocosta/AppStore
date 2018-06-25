using AppStore.API.Helpers;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AppStore.API.Filters
{
    public class RequestValidate : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid) return;
            actionContext.Response = actionContext.Request.BuildResponse(HttpStatusCode.BadRequest, 
                new ApiHttpError(actionContext.ModelState));
        }
    }
}