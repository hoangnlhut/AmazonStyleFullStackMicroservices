using Catalog.Data;
using Catalog.Handlers.Queries;
using Catalog.Models;
using Catalog.Repositories;
using System.Reflection;

namespace Catalog.Bootstrapping
{
    public static class  ConfigureServices
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
                Assembly.GetExecutingAssembly(),
                Assembly.GetAssembly(typeof(GetAllBrandsHandler))
            };
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));


            // register database settings and seeder
            builder.Services.Configure<CatalogDatabaseSettings>(
                builder.Configuration.GetSection("DatabaseSettings"));

            builder.Services.AddSingleton<DatabaseSeeder>();

            // custom services
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<ITypeRepository, TypeRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            return builder;
        }
}
}
