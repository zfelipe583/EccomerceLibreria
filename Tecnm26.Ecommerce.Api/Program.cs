using Dapper.Contrib.Extensions;
using Tecnm26.Ecommerce.Api.DataAccess;
using Tecnm26.Ecommerce.Api.DataAccess.Interfaces;
using Tecnm26.Ecommerce.Api.Repositories;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUsuarioRepository,UsuarioRepository>();
builder.Services.AddScoped<IAutoraRepository,AutoraRepository>();
builder.Services.AddScoped<ILibrosRepository, LibrosRepository>();
builder.Services.AddScoped<IVentasRepository, VentasRepository>();
builder.Services.AddScoped<IDetalleVentasRepository, DetalleVentasRepository>();
builder.Services.AddScoped<IDetalleLibrosRepository, DetalleLibrosRepository>();

builder.Services.AddScoped<IDbContext, DbContext>();

SqlMapperExtensions.TableNameMapper = entityType =>
{
    var name = entityType.ToString();
    if (name.Contains("Tecnm26.Ecommerce.Core.Entities."))
        name = name.Replace("Tecnm26.Ecommerce.Core.Entities.", "");

    var letters = name.ToCharArray();
    letters[0] = char.ToUpper(letters[0]);
    return new string(letters);
};

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();