using FreeSecur.Logic.UserLogic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Logic
{
    public static class FsLogic
    {
        public static void AddFreeSecurLogic(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<UserService>();
        }
    }
}
