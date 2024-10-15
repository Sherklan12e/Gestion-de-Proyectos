using biblioteca;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

   // Configurar el contexto de la base de datos

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

app.MapGet("/usuarios", async (ApplicationDbContext db) =>
{
       return await db.Usuarios.ToListAsync();
});

app.MapPost("/usuarios", async (Usuario usuario, ApplicationDbContext db) =>
{
    db.Usuarios.Add(usuario);
    await db.SaveChangesAsync();
    return Results.Created($"/usuarios/{usuario.Id}", usuario);
});

app.MapDelete("/usuarios/{id}", async (int id, ApplicationDbContext db) =>
{
    var usuario = await db.Usuarios.FindAsync(id);
    if (usuario is null) return Results.NotFound();

    db.Usuarios.Remove(usuario);
    await db.SaveChangesAsync();
    return Results.NoContent();
});


app.Run();

