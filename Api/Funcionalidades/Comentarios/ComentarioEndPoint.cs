using Microsoft.AspNetCore.Mvc;

namespace Api.Funcionalidades.Comentarios;


public static class ComentarioEndpoints
{
    public static RouteGroupBuilder MapComentarioEndpoints(this RouteGroupBuilder app)
    {
        app.MapGet("/comentarios", ([FromServices] IComentarioService comentarioService) =>
        {
            var comentarios = comentarioService.GetComentarios();
            return Results.Ok(comentarios);
        });

        app.MapPost("/comentario", ([FromServices] IComentarioService comentarioService, ComentarioCommandDto comentarioDto) =>
        {
            comentarioService.CreateComentario(comentarioDto);
            return Results.Ok();
        });

        app.MapPut("/comentario/{idComentario}", ([FromServices] IComentarioService comentarioService, Guid idComentario, ComentarioCommandDto comentarioDto) =>
        {
            comentarioService.UpdateComentario(idComentario, comentarioDto);
            return Results.Ok();
        });

        app.MapDelete("/comentario/{idComentario}", ([FromServices] IComentarioService comentarioService, Guid idComentario) =>
        {
            comentarioService.DeleteComentario(idComentario);
            return Results.Ok();
        });

        return app;
    }
}

