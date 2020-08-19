using FreeSecur.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Logic.AccessManagement
{
    public static class AccessMangementExtensions
    {
        public static CoreConfigurer AddFreeSecurAuthentication(this CoreConfigurer fsCoreConfigurer)
        {
            var configuration = fsCoreConfigurer.Configuration;
            var services = fsCoreConfigurer.Services;

            var jwtSettingsSection = configuration
                .GetSection(nameof(FsJwtAuthentication));

            var jwtAuthenticationSettings = jwtSettingsSection
                .Get<FsJwtAuthentication>();

            services.Configure<FsJwtAuthentication>(jwtSettingsSection);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtAuthenticationSettings.Issuer,
                        ValidAudience = jwtAuthenticationSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuthenticationSettings.SecretKey)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return fsCoreConfigurer;
        }
    }
}
