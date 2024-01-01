﻿using Application.Authentication;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConn")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                                .AddJwtBearer(options =>
                                {
                                    options.TokenValidationParameters = new()
                                    {
                                        ValidateIssuer = true,
                                        ValidateAudience = true,
                                        ValidateLifetime = true,
                                        ValidateIssuerSigningKey = true,
                                        ClockSkew = TimeSpan.Zero,
                                        ValidIssuer = configuration["Jwt:Issuer"],
                                        ValidAudience = configuration["Jwt:Audience"],
                                        IssuerSigningKey = new SymmetricSecurityKey(
                                            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                                    };
                                });

            services.AddAuthorization();

 
            services.AddScoped<IJwtProvider, JwtProvider>();
    

            return services;
        }
    }
}
