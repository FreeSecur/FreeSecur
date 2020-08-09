using FreeSecur.Core.Cryptography;
using FreeSecur.Core.Reflection;
using FreeSecur.Core.Serialization;
using FreeSecur.Core.Validation.Validator;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Core
{
    public static class FsCore
    {
        public static void AddFreeSecurCore(
            this IServiceCollection services,
            Action<FsOptions> optionsDelegate = null)
        {
            var defaultJsonSerializerOptions = FsSerialization.GetDefaultJsonSerializerOptions();
            var fsOptions = new FsOptions(defaultJsonSerializerOptions);
            optionsDelegate?.Invoke(fsOptions);

            services.AddSingleton<MetadataReflectionService>();
            services.AddSingleton<FsModelValidator>();
            services.AddSingleton<IFsSerializer>(
                (serviceProvider) => new FsJsonSerializer(fsOptions.JsonSerializerOptions));

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IEncryptionModule, AesEncryptionModule>();
            services.AddSingleton<IHashModule, BCryptHashModule>();
        }
    }
}
