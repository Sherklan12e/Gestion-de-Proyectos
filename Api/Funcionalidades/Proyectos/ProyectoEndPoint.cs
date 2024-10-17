using Microsoft.AspNetCore.Mvc;

namespace Api.Funcionalidades.Proyectos;

public static class ProyectoEndpoints
{
    public static RouteGroupBuilder MapProyectoEndpoints(this RouteGroupBuilder app)
    {
        app.MapGet("/proyectos", ([FromServices] IProyectoService proyectoService) =>
        {
            var proyectos = proyectoService.GetProyectos();
            return Results.Ok(proyectos);
        });

        app.MapPost("/proyecto", ([FromServices] IProyectoService proyectoService, ProyectoCommandDto proyectoDto) =>
        {
            proyectoService.CreateProyecto(proyectoDto);
            return Results.Ok();
        });

        app.MapPut("/proyecto/{idProyecto}", ([FromServices] IProyectoService proyectoService, Guid idProyecto, ProyectoCommandDto proyectoDto) =>
        {
            proyectoService.UpdateProyecto(idProyecto, proyectoDto);
            return Results.Ok();
        });

        app.MapDelete("/proyecto/{idProyecto}", ([FromServices] IProyectoService proyectoService, Guid idProyecto) =>
        {
            proyectoService.DeleteProyecto(idProyecto);
            return Results.Ok();
        });

        return app;
    }
}

