using System;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BookService.Services
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ICacheService _cacheService;

        //public JwtMiddleware(RequestDelegate next, ICacheService cacheService)
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
            //_cacheService = cacheService;
        }

        public async Task Invoke(HttpContext context)
        {

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            // Check the token we got if its saved in the db
            //var storedRefreshToken = _cacheService.GetValue<string>(token).Result;
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() is object)
            {
                await _next(context);
                return;
            }
            else if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                context.Response.Headers.Add("Token-Expired", "true");
                var response = JsonConvert.SerializeObject("The access token provided is not valid.");
                await context.Response.WriteAsync(response);
                await Task.CompletedTask;
                return;
            }

            var claimsIdentity = context.User.Identity as ClaimsIdentity;

            if (claimsIdentity != null && claimsIdentity.Name == "Guest")
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                context.Response.Headers.Add("Token-Expired", "true");
                var response = JsonConvert.SerializeObject("The access token provided is not valid.");
                await context.Response.WriteAsync(response);
                await Task.CompletedTask;
                return;
            }


            await _next(context);
        }
    }
}

