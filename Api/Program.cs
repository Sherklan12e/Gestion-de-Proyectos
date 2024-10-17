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
var options = new DbContextOptionsBuilder<GestionTareasDbContext>();
options.UseMySql(connectionString, new MySqlServerVersion("8.0.39"));
var context = new GestionTareasDbContext(options.Options);

context.Database.EnsureCreated();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>{
        options.RouteTemplate = "openapi/{documentName}.json";
    });
    app.MapScalarApiReference();
}


app.UseHttpsRedirection();


var apiGroup = app.MapGroup("/api");

// Map all endpoints
apiGroup.MapUsuarioEndpoints();
apiGroup.MapProyectoEndpoints();
apiGroup.MapTicketEndpoints();
apiGroup.MapComentarioEndpoints();

builder.Services.AddScoped<IComentarioService, IComentarioService>();
builder.Services.AddScoped<IProyectoService, IProyectoService>();
builder.Services.AddScoped<ITicketService, ITicketService>();
builder.Services.AddScoped<IUsuarioService, IUsuarioService>();

app.Run();
