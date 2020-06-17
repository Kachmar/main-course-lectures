using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Models.Models;

namespace ASP.NET.Demo.Authorization
{

    public class SameStudentRequirementAuthorizationHandler : AuthorizationHandler<SameStudentRequirement, Student>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            SameStudentRequirement requirement,
            Student resource)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }

            if (context.User.Identity.Name == resource.Email)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
