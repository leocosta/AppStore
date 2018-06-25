using AppStore.API.Core.App_Start;
using System.Web;
using System.Web.Http;

namespace AppStore.API
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            SimpleInjectorWebApiInitializer.Initialize();
        }
    }
}
