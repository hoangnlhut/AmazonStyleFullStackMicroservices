using Discount.Handlers.Commands;
using Discount.Queries;
using Discount.Repositories;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Discount.Bootstrappings
{
    public static class ConfigureServices
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            // register database settings and seeder
            builder.Services.Configure<DiscountDatabaseSettings>(
                builder.Configuration.GetSection("DatabaseSettings"));

            // Register as a singleton for direct injection (as in your code)
            builder.Services.AddSingleton(resolver =>
                resolver.GetRequiredService<IOptions<DiscountDatabaseSettings>>().Value);

            //Register MediatR
            var assemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly(),
                typeof(CreateDiscountHandler).Assembly
            };
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));


            // custom services
            builder.Services.AddScoped<IDiscountRepository, DiscountPostgreRepository>();

            //add Grpc services
            builder.Services.AddGrpc();

            return builder;
        }
    }
}
