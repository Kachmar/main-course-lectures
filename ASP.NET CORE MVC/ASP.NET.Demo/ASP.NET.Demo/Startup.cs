using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataAccess.ADO;
using Services;
using DataAccess.EF;

namespace ASP.NET.Demo
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

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
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase());
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            this.CreateAdminUser(userManager, roleManager);
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Course}/{action=Courses}/{id?}");
            });
        }

        private void CreateAdminUser(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            IdentityUser identityUser = new IdentityUser("Admin@test.com") { Email = "Admin@test.com" };
            var userRes = userManager.CreateAsync(identityUser, "Qwerty1234!");
            var rol = roleManager.CreateAsync(new IdentityRole("Admin"));
            var res = userManager.AddToRoleAsync(identityUser, "Admin");
        }
    }
}
