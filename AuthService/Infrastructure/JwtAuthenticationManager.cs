using System;
using AuthService.Infrastructure.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Models;

namespace AuthService.Infrastructure
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {

        public UserTokens GenerateAccessTokenAsync(User model, JwtSettings jwtSettings)
        {
            if (model == null)
                throw new ArgumentException(null, nameof(model));

            var userToken = new UserTokens();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id",model.UserId.ToString()),
                    new Claim(ClaimTypes.Name, model.Name),
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = jwtSettings.ValidAudience,
                Issuer = jwtSettings.ValidIssuer,
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            userToken.Token = tokenHandler.WriteToken(token);
            userToken.UserName = model.Name;
            userToken.Id = model.UserId;
            userToken.GuidId = Guid.NewGuid();
            userToken.EmailId = model.Email;

            return userToken;
        }
    }
}

