using System.Web;
using System.Web.Http;

namespace Canary
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            
        }
    }
}
