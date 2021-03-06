﻿using FreeSecur.API.Core.Cryptography;
using FreeSecur.API.Core.ExceptionHandling;
using FreeSecur.API.Core.ExceptionHandling.ExceptionHandlers;
using FreeSecur.API.Core.ExceptionHandling.Exceptions;
using FreeSecur.API.Core.GeneralHelpers;
using FreeSecur.API.Core.Mailing;
using FreeSecur.API.Core.Reflection;
using FreeSecur.API.Core.Serialization;
using FreeSecur.API.Core.Url;
using FreeSecur.API.Core.Validation.Validator;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Net.Mail;

namespace FreeSecur.API.Core
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
            services.AddSingleton<ISecureRandomGenerator, SecureRandomGenerator>();
            services.AddSingleton<IMailService, MailService>();
            services.AddSingleton<IUrlService, UrlService>();
            services.AddSingleton<IExceptionHandler<StatusCodeException>, StatusCodeExceptionHandler>();
            services.AddSingleton<IExceptionHandler<ErrorCodeException>, ErrorCodeExceptionHandler>();
            services.AddSingleton<ISmtpClient, FsSmtpClient>();

            services.AddSingleton<IObjectModelValidator, NullObjectModelValidator>();


            services.Configure<FsMail>(configuration.GetSection(nameof(FsMail)));
            services.Configure<FsEncryption>(configuration.GetSection(nameof(FsEncryption)));

            var configurer = new CoreConfigurer(services, configuration);

            return configurer;
        }
    }
}
