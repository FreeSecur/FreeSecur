using FreeSecur.Core.Cryptography;
using FreeSecur.Core.ExceptionHandling;
using FreeSecur.Core.ExceptionHandling.ExceptionHandlers;
using FreeSecur.Core.ExceptionHandling.Exceptions;
using FreeSecur.Core.GeneralHelpers;
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
    public static class Core
    {
        public static CoreConfigurer AddFreeSecurCore(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<Options> optionsDelegate = null)
        {
            var defaultJsonSerializerOptions = Serialization.Serialization.GetDefaultJsonSerializerOptions();
            var fsOptions = new Options(defaultJsonSerializerOptions);
            optionsDelegate?.Invoke(fsOptions);

            services.AddSingleton<ReflectionService>();
            services.AddSingleton<StringInterpolationService>();
            services.AddSingleton<ModelValidator>();
            services.AddSingleton<ISerializer>(
                (serviceProvider) => new JsonSerializer(fsOptions.JsonSerializerOptions));

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IEncryptionService, AesEncryptionService>();
            services.AddSingleton<IHashService, BCryptHashService>();
            services.AddSingleton<IMailService, MailService>();
            services.AddSingleton<IUrlService, UrlService>();
            services.AddSingleton<IExceptionHandler<StatusCodeException>, StatusCodeExceptionHandler>();
            services.AddSingleton<IExceptionHandler<ErrorCodeException>, ErrorCodeExceptionHandler>();

            services.Configure<Mail>(configuration.GetSection(nameof(Mail)));
            services.Configure<FsEncryption>(configuration.GetSection(nameof(FsEncryption)));

            var configurer = new CoreConfigurer(services, configuration);

            return configurer;
        }
    }
}
