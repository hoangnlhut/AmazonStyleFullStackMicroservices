
using Ordering.Bootstrappings;
using Ordering.Data;
using Ordering.Extensions;

namespace Ordering
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //application services
            builder.Services.AddApplicationServices();
            //infra services
            builder.Services.AddInfraServices(builder.Configuration);

            var app = builder.Build();

            //migration
            app.MigrateDatabase<OrderContext>((context, services) =>
            {
                 var logger = services.GetService<ILogger<OrderContextSeed>>();
                 OrderContextSeed.SeedAsync(context, logger).Wait();
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            //Enable Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
