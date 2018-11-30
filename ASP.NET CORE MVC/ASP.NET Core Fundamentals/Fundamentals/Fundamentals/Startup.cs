using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamentals
{
    using Fundamentals.DependancyInjection;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Add(new ServiceDescriptor(typeof(IScopedService), typeof(ScopedService), ServiceLifetime.Scoped));
            //alternatives
            //services.AddScoped<IScopedService, ScopedService>();
            // services.AddScoped(typeof(IScopedService), typeof(ScopedService));

            services.AddTransient<ITransientService, TransientService>();

            services.AddSingleton<ISingletonService, SingletonService>();
            //alternative
            //services.AddSingleton<ISingletonService>(new SingletonService());

            //make dependancy container aware of service
            //lets not add service, so that app crashes.
            services.AddTransient<ScopeOrchestration>();

            //
            services.AddTransient<IClassWithInterface, ClassWithInterface>();
            services.AddTransient<ClassDependingOnInterface>();
            services.AddTransient<ClassWithDependancies>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger("Demo");


            //  ShowScopesDemo(app);
            app.UseStaticFiles();

            app.MapWhen(
                context => { return context.Request.Headers.ContainsKey("Agent"); },
                builder =>
                    {
                        builder.Run(
                            context =>
                                {
                                    Console.WriteLine("MapWhen works");
                                    return Task.CompletedTask;
                                });
                    });
            app.Map(
                "/SpecialRoute",
                appBuilder =>
                    {
                        appBuilder.Run(context => context.Response.WriteAsync("Special Route reached."));
                    });

            //This adds middleware
            app.UseMiddleware<MyRequestHandler>();
            app.Use(async (context, next) => { next(); });
            app.Use(
                (Httpcontext, func) =>
                {
                    foreach (var keyValuePair in Httpcontext.Request.Headers)
                    {
                        logger.Log(LogLevel.Information, keyValuePair.Value);
                    }

                    Httpcontext.Response.WriteAsync("This is response from middleware 1");
                    return func();
                });
            app.Use(
                (Httpcontext, func) =>
                    {
                        return Httpcontext.Response.WriteAsync("This is response from middleware 2");
                        // return func();
                    });
            //This is the last item in a chain, and Run means no further items in the pipeline
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
            ShowDIDemo(app.ApplicationServices);
        }

        private void ShowScopesDemo(IApplicationBuilder app)
        {
            app.Run(async (context) =>
                {

                    ScopeOrchestration scopeOrchestration = context.RequestServices.GetService<ScopeOrchestration>();

                    await context.Response.WriteAsync(scopeOrchestration.GetScopesStringTask());
                });
        }

        private void ShowDIDemo(IServiceProvider appApplicationServices)
        {
            ClassWithDependancies viaConstructors = this.GetClassWithDependancies();
            //or
            ClassWithDependancies fromDi = appApplicationServices.GetService<ClassWithDependancies>();
        }

        private ClassWithDependancies GetClassWithDependancies()
        {
            ClassWithInterface classWithInterface = new ClassWithInterface();
            ClassDependingOnInterface classDependingOnInterface = new ClassDependingOnInterface(classWithInterface);
            ClassWithDependancies classWithDependancies = new ClassWithDependancies(classDependingOnInterface);

            return classWithDependancies;
        }
    }
}
