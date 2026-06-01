using Microsoft.EntityFrameworkCore;
using StockManager.Application.Services;
using StockManager.Domain.Interfaces;
using StockManager.Infrastructure.Context;
using StockManager.Infrastructure.Repositories;
using StockManager.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

string? mySqlConnection = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        mySqlConnection,
        new MariaDbServerVersion(new Version(11, 4))
    )
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler(app.Environment);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
