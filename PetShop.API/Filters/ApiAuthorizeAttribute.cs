using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using PetShop.Services.Abstractions.Jwt;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace PetShop.API.Filters
{
    public class ApiAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string[] Roles { get; set; }

        public ApiAuthorizeAttribute(params string[] roles)
        {
            Roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authHeaders);
            string token = authHeaders.FirstOrDefault(h => h.StartsWith("Bearer "))?.Replace("Bearer ", string.Empty);
            if (token == null)
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                IJwtService tokenService = (IJwtService)context.HttpContext.RequestServices.GetService(typeof(IJwtService));
                ClaimsPrincipal claims = tokenService.DecodeJwt(token);
                if (claims == null)
                {
                    context.Result = new UnauthorizedResult();
                }
                else
                {
                    foreach (string r in Roles)
                    {
                        if (claims.IsInRole(r))
                        {
                            context.HttpContext.User = claims;
                            return;
                        }
                    } 
                    context.Result = new UnauthorizedResult();
                }
            }
        }
    }
}
