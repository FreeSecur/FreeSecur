using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FreeSecur.Core;
using FreeSecur.Domain;
using FreeSecur.Logic;
using FreeSecur.Core.Serialization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using FreeSecure.API.Utils;
using FreeSecur.Core.Url;
using FreeSecur.Core.ExceptionHandling;
using FreeSecure.Core.Validation.Filter;
using FreeSecur.Logic.AccessManagement;

namespace FreeSecure
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddFreeSecurCore(Configuration)
                .AddFreeSecurDomain()
                .AddFreeSecurLogic()
                .AddFreeSecurAuthentication();

            services.AddControllers(options =>
            {
                options.Filters.Add(new ExceptionFilter());
                options.Filters.Add(new ValidationFilter());
            })
            .AddJsonOptions(options =>
            {
                Serialization.ConfigureSerializerOptions(options.JsonSerializerOptions);
            });

            services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ProduceResponseTypeProvider>());

            services.AddSwaggerGen();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<UrlMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FreeSecur API v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseFreeSecurDomain();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
