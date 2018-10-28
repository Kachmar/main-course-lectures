using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Fundamentals
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            //Show how to change the default wwwRoot

           return WebHost.CreateDefaultBuilder(args)
                .UseWebRoot("AlternativeWebRoot")//WebRoot is were static files live
                .UseContentRoot("AlternativeContentRoot") //Application base path.
                .UseSetting(WebHostDefaults.CaptureStartupErrorsKey, "true").UseStartup<Startup>();
        }
    }
}