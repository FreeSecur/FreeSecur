using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSecur.Core
{
    public class FsCoreConfigurer
    {
        internal FsCoreConfigurer(IServiceCollection services, IConfiguration configuration)
        {
            Services = services;
            Configuration = configuration;
        }

        public IServiceCollection Services { get; }
        public IConfiguration Configuration { get; }
    }
}
