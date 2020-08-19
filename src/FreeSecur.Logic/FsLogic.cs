using FreeSecur.Logic.UserLogic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FreeSecur.Core;
using FreeSecur.Logic.AccessManagement;

namespace FreeSecur.Logic
{
    public static class FsLogic
    {
        public static CoreConfigurer AddFreeSecurLogic(
            this CoreConfigurer fsCoreConfigurer)
        {
            fsCoreConfigurer.Services.AddTransient<AccountManagementService>();
            fsCoreConfigurer.Services.AddTransient<AccessManagementService>();


            return fsCoreConfigurer;
        }
    }
}
