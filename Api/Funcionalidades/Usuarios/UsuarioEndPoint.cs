using Microsoft.AspNetCore.Mvc;

namespace Api.Funcionalidades.Usuarios;

public static class UsuarioEndpoints
{
    public static RouteGroupBuilder MapUsuarioEndpoints(this RouteGroupBuilder app)
    {
        app.MapGet("/usuarios", ([FromServices] IUsuarioService usuarioService) =>
        {
            var usuarios = usuarioService.GetUsuarios();
            return Results.Ok(usuarios);
        });

        app.MapGet("/usuario/{idUsuario}", ([FromServices] IUsuarioService usuarioService, Guid idUsuario) =>
        {
            var usuario = usuarioService.GetUsuarioById(idUsuario);
            return usuario != null ? Results.Ok(usuario) : Results.NotFound();
        });

        app.MapPost("/usuario", ([FromServices] IUsuarioService usuarioService, UsuarioCommandDto usuarioDto) =>
        {
            var nuevoUsuario = usuarioService.CreateUsuario(usuarioDto);
            return Results.Created($"/usuario/{nuevoUsuario.Id}", nuevoUsuario);
        });

        app.MapPut("/usuario/{idUsuario}", ([FromServices] IUsuarioService usuarioService, Guid idUsuario, UsuarioCommandDto usuarioDto) =>
        {
            var actualizado = usuarioService.UpdateUsuario(idUsuario, usuarioDto);
            return actualizado ? Results.Ok() : Results.NotFound();
        });

        app.MapDelete("/usuario/{idUsuario}", ([FromServices] IUsuarioService usuarioService, Guid idUsuario) =>
        {
            var eliminado = usuarioService.DeleteUsuario(idUsuario);
            return eliminado ? Results.Ok() : Results.NotFound();
        });

        return app;
    }
}

