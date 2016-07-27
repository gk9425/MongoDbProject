using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace MongoDb.WebUI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //ApiByAction
            config.Routes.MapHttpRoute("ApiByAction",
                "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional }
                );
            //Default Api
            config.Routes.MapHttpRoute("DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
                );
            //
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            //configuración para solo serializar en json
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
