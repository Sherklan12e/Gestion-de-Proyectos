using Microsoft.AspNetCore.Mvc;

namespace Api.Funcionalidades.Proyectos;

public static class ProyectoEndpoints
{
    public static RouteGroupBuilder MapProyectoEndpoints(this RouteGroupBuilder app)
    {
        app.MapGet("/proyectos", ([FromServices] IProyectoService proyectoService) =>
        {
            return Results.Ok(proyectoService.ObtenerProyectos());
        });
        app.MapGet("/proyectos/{idProyecto}", ([FromServices] IProyectoService proyectoService, Guid idProyecto) =>
        {   
            try 
            {
                var proyecto = proyectoService.ProyectoId(idProyecto);
                return Results.Ok(proyecto);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });
        app.MapPost("/proyectos/{idProyecto}/usuario/{idUsuario}", ([FromServices] IProyectoService proyectoService, Guid idProyecto, Guid idUsuario) =>
        {
            
            proyectoService.AsicnarUsuario(idProyecto, idUsuario);
            return Results.Ok();
            
            
       
        });
        app.MapPost("/proyecto", ([FromServices] IProyectoService proyectoService, ProyectoCommandDto proyectoDto) =>
        {
            proyectoService.CrearProyeto(proyectoDto);
            return Results.Created();
        });

        app.MapPut("/proyecto/{idProyecto}", ([FromServices] IProyectoService proyectoService, Guid idProyecto, ProyectoCommandDto proyectoDto) =>
        {
            try
            {
                proyectoService.ActualizarProyecto(idProyecto, proyectoDto);
                return Results.NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        app.MapDelete("/proyecto/{idProyecto}", ([FromServices] IProyectoService proyectoService, Guid idProyecto) =>
        {
            try
            {
                proyectoService.EliminarProyecto(idProyecto);
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

