using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using AuthService.Infrastructure.Interfaces;
using AuthService.Models;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        private readonly JwtSettings jwtSettings;
        public AuthController(JwtSettings _jwtSettings, IJwtAuthenticationManager _jwtAuthenticationManager)
        {
            jwtSettings = _jwtSettings;
            jwtAuthenticationManager = _jwtAuthenticationManager;
        }

        [AllowAnonymous]
        [HttpPost("GenerateToken")]
        [ProducesResponseType(typeof(UserTokens), (int)HttpStatusCode.OK)]
        public UserTokens GenerateToken([FromBody] User validUser)
        {
            try
            {
                return jwtAuthenticationManager.GenerateAccessTokenAsync(validUser, jwtSettings);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

