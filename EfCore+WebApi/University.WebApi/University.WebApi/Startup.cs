using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models;
using Models.Models;
using University.DAL;

namespace University.WebApi
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
            services.AddDbContext<UniversityContext>(p => p.UseSqlServer(Configuration.GetConnectionString("UniversityConnectionString"))
                .UseLazyLoadingProxies());

            services.AddTransient<ICourseRepository, CourseRepository>();


            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<UniversityContext>();

                // Ensure the database is created.
                db.Database.EnsureCreated();

                var homeTask1 = new HomeTask { Title = "W1" };
                var homeTask2 = new HomeTask { Title = "W2" };
                var homeTask3 = new HomeTask { Title = "W3" };
                var homeTask4 = new HomeTask { Title = "W4" };
                var course1 = new Course { Name = "Math", HomeTasks = new List<HomeTask> { homeTask1, homeTask2 } };
                var course2 = new Course { Name = "Physics", HomeTasks = new List<HomeTask> { homeTask3, homeTask4 } };
              
                db.Courses.Add(course1);
                db.Courses.Add(course2);
                db.SaveChanges();
            }

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
