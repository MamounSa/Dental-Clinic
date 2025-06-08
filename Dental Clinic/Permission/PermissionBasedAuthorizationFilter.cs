using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class PermissionBasedAuthorizationFilter : IAuthorizationFilter
{
  

    public PermissionBasedAuthorizationFilter()
    {
        
    }
    List<UserPermission> userPermissions = new List<UserPermission>
    {
        new UserPermission
        {
            PermissionId = 1,
            UserId = 1,

        },
        new UserPermission
        {
            PermissionId = 1,
            UserId = 2,

        },
    };


    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var attribute = (CheckPermissionAttribute)context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is CheckPermissionAttribute);
        if (attribute != null)
        {
            var claimIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
            if (claimIdentity == null || !claimIdentity.IsAuthenticated)
            {
                context.Result = new ForbidResult();
                return;
            }

            var userId = int.Parse(claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            var hasPermission = userPermissions.Any(x => x.UserId == userId && x.PermissionId == (int)attribute.Permission);
          
            if (!hasPermission)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}

