using Basket.Bootstrapping;
using Microsoft.AspNetCore.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddServices();

var app = builder.Build();

//Configure the HTTP request pipeline.
{
    if (app.Environment.IsDevelopment())
        //openapi/v1.json
        app.MapOpenApi();
        // scalar/
        //app.MapScalarApiReference();
}

//Enable Swagger UI in development environment
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
