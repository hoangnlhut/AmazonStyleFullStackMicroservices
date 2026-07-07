using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Behaviors;
using Ordering.Data;
using Ordering.Repositories;
using System.Reflection;

namespace Ordering.Bootstrappings
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddControllers();
            //swagger
            services.AddEndpointsApiExplorer();
            services.AddOpenApi();
            services.AddSwaggerGen();
            
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandleExceptionBehavior<,>));
            return services;
        }

        public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(options => options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString"), sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}
