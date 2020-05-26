using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAuthorize.PermissionAccess
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var permissionsClaim =
                context.User.Claims.SingleOrDefault(c => c.Type == PermissionConstants.PackedPermissionClaimType);
            //var permissions =
            //   context.User.Claims.Where(c => c.Type == PermissionConstants.PackedPermissionClaimType).Select(x => x.Value).ToList();
            // If user does not have the scope claim, get out of here
            if (permissionsClaim == null)
                return Task.CompletedTask;

            if (permissionsClaim.Value.ThisPermissionIsAllowed(requirement.PermissionName))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
