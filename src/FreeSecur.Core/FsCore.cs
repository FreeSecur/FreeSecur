using FreeSecur.Core.Cryptography;
using FreeSecur.Core.Mailing;
using FreeSecur.Core.Reflection;
using FreeSecur.Core.Serialization;
using FreeSecur.Core.Url;
using FreeSecur.Core.Validation.Validator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FreeSecur.Core
{
    public static class FsCore
    {
        public static FsCoreConfigurer AddFreeSecurCore(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<FsOptions> optionsDelegate = null)
        {
            var defaultJsonSerializerOptions = FsSerialization.GetDefaultJsonSerializerOptions();
            var fsOptions = new FsOptions(defaultJsonSerializerOptions);
            optionsDelegate?.Invoke(fsOptions);

            services.AddSingleton<ReflectionService>();
            services.AddSingleton<StringInterpolationService>();
            services.AddSingleton<FsModelValidator>();
            services.AddSingleton<IFsSerializer>(
                (serviceProvider) => new FsJsonSerializer(fsOptions.JsonSerializerOptions));

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IEncryptionService, AesEncryptionService>();
            services.AddSingleton<IHashService, BCryptHashService>();
            services.AddSingleton<IMailService, MailService>();
            services.AddSingleton<IUrlService, UrlService>();

            services.Configure<FsMail>(configuration.GetSection(nameof(FsMail)));
            services.Configure<FsEncryption>(configuration.GetSection(nameof(FsEncryption)));

            var configurer = new FsCoreConfigurer(services, configuration);

            return configurer;
        }
    }
}
