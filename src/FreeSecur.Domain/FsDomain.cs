using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSecur.Domain
{
    public static class FsDomain
    {
        public static void AddFreeSecurDomain(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            var settingsSection = configuration.GetSection(nameof(FsDomainSettings));
            var settings = settingsSection.Get<FsDomainSettings>();

            services.AddDbContextPool<FsDbContext>(options => {
                options.UseSqlServer(settings.ConnectionString);
            });

            services.AddTransient<IFsEntityRepository, FsEntityRepository>();
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
