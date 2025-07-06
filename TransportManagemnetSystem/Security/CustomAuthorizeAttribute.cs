using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace TransportManagemnetSystem.Security
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly ILogger<CustomAuthorizeAttribute> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string userId = null;
        public new AuthorizationPolicy Policy { get; }

        public CustomAuthorizeAttribute(ILogger<CustomAuthorizeAttribute> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        }


        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;
            bool isAuthorized = httpContext.User.Identity.IsAuthenticated;

            var area = context.RouteData.Values["area"];
            var controller = context.RouteData.Values["controller"] ?? "";
            var action = context.RouteData.Values["action"];
            var page = context.RouteData.Values["page"];

            _logger.LogInformation("User Log Date and Time : " + DateTime.UtcNow + " User Id : " + userId + " Url : " + _httpContextAccessor.HttpContext.Request.Host + _httpContextAccessor.HttpContext.Request.Path);


            ///YYYY-MM-DD
            //if (!fullPath.Contains("localhost"))
            //{
            //    if (Convert.ToDateTime("2026-04-01") < DateTime.UtcNow || !fullPath.Contains("krishi-demoapi.meropalika.com"))
            //    {
            //        //return Ok(new ApiResponse() { Status = false, Message = "Please Contact with Administrator!!! " });
            //    }
            //}

            IPolicyEvaluator policyEvaluator = context.HttpContext.RequestServices.GetRequiredService<IPolicyEvaluator>();
            _ = await policyEvaluator.AuthenticateAsync(Policy, context.HttpContext);
            if (HasAllowAnonymous(context))
                return;
            var result = new UnauthorizedResult(); //new ViewResult { ViewName = "~/Views/Shared/404Page.cshtml" };
            if (isAuthorized)
            {
                //var isRolePresent = httpContext.User.FindFirstValue("IsRolePresent");
                //if (isRolePresent != null && !Convert.ToBoolean(isRolePresent))
                //{
                //    context.Result = result;
                //    return;
                //}

                if (area != null)
                {
                    if (httpContext.User.IsInRole(UserRoles.User) && !area.Equals("Identity"))
                    {
                        context.Result = result;
                    }
                    //if (httpContext.User.IsInRole(UserRoles.User))
                    //{

                    //    if (action != null && action.Equals("UserList"))
                    //    {
                    //        context.Result = result;
                    //    }
                    //}
                }
                if (httpContext.User.IsInRole(UserRoles.Administrator) || httpContext.User.IsInRole(UserRoles.SuperAdmin))
                {
                    return;
                }
            }
            else
            {
                context.Result = new RedirectResult("/Identity/Account/Login");
                // context.Result = new RedirectResult("/LandingPage/Index");
            }

            return;

        }
        private static bool HasAllowAnonymous(AuthorizationFilterContext context)
        {
            if (context.Filters.Any(x => x is IAllowAnonymousFilter))
                return true;

            // When doing endpoint routing, MVC does not add AllowAnonymousFilters for AllowAnonymousAttributes that
            // were discovered on controllers and actions. To maintain compat with 2.x,
            // we'll check for the presence of IAllowAnonymous in endpoint metadata.
            var endpoint = context.HttpContext.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                return true;
            }

            return false;
        }

    }
}
