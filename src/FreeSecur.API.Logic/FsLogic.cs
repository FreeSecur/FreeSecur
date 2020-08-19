using FreeSecur.API.Logic.UserLogic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FreeSecur.API.Core;
using FreeSecur.API.Logic.AccessManagement;

namespace FreeSecur.API.Logic
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
