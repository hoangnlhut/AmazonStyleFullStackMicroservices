using Basket.Repositories;
using System.Reflection;

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
                Assembly.GetExecutingAssembly()
            };
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));


            //Configure connection to your Redis instance
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
                options.InstanceName = "Basket_";
            });

            // custom services
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();

            return builder;
        }
    }
}
