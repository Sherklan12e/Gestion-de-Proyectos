using biblioteca.Dominio;
using Api.Persistencia;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using MySqlX.XDevAPI.Common;
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


app.MapGet("/usuarios", (GestionTareasDbContext context) => {
    var usuarios = context.Usuarios.ToList();
    return Results.Ok(usuarios);
});

app.MapPost("/crear_user", (GestionTareasDbContext context, Usuario usuario) => {
    context.Usuarios.Add(usuario);
    var usuarios = context.Usuarios.ToList();
    return Results.Ok(usuarios);
});

app.Run();

