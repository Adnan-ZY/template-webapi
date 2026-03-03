using Template.Core.Interfaces;
using Template.Infrastructure.Data;
using Template.Infrastructure.Repositories;
using Template.Application.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Services.
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ProductsDb")); // for testing, later can use SQL Server

builder.Services.AddExceptionHandler<Template.Api.Middlewares.GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// DI
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddControllers();

var app = builder.Build();

// Http req pipeline 
app.UseExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Template API");
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
