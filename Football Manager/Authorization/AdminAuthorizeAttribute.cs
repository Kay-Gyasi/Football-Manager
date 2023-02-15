using Microsoft.AspNetCore.Mvc.Filters;

namespace Football_Manager.Authorization;

public class AdminAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.FindAll("role").All(x => x.Value != "admin"))
        {
            context.Result = new ForbidResult();
        }
    }
}