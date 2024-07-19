using System;
using AuthService.Models;

namespace AuthService.Infrastructure.Interfaces
{
	public interface IJwtAuthenticationManager
	{
        UserTokens GenerateAccessTokenAsync(User model, JwtSettings jwtSettings);
    }
}

