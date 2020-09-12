using FreeSecur.API.Logic.UserLogic;
using Microsoft.Extensions.DependencyInjection;
using FreeSecur.API.Core;
using FreeSecur.API.Logic.AccessManagement;
using FreeSecur.API.Logic.VaultManagement;

namespace FreeSecur.API.Logic
{
    public static class FsLogic
    {
        public static CoreConfigurer AddFreeSecurLogic(
            this CoreConfigurer fsCoreConfigurer)
        {
            fsCoreConfigurer.Services.AddTransient<AccountManagementService>();
            fsCoreConfigurer.Services.AddTransient<AccessManagementService>();
            fsCoreConfigurer.Services.AddTransient<VaultService>();


            return fsCoreConfigurer;
        }
    }
}
