using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WeatherAPI.Filters
{
    public class AuthenticationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var result = context.HttpContext.Request.Headers.ContainsKey("Authorization");

            var token = string.Empty;
            if (result) token = context.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
        }
    }
}