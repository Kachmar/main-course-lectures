using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApiDemo.Security
{
    public class UkrainianRequirement : IAuthorizationRequirement
    {
    }
}
