using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PruebaTecnicaMultitenant.Src.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Method)]
    public class RequiresClaimAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _claimName;

        public RequiresClaimAttribute(string claimName)
        {
            _claimName = claimName;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var slugTenant = context.HttpContext.GetRouteValue("slugTenant")?.ToString();

            var claim = context.HttpContext.User.Claims
                .SingleOrDefault(c => c.Type == _claimName);

            if(claim.Value != slugTenant) context.Result = new ForbidResult();
        }

    }
}