using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Api;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using HR.LeaveManagement.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;

namespace HR.LeaveManagement.Identity
{
    public static class IdentityServicesRegistration
    {
        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddDbContext<LeaveManagementIdentityDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("LeaveManagementIdentityConnectionString"),
                b => b.MigrationsAssembly(typeof(LeaveManagementIdentityDbContext).Assembly.FullName)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<LeaveManagementIdentityDbContext>().AddDefaultTokenProviders();

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IjwtService, jwtService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;

                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        RequireSignedTokens = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])),
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateAudience = true, // on production make it true
                        ValidAudience = configuration["JwtSettings:Audience"],
                        ValidateIssuer = true, // on production make it true
                        ValidIssuer = configuration["JwtSettings:Issuer"],

                        TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:EncrypKey"])),

                    };


                    o.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context => {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                            }
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            if (context.AuthenticateFailure != null)
                                throw new AppException(ApiResultStatusCode.UnAuthorized, "Authenticate failure.", HttpStatusCode.Unauthorized, context.AuthenticateFailure, null);
                            throw new AppException(ApiResultStatusCode.UnAuthorized, "You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);
                        }
                    };

                })

                .AddJwtBearer("Refresh", options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])),
                        TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:EncrypKey"])),

                    };
                });

            return services;
        }
    }
}
