using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.ADO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApiDemo.Security;

namespace WebApiDemo
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(p =>
            {
                p.UseSqlServer("Data Source=NB00MCF001\\SQLEXPRESS;Initial Catalog=DemoIdentity;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            });
            services.Configure<RepositoryOptions>(_configuration);

            services.AddControllersWithViews();
            services.AddTransient<SomeService>();
            services.AddScoped<Repository>();
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddSingleton<IAuthorizationHandler, UkrainianAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SameStudentAuthorizationHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("UkrainiansOrAdmin", policy =>
                    policy.Requirements.Add(new UkrainianRequirement()));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("SameUserPolicy", policy =>
                    policy.Requirements.Add(new SameStudentRequirement()));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, func) =>
            {
                var userManager = context.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = context.RequestServices.GetRequiredService<RoleManager<IdentityRole>>();
                await this.CreateAdminUser(userManager, roleManager);
                await func();
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(builder =>
            {
                builder.MapControllerRoute("default", "{controller=students}/{action=students}/{id?}");
            });
        }

        private async Task CreateAdminUser(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            IdentityUser identityUser = new IdentityUser("Admin@test.com") { Email = "Admin@test.com" };
            var userRes = await userManager.CreateAsync(identityUser, "Qwerty1234!");
            if (userRes.Succeeded)
            {
                var rol = await roleManager.CreateAsync(new IdentityRole("Admin"));
                var res = await userManager.AddToRoleAsync(identityUser, "Admin");
            }
        }
    }
}
