using System;
using System.Diagnostics;
using System.Web;
using System.Web.Http;

namespace MongoDb.WebUI
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
