using Microsoft.AspNetCore.Mvc;

namespace Api.Funcionalidades.Tickets;

public static class TicketEndpoints
{
    public static RouteGroupBuilder MapTicketEndpoints(this RouteGroupBuilder app)
    {
        app.MapGet("/tickets", ([FromServices] ITicketService ticketService) =>
        {
            var tickets = ticketService.GetTickets();
            return Results.Ok(tickets);
        });

        app.MapPost("/ticket", ([FromServices] ITicketService ticketService, TicketCommandDto ticketDto) =>
        {
            ticketService.CreateTicket(ticketDto);
            return Results.Ok();
        });

        app.MapPut("/ticket/{idTicket}", ([FromServices] ITicketService ticketService, Guid idTicket, TicketCommandDto ticketDto) =>
        {
            ticketService.UpdateTicket(idTicket, ticketDto);
            return Results.Ok();
        });

        app.MapDelete("/ticket/{idTicket}", ([FromServices] ITicketService ticketService, Guid idTicket) =>
        {
            ticketService.DeleteTicket(idTicket);
            return Results.Ok();
        });

        return app;
    }
}

