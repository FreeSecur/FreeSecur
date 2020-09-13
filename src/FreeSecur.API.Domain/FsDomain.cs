using FreeSecur.API.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSecur.API.Domain
{
    public static class FsDomain
    {
        public static CoreConfigurer AddFreeSecurDomain(
            this CoreConfigurer fsCoreConfigurer)
        {
            var settingsSection = fsCoreConfigurer.Configuration.GetSection(nameof(FsDomainSettings));
            var settings = settingsSection.Get<FsDomainSettings>();

            fsCoreConfigurer.Services.AddDbContext<FsDbContext>(options => {
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
