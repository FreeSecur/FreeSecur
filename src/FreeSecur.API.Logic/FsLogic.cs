using FreeSecur.API.Logic.UserLogic;
using Microsoft.Extensions.DependencyInjection;
using FreeSecur.API.Core;
using FreeSecur.API.Logic.AccessManagement;
using FreeSecur.API.Logic.VaultManagement;
using FreeSecur.API.Logic.AccountManagement;

namespace FreeSecur.API.Logic
{
    public static class FsLogic
    {
        public static CoreConfigurer AddFreeSecurLogic(
            this CoreConfigurer fsCoreConfigurer)
        {
            fsCoreConfigurer.Services.AddTransient<AccountManagementService>();
            fsCoreConfigurer.Services.AddTransient<AccessManagementService>();
            fsCoreConfigurer.Services.AddTransient<VaultCreationService>();
            fsCoreConfigurer.Services.AddTransient<VaultInformationService>();
            fsCoreConfigurer.Services.AddTransient<VaultItemService>();
            fsCoreConfigurer.Services.AddTransient<IVerificationService, VerificationService>();

            return fsCoreConfigurer;
        }
    }
}
