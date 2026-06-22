using Discount.Repositories;
using System.Reflection;

namespace Discount.Bootstrappings
{
    public static class ConfigureServices
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            //Add Swagger services
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            builder.Services.AddOpenApi();

            //Register MediatR
            var assemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly(),
                //Assembly.GetAssembly(typeof(GetAllBrandsHandler))
            };
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));


            // register database settings and seeder
            builder.Services.Configure<DiscountDatabaseSettings>(
                builder.Configuration.GetSection("DatabaseSettings"));


            // custom services
            builder.Services.AddScoped<IDiscountRepository, DiscountPostgreRepository>();

            return builder;
        }
    }
}
