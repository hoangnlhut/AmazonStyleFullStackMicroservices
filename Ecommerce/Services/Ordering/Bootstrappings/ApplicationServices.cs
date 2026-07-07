using FluentValidation;
using MediatR;
using Ordering.Behaviors;
using Ordering.Repositories;
using System.Reflection;

namespace Ordering.Bootstrappings
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandleExceptionBehavior<,>));
            return services;
        }
    }
}
