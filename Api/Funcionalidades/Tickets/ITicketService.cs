namespace Api.Funcionalidades.Tickets;

public interface ITicketService
{
    IEnumerable<TicketQueryDto> GetTickets();
    void CreateTicket(TicketCommandDto ticketDto);
    void UpdateTicket(Guid idTicket, TicketCommandDto ticketDto);
    void DeleteTicket(Guid idTicket);
}
