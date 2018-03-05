using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCBasics.Policies
{
    public class TeacherIsSelfPolicy : AuthorizationHandler<TeacherIsSelfPolicy>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TeacherIsSelfPolicy requirement)
        {
            if (context.User.HasClaim(ClaimTypes.Role, "Admin") || context.User.HasClaim(ClaimTypes.Role, "Registrar") || context.User.HasClaim(ClaimTypes.Role, "Teacher"))
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}
