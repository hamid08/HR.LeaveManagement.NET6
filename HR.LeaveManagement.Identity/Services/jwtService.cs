using HR.LeaveManagement.Application.Constants;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity.Services
{
    public class jwtService : IjwtService
    {

        private readonly JwtSettings _jwtSettings;

        public jwtService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;

        }

        public Tokens GenerateToken(IEnumerable<Claim> claims)
        {
            return GenerateJWTTokens(claims);
        }

        public Tokens GenerateRefreshToken(IEnumerable<Claim> claims)
        {
            return GenerateJWTTokens(claims);
        }

        public Tokens GenerateJWTTokens(IEnumerable<Claim> claims)
        {
            try
            {
                var secretKey = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
                var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

                var encrytionKey = Encoding.UTF8.GetBytes(_jwtSettings.EncrypKey);
                var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encrytionKey),
                    SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Issuer = _jwtSettings.Issuer,
                    Audience = _jwtSettings.Audience,
                    IssuedAt = DateTime.Now,
                    NotBefore = DateTime.Now.AddMinutes(_jwtSettings.NotBeforeMinutes),
                    Expires = DateTime.Now.AddMinutes(_jwtSettings.ExpirationMinutes),
                    SigningCredentials = signingCredentials,
                    Subject = new ClaimsIdentity(claims),
                    EncryptingCredentials = encryptingCredentials,
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
              
                var token = tokenHandler.WriteToken(securityToken);

                var refreshToken = GenerateRefreshToken();

                return new Tokens { Access_Token = token, Refresh_Token = refreshToken };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var Key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }


            return principal;
        }


        
    }
}
