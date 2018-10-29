using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ASP.NET_CORE_WEB.API
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.AspNetCore.Routing.Constraints;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();//.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<ConfigurationContainer>(Configuration.GetSection("WebApiConfig"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
       //Calling UseMvc without builder will not add a default route. Instead the application will construct routes based on the Route attribute 
            //on controller class.
            //However when we add MapRoute, we can pass the requests the class, that is not inherited from ControllerBase class nor has any attributes.
            //Simply using its name and method name prefix/CustomWithoutBaseClass/GetValue
            app.UseMvc(
                builder =>
                    {
                        //this route (error/x/y) will throw an error, cause we are using default MVCRouteHandler,
                        //and it expects the Controller and action name among RouteValues.
                        builder.MapRoute("errorous", "error/x/y");

                        builder.MapRoute("2", "prefix/{controller=Values}/{action=Index}/{id?}");
                        //this route shows how to hardcode the route
                        builder.MapRoute(
                                             name: "routeThatHasHardcodedActionAndController",
                                             template: "en-US/Products/{id}",
                                             defaults: new { controller = "Values", action = "" },
                                             constraints: new { id = new IntRouteConstraint() },
                                             dataTokens: new { locale = "en-US" });
                    });

            //Also we might want to add middleware with custom route handler and below is a sample
            var trackPackageRouteHandler = new RouteHandler(context =>
                  {
                      var routeValues = context.GetRouteData().Values;
                      return context.Response.WriteAsync(
                          $"Hello! Route values: {string.Join(", ", routeValues)}");
                  });

            var routeBuilder = new RouteBuilder(app, trackPackageRouteHandler);

            routeBuilder.MapRoute(
                "Track Package Route",
                "package/{operation:regex(^track|create|detonate$)}/{id:int}");

            routeBuilder.MapGet("hello/{name}", context =>
                {
                    var name = context.GetRouteValue("name");
                    // The route handler when HTTP GET "hello/<anything>" matches
                    // To match HTTP GET "hello/<anything>/<anything>, 
                    // use routeBuilder.MapGet("hello/{*name}"
                    return context.Response.WriteAsync($"Hi, {name}!");
                });

            var routes = routeBuilder.Build();
            app.UseRouter(routes);
        }
    }
}
