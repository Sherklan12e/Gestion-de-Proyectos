using Microsoft.AspNetCore.Mvc;

namespace Api.Funcionalidades.Comentarios;


public static class ComentarioEndpoints
{
    public static RouteGroupBuilder MapComentarioEndpoints(this RouteGroupBuilder app)
    {
        app.MapGet("/comentarios", ([FromServices] IComentarioService comentarioService) =>
        {
             throw new NotImplementedException();
        });

        app.MapPost("/comentario", ([FromServices] IComentarioService comentarioService, ComentarioCommandDto comentarioDto) =>
        {
             throw new NotImplementedException();
        });

        app.MapPut("/comentario/{idComentario}", ([FromServices] IComentarioService comentarioService, Guid idComentario, ComentarioCommandDto comentarioDto) =>
        {
             throw new NotImplementedException();
        });

        app.MapDelete("/comentario/{idComentario}", ([FromServices] IComentarioService comentarioService, Guid idComentario) =>
        {
             throw new NotImplementedException();
        });

        return app;
    }
}

