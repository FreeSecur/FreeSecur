using FreeSecur.Logic.UserLogic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSecur.Logic
{
    public static class FsLogic
    {
        public static void AddFreeSecurLogic(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<AccountManagementService>();
        }
    }
}
