using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE_WEB.API.Controllers
{
    using Microsoft.AspNetCore.Routing;

    // [Route("api/[controller]")]
    //[ApiController]
    public class CustomWithoutBaseClassController
    {
        //Somehow when navigating to /api/CustomWithoutBaseClass rout, it calls Abracadabra
        public string GetValue()
        {
            return "Value from custom";
        }
        //Adding the thing, breaks the behaviour
        //public string GetValue2()
        //{
        //    return "Value from custom2";
        //}


        //Having the class without any attributes, it is enough to have route attr on method
        // to serve web request
        [Route("api/Custom")]
        public string WithRoute()
        {
            
            return "With route";
        }

        // multiple return value
        // api/WithActionResult?returnError=true
        [Route("api/WithActionResult")]
        public ActionResult<string> WithActionResult([FromQuery] bool returnError)
        {
            if (returnError)
            {
                return new BadRequestResult();
            }
            
            return "With route";
        }

    }
}