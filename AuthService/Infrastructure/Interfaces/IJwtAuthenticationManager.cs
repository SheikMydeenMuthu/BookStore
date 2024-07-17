using System;
using AuthService.Model;

namespace AuthService.Infrastructure.Interfaces
{
	public interface IJwtAuthenticationManager
	{
        UserTokens GenerateAccessTokenAsync(User model, JwtSettings jwtSettings);
    }
}

