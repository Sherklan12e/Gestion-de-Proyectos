using biblioteca;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

List<Usuario> usuario1 = [];

app.MapGet("/prueba", () =>{
    return Results.Ok(usuario1);
});
app.MapPost("/subir", (Usuario usuario2) =>{
    usuario1.Add(usuario2);
    return  Results.Ok(usuario2);
});
app.Run();

