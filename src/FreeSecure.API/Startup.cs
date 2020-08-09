using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FreeSecur.Core;
using FreeSecur.Domain;
using FreeSecure.API.ErrorHandling;
using FreeSecur.Logic;
using FreeSecur.Core.Serialization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using FreeSecure.API.Utils;

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
            services.AddFreeSecurCore();
            services.AddFreeSecurDomain(Configuration);
            services.AddFreeSecurLogic(Configuration);

            services.AddControllers(options =>
            {
                options.Filters.Add(new FsExceptionFilter());
                options.Filters.Add(new FsValidationFilter());
            })
            .AddJsonOptions(options =>
            {
                FsSerialization.ConfigureSerailizerOptions(options.JsonSerializerOptions);
            });

            services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ProduceResponseTypeProvider>());

            services.AddSwaggerGen();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
