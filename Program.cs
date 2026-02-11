using Microsoft.EntityFrameworkCore;
using Prueba_Completa_NET.Data;
using Prueba_Completa_NET.Interfaces.IRepository;
using Prueba_Completa_NET.Interfaces.IServices;
using Prueba_Completa_NET.Mappings;
using Prueba_Completa_NET.Repositories;
using Prueba_Completa_NET.Services;
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
builder.Services.AddScoped<ProductoUpdateValidator>();

//EF Core configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//AutoMapper configuration
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

//Registro de repositorios
builder.Services.AddScoped<IClienteRepository,ClienteRepository>();
builder.Services.AddScoped<IProductoRepository,ProductoRepository>();
builder.Services.AddScoped<IOrdenRepository,OrdenRepository>();

//Registro de servicios
builder.Services.AddScoped<IClienteService,ClienteService>();
builder.Services.AddScoped<IProductoService,ProductoService>();
builder.Services.AddScoped<IOrdenService,OrdenService>();

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
