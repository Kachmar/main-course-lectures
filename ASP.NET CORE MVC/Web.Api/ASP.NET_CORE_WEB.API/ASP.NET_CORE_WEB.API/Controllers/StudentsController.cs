using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE_WEB.API.Controllers
{
    using Microsoft.CodeAnalysis.Options;
    using Microsoft.Extensions.Options;

    public class StudentsController : ControllerBase
    {
        private readonly IOptions<ConfigurationContainer> options;

        public StudentsController(IOptions<ConfigurationContainer> options)
        {
            this.options = options;
        }
        //Somehow when navigating to /api/students rout, it calls Abracadabra
        public async Task<string> Abracadabra()
        {
            var routeDAta = RouteData;
            return "Tet";
        }
    }
}