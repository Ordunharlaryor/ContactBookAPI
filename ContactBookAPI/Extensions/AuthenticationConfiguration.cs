using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Text;
using ContactBook.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ContactBook.API.Extensions
{
    public static class AuthenticationConfiguration
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(v =>
                   {
                       v.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                       v.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                       
                   })
                    .AddJwtBearer(v =>
                    {
                        v.SaveToken = true;
                        v.RequireHttpsMetadata = false;
                        v.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,//configuration.GetSection("Jwt:Issuer").Value != null,
                            ValidateLifetime = true,
                            ValidateAudience = true,//configuration.GetSection("Jwt:Audience").Value != null,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = configuration.GetSection("Jwt:Issuer").Value ?? null,
                            ValidAudience = configuration.GetSection("Jwt:Audience").Value ?? null,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Token") ?? string.Empty))
                        };
                    });
        }
    }
}



