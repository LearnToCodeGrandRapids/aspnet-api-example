using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace ExampleApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Allow using attributes to define routes for API end points.
            config.MapHttpAttributeRoutes();

            // Set up a default route in the event a controller doens't provide
            // attributes for routing.
            config.Routes.MapHttpRoute(
                name: "default",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional});

            // Enable camel-cased property names for any JSON returned by the API.
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}