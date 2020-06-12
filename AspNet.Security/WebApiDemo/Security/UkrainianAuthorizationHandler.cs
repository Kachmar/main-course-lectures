using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApiDemo.Security
{
    public class UkrainianAuthorizationHandler : AuthorizationHandler<UkrainianRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UkrainianRequirement requirement)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }

            var claims = context.User.Claims;
            if (context.User.HasClaim("Country", "Ukraine"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
