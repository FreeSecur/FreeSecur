using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSecur.API.Core
{
    public class CoreConfigurer
    {
        internal CoreConfigurer(IServiceCollection services, IConfiguration configuration)
        {
            Services = services;
            Configuration = configuration;
        }

        public IServiceCollection Services { get; }
        public IConfiguration Configuration { get; }
    }
}
