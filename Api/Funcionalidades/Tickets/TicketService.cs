using Microsoft.EntityFrameworkCore;

namespace Api.Funcionalidades.Tickets;

public class TicketService : ITicketService
{
    private readonly ApplicationDbContext _context;

    public TicketService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<TicketQueryDto> GetTickets()
    {
        return _context.Tickets
            .Include(t => t.Comentarios)
            .Select(t => new TicketQueryDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                Estado = t.Estado,
                Prioridad = t.Prioridad,
                FechaCreacion = t.FechaCreacion,
                UsuarioAsignadoId = t.UsuarioAsignadoId,
                ProyectoId = t.ProyectoId,
                Comentarios = t.Comentarios.Select(c => new ComentarioQueryDto
                {
                    Id = c.Id,
                    Contenido = c.Contenido,
                    FechaCreacion = c.FechaCreacion,
                    UsuarioId = c.UsuarioId,
                    TicketId = c.TicketId
                }).ToList()
            })
            .ToList();
    }

    public void CreateTicket(TicketCommandDto ticketDto)
    {
        var ticket = new Ticket
        {
            Titulo = ticketDto.Titulo,
            Descripcion = ticketDto.Descripcion,
            Estado = ticketDto.Estado,
            Prioridad = ticketDto.Prioridad,
            FechaCreacion = DateTime.UtcNow,
            UsuarioAsignadoId = ticketDto.UsuarioAsignadoId,
            ProyectoId = ticketDto.ProyectoId
        };

        _context.Tickets.Add(ticket);
        _context.SaveChanges();
    }

    public void UpdateTicket(Guid idTicket, TicketCommandDto ticketDto)
    {
        var ticket = _context.Tickets.Find(idTicket);
        if (ticket == null)
            throw new KeyNotFoundException("Ticket not found");

        ticket.Titulo = ticketDto.Titulo;
        ticket.Descripcion = ticketDto.Descripcion;
        ticket.Estado = ticketDto.Estado;
        ticket.Prioridad = ticketDto.Prioridad;

        _context.SaveChanges();
    }

    public void DeleteTicket(Guid idTicket)
    {
        var ticket = _context.Tickets.Find(idTicket);
        if (ticket == null)
            throw new KeyNotFoundException("Ticket not found");

        _context.Tickets.Remove(ticket);
        _context.SaveChanges();
    }
}
