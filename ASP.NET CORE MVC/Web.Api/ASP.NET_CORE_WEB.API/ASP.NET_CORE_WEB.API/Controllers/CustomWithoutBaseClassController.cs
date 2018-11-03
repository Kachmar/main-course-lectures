using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE_WEB.API.Controllers
{
    using Microsoft.AspNetCore.Routing;
    [Route("Custom")]
    public class CustomWithoutBaseClassController
    {
        //Somehow when navigating to /prefix/CustomWithoutBaseClass/Abracadabra route, it calls Abracadabra
        public string Abracadabra()
        {
            return "Value from Abracadabra";
        }


        //Having the class without any attributes, it is enough to have route attr on method
        // to serve web request
        [Route("")]
        [Route("index")]
        [Route("MyAction")]
        public string WithRoute()
        {

            return "With route";
        }

        // This is an example of the need ActionResult, because we want to return either string result
        // or http status code
        // multiple return value
        // api/WithActionResult?returnError=true
        [Route("[action]")]
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