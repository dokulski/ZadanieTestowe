using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using Pumox.Server;
using System;
using System.Configuration;
using System.Net.Http.Headers;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]
namespace Pumox.Server
{
   
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            app.UseCors(CorsOptions.AllowAll);

            config.Formatters
                  .JsonFormatter
                  .MediaTypeMappings
                  .Add(new System.Net.Http.Formatting.RequestHeaderMapping("Accept",
                              "text/html",
                              StringComparison.InvariantCultureIgnoreCase,
                              true,
                              "application/json"));

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                 name:"DefaultApi",
                 routeTemplate: "{controller}/{action}/{id}",
                 defaults: new { 
                     controller="GetAll",
                     id = RouteParameter.Optional
                 }
            );

            app.UseWebApi(config);

           config.EnsureInitialized();

            app.Run(context =>
            {
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync($"Listening on port {ConfigurationManager.AppSettings["port"]}...");
            });
        }
    }
}
