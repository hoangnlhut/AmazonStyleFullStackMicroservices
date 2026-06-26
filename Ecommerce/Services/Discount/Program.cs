using Discount.Bootstrappings;
using Discount.Extensions;
using Discount.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServices();

var app = builder.Build();

//Migrate database
app.MigrateDatabase<Program>();
app.UseRouting();
app.UseEndpoints(endpoints => 
{
    endpoints.MapGrpcService<DiscountService>();
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
    });
});

app.Run();
