using HR.LeaveManagement.Application.Constants;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IjwtService _jwtService;
        private readonly IUserService _userService;


        public AuthService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IjwtService jwtService,
            IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _userService = userService;

        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                {
                    throw new Exception($"User with {request.Email} not found.");
                }

                var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

                if (!result.Succeeded)
                {
                    throw new Exception($"Credentials for '{request.Email} aren't valid'.");
                }

                var token = _jwtService.GenerateToken(await GetClaimsAsync(user));


                if (token == null)
                {
                    throw new Exception("Invalid Attempt!");
                }

                // saving refresh token to the db
                UserRefreshToken obj = new UserRefreshToken
                {
                    RefreshToken = token.Refresh_Token,
                    UserId = user.Id
                };

                await _userService.AddUserRefreshTokens(obj);
                await _userService.SaveCommit();

                AuthResponse response = new AuthResponse
                {
                    Id = user.Id,
                    Token = _jwtService.GenerateToken(await GetClaimsAsync(user)),
                    Email = user.Email,
                    UserName = user.UserName
                };

                return response;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
            {
                throw new Exception($"Username '{request.UserName}' already exists.");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true
            };

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Employee");
                    return new RegistrationResponse() { UserId = user.Id };
                }
                else
                {
                    throw new Exception($"{result.Errors}");
                }
            }
            else
            {
                throw new Exception($"Email {request.Email } already exists.");
            }
        }

        private async Task<IEnumerable<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            return claims;
        }

        public async Task<Tokens> Refresh(Tokens token)
        {
            var principal = _jwtService.GetPrincipalFromExpiredToken(token.Access_Token);
            var userId = principal.Identities.First().Claims.FirstOrDefault(x =>
            x.Type == CustomClaimTypes.Uid)?.Value;

            //retrieve the saved refresh token from database
            var savedRefreshToken = await _userService.GetSavedRefreshTokens(userId, token.Refresh_Token);

            if (savedRefreshToken.RefreshToken != token.Refresh_Token)
            {
                return null;
            }

            var user = await _userManager.FindByIdAsync(userId);

            var newJwtToken = _jwtService.GenerateRefreshToken(await GetClaimsAsync(user));

            if (newJwtToken == null)
            {
                return null;
            }

            // saving refresh token to the db
            UserRefreshToken obj = new UserRefreshToken
            {
                RefreshToken = newJwtToken.Refresh_Token,
                UserId = userId
            };

           await _userService.DeleteUserRefreshTokens(userId, token.Refresh_Token);
           await _userService.AddUserRefreshTokens(obj);
            _userService.SaveCommit();

            return newJwtToken;
        }


    }
}
