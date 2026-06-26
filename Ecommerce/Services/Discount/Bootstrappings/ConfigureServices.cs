using Discount.Queries;
using Discount.Repositories;
using System.Reflection;

namespace Discount.Bootstrappings
{
    public static class ConfigureServices
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            //Register MediatR
            var assemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly(),
                typeof(GetDiscountByProductNameQuery).Assembly
            };
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));


            // register database settings and seeder
            builder.Services.Configure<DiscountDatabaseSettings>(
                builder.Configuration.GetSection("DatabaseSettings"));


            // custom services
            builder.Services.AddScoped<IDiscountRepository, DiscountPostgreRepository>();

            //add Grpc services
            builder.Services.AddGrpc();

            return builder;
        }
    }
}
