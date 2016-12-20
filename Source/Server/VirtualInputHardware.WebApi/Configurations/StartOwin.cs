namespace VirtualInputHardware.WebApi.Configurations
{
    using System.Web.Http;
    using Infrastructure.Middlewares.OwinContext;
    using Microsoft.AspNet.SignalR;
    using Microsoft.Owin.Cors;
    using Owin;
    using VirtualInputHardware.WebApi.Infrastructure.Handlers;
    using VirtualInputHardware.WebApi.Infrastructure.Middlewares;

    public class StartOwin
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Add(new StreamReadingDelegatingHandler());

            appBuilder.UseCors(CorsOptions.AllowAll);
            appBuilder.MapSignalR();
            appBuilder.Use<OwinContextMiddleware>();
            appBuilder.Use<RequestBufferingMiddleware>();
            appBuilder.UseWebApi(config);
        }
    }
}