using Microsoft.EntityFrameworkCore;
using WalletManagementApi.Data;
using WalletManagementApi.Repositories;
using WalletManagementApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register WalletDbContext with an in-memory 
builder.Services.AddDbContext<WalletDbContext>(options =>
    options.UseInMemoryDatabase("WalletDb")); // Using an in-memory database
 

// Register the repository and service
builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<WalletService>();

builder.Services.AddEndpointsApiExplorer(); // Enables support for minimal APIs in Swagger
builder.Services.AddSwaggerGen();          // Registers Swagger generator

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
