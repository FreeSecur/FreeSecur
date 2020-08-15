﻿using FreeSecur.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSecur.Domain
{
    public static class FsDomain
    {
        public static FsCoreConfigurer AddFreeSecurDomain(
            this FsCoreConfigurer fsCoreConfigurer)
        {
            var settingsSection = fsCoreConfigurer.Configuration.GetSection(nameof(FsDomainSettings));
            var settings = settingsSection.Get<FsDomainSettings>();

            fsCoreConfigurer.Services.AddDbContextPool<FsDbContext>(options => {
                options.UseSqlServer(settings.ConnectionString);
            });

            fsCoreConfigurer.Services.AddTransient<IFsEntityRepository, FsEntityRepository>();

            return fsCoreConfigurer;
        }

        public static void UseFreeSecurDomain(
            this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<FsDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
