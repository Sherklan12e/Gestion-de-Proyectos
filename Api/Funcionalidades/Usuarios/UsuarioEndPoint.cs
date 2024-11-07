using Microsoft.AspNetCore.Mvc;

namespace Api.Funcionalidades.Usuarios;

public static class UsuarioEndpoints
{
    public static RouteGroupBuilder MapUsuarioEndpoints(this RouteGroupBuilder app)
    {
        app.MapGet("/", async (IUsuarioService usuarioService) =>
        {
            return Results.Ok(usuarioService.ObtenerUsuarios());
        });
        app.MapPost("/login", async (IUsuarioService usuarioService, UsuarioCommandDto usuarioDto) =>
        {
            var usuario = usuarioService.AutenticarUsuario(usuarioDto.Email, usuarioDto.Password);
            if (usuario == null)
                return Results.NotFound("Credenciales incorrectas");

            return Results.Ok(usuario);
        });
        // app.MapGet("/{id}", async (Guid id, IUsuarioService usuarioService) =>
        // {
        //     var usuario = usuarioService.ObtenerUsuarioPorId(id);
        //     if (usuario == null)
        //         return Results.NotFound("Usuario no encontrado");
        //     return Results.Ok(usuario);
        // });
        app.MapGet("/{id}", async (Guid id,IUsuarioService usuarioService) => {
            var usuario = usuarioService.TraerUsuario(id);
            if (usuario == null){
                return Results.NotFound("no encontrado");
            }
            return Results.Ok(usuario);
        });
            
        
        app.MapPost("/usuario", async (UsuarioCommandDto usuarioDto, IUsuarioService usuarioService) =>
        {
            usuarioService.CrearUsuario(usuarioDto);
            return Results.Created();
        });

        app.MapPut("/{id}", async (Guid id, UsuarioCommandDto usuarioDto, IUsuarioService usuarioService) =>
        {
            try
            {
                usuarioService.ActualizarUsuario(id, usuarioDto);
                return Results.NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        app.MapDelete("/{id}", async (Guid id, IUsuarioService usuarioService) =>
        {
            try
            {
                usuarioService.EliminarUsuario(id);
                return Results.NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        return app;
    }
}