using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Models.Models;

namespace WebApiDemo.Security
{
    public class SameStudentAuthorizationHandler : AuthorizationHandler<SameStudentRequirement, Student>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameStudentRequirement requirement, Student student)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }


            if (context.User.Identity.Name == student.Email)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
