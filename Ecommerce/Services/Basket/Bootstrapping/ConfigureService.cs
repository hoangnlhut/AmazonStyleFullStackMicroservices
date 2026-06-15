using Basket.Repositories;
using System.Reflection;

namespace Basket.Bootstrapping
{
    public static class ConfigureService
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            //Add Swagger services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddOpenApi();

            //Register MediatR
            var assemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly()
            };
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));


            //Configure connection to your Redis instance
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379";
                options.InstanceName = "Basket_";
            });

            // register database settings and seeder
            //builder.Services.Configure<CatalogDatabaseSettings>(
            //    builder.Configuration.GetSection("DatabaseSettings"));

            //builder.Services.AddSingleton<DatabaseSeeder>();

            // custom services
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();

            return builder;
        }
    }
}
