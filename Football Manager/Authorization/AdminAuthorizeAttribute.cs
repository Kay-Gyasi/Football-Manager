using Microsoft.AspNetCore.Mvc.Filters;

namespace Football_Manager.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AdminAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        if (context.HttpContext.User.FindAll("role").All(x => x.Value != "admin"))
        {
            context.Result = new ForbidResult();
        }
    }
}