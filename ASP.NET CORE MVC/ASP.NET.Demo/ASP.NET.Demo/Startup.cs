﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NET.Demo.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataAccess.ADO;
using Services;
using DataAccess.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET.Demo
{

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
            services.AddMvc();
            services.AddDbContext<ApplicationDbContext>( options => options.UseInMemoryDatabase("demo.site"));

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.Configure<RepositoryOptions>(Configuration);
            services.AddScoped<StudentService>();
            services.AddScoped<CourseService>();
            services.AddScoped<HomeTaskService>();
            services.AddScoped<LecturerService>();
            services.AddDbContext<UniversityContext>();
            services.AddScoped(typeof(UniversityRepository<>));
            services.ConfigureApplicationCookie(p =>
                {
                    p.LoginPath = "/Security/Login";
                    p.Cookie.Name = "ASP.NET.Demo.App";
                });
            services.AddAuthorization(options => {
                options.AddPolicy("UkrainiansOnly", builder =>
                {
                    builder.AddRequirements(new UkrainianRequirement());
                });
                options.AddPolicy("SameUserPolicy", builder =>
                {
                    builder.AddRequirements(new SameStudentRequirement());
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDatabaseErrorPage();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            this.CreateAdminUser(userManager, roleManager);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(routes =>
            {
                routes.MapControllerRoute(
                     "default",
                    "{controller=Course}/{action=Courses}/{id?}");
            });
        }

        private async Task CreateAdminUser(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            IdentityUser identityUser = new IdentityUser("Admin@test.com") { Email = "Admin@test.com" };
            var userRes = await userManager.CreateAsync(identityUser, "Qwerty1234!");
            var rol = await roleManager.CreateAsync(new IdentityRole("Admin"));
            var res = await userManager.AddToRoleAsync(identityUser, "Admin");
        }
    }
}
