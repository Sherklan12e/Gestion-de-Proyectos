using Microsoft.AspNetCore.Mvc;

namespace Api.Funcionalidades.Comentarios;

public static class ComentarioEndpoints
{
    public static RouteGroupBuilder MapComentarioEndpoints(this RouteGroupBuilder app)
    {
        app.MapGet("/comentarios", ([FromServices] IComentarioService comentarioService) =>
        {
            var comentarios = comentarioService.ObtenerComentarios();
            return Results.Ok(comentarios);
        });

        app.MapPost("/comentario", ([FromServices] IComentarioService comentarioService, ComentarioCommandDto comentarioDto) =>
        {
            try
            {
                comentarioService.CrearComentario(comentarioDto);
                return Results.Created($"/comentario/{comentarioDto.TicketId}", comentarioDto);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        app.MapPut("/comentario/{idComentario}", ([FromServices] IComentarioService comentarioService, Guid idComentario, ComentarioCommandDto comentarioDto) =>
        {
            try
            {
                comentarioService.ActualizarComentario(idComentario, comentarioDto);
                return Results.NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        app.MapDelete("/comentario/{idComentario}", ([FromServices] IComentarioService comentarioService, Guid idComentario) =>
        {
            try
            {
                comentarioService.EliminarComentario(idComentario);
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

