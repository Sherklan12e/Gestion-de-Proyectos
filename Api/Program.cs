using biblioteca.Dominio;
using Api.Persistencia;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

using Api.Funcionalidades.Usuarios;
using Api.Funcionalidades.Proyectos;
using Api.Funcionalidades.Tickets;
using Api.Funcionalidades.Comentarios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("gestiontarea_db");
builder.Services.AddDbContext<GestionTareasDbContext>(option => option.UseMySql(connectionString, new MySqlServerVersion("8.0.39")));

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITicketService, TicketService>();

var options = new DbContextOptionsBuilder<GestionTareasDbContext>();
options.UseMySql(connectionString, new MySqlServerVersion("8.0.39"));
var context = new GestionTareasDbContext(options.Options);
context.Database.Migrate();

// builder.Services.AddServiceManager();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "openapi/{documentName}.json";
    });
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapGroup("/api")
    .MapUsuarioEndpoints()
    .WithTags("Usuario");

app.MapGroup("/api")
    .MapTicketEndpoints()
    .WithTags("Tickets");



// Map all endpoints

// apiGroup.MapProyectoEndpoints();

// apiGroup.MapComentarioEndpoints();

// builder.Services.AddScoped<IComentarioServaice, ComentarioService>();
// builder.Services.AddScoped<IProyectoService, ProyectoService>();


app.Run();
