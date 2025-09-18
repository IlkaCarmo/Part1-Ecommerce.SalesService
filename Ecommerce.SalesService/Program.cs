using Ecommerce.SalesService.Data;
using Ecommerce.SalesService.Interfaces;
using Ecommerce.SalesService.Messaging;
using Ecommerce.SalesService.Repositories;
using Ecommerce.SalesService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🔹 EF Core + MySQL
builder.Services.AddDbContext<SalesDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 34)) 
    ));
// Add services to the container.

//DI
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IRabbitMqPublisher, RabbitMqPublisher>();

builder.Services.AddScoped<OrderService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
