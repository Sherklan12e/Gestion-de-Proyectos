using biblioteca.Dominio;
using Api.Persistencia;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

using Api.Funcionalidades.Usuarios;
using Api.Funcionalidades.Proyectos;
using Api.Funcionalidades.Tickets;
using Api.Funcionalidades.Comentarios;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("gestiontarea_db");
builder.Services.AddDbContext<GestionTareasDbContext>(option => option.UseMySql(connectionString, new MySqlServerVersion("8.0.39")));

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();
builder.Services.AddScoped<IProyectoService, ProyectoService>();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Migraciones de base de datos
var options = new DbContextOptionsBuilder<GestionTareasDbContext>();
options.UseMySql(connectionString, new MySqlServerVersion("8.0.39"));
var context = new GestionTareasDbContext(options.Options);
context.Database.Migrate();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "openapi/{documentName}.json";
    });
    app.MapScalarApiReference();
}

// Uso de HTTPS y CORS
app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");

// Endpoints
app.MapGroup("/api")
    .MapUsuarioEndpoints()
    .WithTags("Usuario");

app.MapGroup("/api")
    .MapTicketEndpoints()
    .WithTags("Tickets");

app.MapGroup("/api")
    .MapProyectoEndpoints()
    .WithTags("Proyectos");

app.MapGroup("/api")
    .MapComentarioEndpoints()
    .WithTags("Comentarios");

app.Run();
