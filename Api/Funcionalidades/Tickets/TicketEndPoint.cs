using Microsoft.AspNetCore.Mvc;
using biblioteca.Dominio;

namespace Api.Funcionalidades.Tickets;

public static class TicketEndpoints
{
    public static RouteGroupBuilder MapTicketEndpoints(this RouteGroupBuilder app)
    {
        app.MapGet("/tickets", ([FromServices] ITicketService ticketService) =>
        {
            var tickets = ticketService.ObtenerTickets();
            return Results.Ok(tickets);
        });

        app.MapGet("/tickets/{idTicket}" ,([FromServices] ITicketService  ticketService , Guid idTicket) => 
        {
            try{

                var ticket = ticketService.TraerTicket(idTicket);
                return Results.Ok(ticket);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
            
        });

        app.MapPost("/ticket", ([FromServices] ITicketService ticketService, TicketCommandDto ticketDto) =>
        {
            ticketService.CrearTicket(ticketDto);
            return Results.Created($"/ticket/{ticketDto.UsuarioAsignadoId}", ticketDto);
        });

        app.MapPut("/ticket/{idTicket}", ([FromServices] ITicketService ticketService, Guid idTicket, TicketCommandDto ticketDto) =>
        {
            try
            {
                ticketService.ActualizarTicket(idTicket, ticketDto);
                return Results.NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });
        app.MapPut("/ticket/actualizarestado/{idTicket}", ([FromServices] ITicketService ticketService, Guid idTicket, [FromBody] ActualizarEstadoTicketDto dto) =>
        {   
            try
            {
                ticketService.ActualizarEstadoTicket(idTicket, dto.Estado);
                return Results.NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });
        app.MapDelete("/ticket/{idTicket}", ([FromServices] ITicketService ticketService, Guid idTicket) =>
        {
            try
            {
                ticketService.DeleteTicket(idTicket);
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

