using AppStore.Application.DataContracts.Users;
using System.Web.Http;

namespace AppStore.API.Controllers
{
    public abstract class BaseController : ApiController
    {
        internal User UserSession
        {
            get
            {
                if (Request.Properties.ContainsKey("User"))
                    return Request.Properties["User"] as User;

                return null;
            }
            set
            {
                Request.Properties["User"] = value;
            }
        }
    }
}