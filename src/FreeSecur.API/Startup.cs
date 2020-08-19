using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FreeSecur.API.Core;
using FreeSecur.API.Domain;
using FreeSecur.API.Logic;
using FreeSecur.API.Core.Serialization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using FreeSecur.API.Utils;
using FreeSecur.API.Core.Url;
using FreeSecur.API.Core.ExceptionHandling;
using FreeSecur.API.Core.Validation.Filter;
using FreeSecur.API.Logic.AccessManagement;

namespace FreeSecur.API
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
