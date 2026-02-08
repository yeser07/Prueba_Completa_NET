using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Prueba_Completa_NET.Data;
using Prueba_Completa_NET.Repositories;
using Prueba_Completa_NET.Validators;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registro de servicios para validadores
builder.Services.AddScoped<ClienteCreateValidator>();
builder.Services.AddScoped<ClienteUpdateValidator>();
builder.Services.AddScoped<ProductoCreateValidator>();

//EF Core configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Registro de repositorios
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<ProductoRepository>();

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
