using Basket.Commands;
using Basket.Handlers.Command;
using Basket.Repositories;
using System.Reflection;
using Microsoft.AspNetCore.OpenApi;

namespace Basket.Bootstrapping
{
    public static class ConfigureService
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            //Add Swagger services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Register MediatR
            var assemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly(),
                typeof(CreateBasketHandler).Assembly
            };
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));


            var configCache = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");

            //Configure connection to your Redis instance
            builder.Services.AddStackExchangeRedisCache(
                options =>
            {
                options.Configuration = configCache;
                //options.InstanceName = "Basket_";
            });

            // custom services
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();

            return builder;
        }
    }
}
