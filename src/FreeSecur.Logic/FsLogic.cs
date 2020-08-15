using FreeSecur.Logic.UserLogic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FreeSecur.Core;

namespace FreeSecur.Logic
{
    public static class FsLogic
    {
        public static FsCoreConfigurer AddFreeSecurLogic(
            this FsCoreConfigurer fsCoreConfigurer)
        {
            fsCoreConfigurer.Services.AddTransient<AccountManagementService>();


            return fsCoreConfigurer;
        }
    }
}
