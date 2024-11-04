using Api.Funcionalidades.Comentarios;
using Api.Funcionalidades.Usuarios;
using Api.Persistencia;
using biblioteca.Dominio;
using Microsoft.EntityFrameworkCore;

namespace Api.Funcionalidades.Tickets;
public interface ITicketService
{
    List<TicketQueryDto> ObtenerTickets();
    void CrearTicket(TicketCommandDto ticketDto);
    void ActualizarTicket(Guid idTicket, TicketCommandDto ticketDto);
    void DeleteTicket(Guid idTicket);
}

public class TicketService : ITicketService
{
    private readonly GestionTareasDbContext context;

    public TicketService(GestionTareasDbContext context)
    {
        this.context = context;
    }

    public List<TicketQueryDto> ObtenerTickets()
    {
        return context.Tickets
            .Include(t => t.Actividad)
            .Include(t => t.Usuario)
            .Select(t => new TicketQueryDto
            {
                Id = t.Id,
                Nombre = t.Nombre,
                Descripcion = t.Descripcion,
                Estado = t.Estado,
                FechaCreacion = t.FechaInicio,
                UsuarioAsignadoId = t.Usuario,
                Actividad = t.Actividad.Select(c => new ComentarioQueryDto
                {
                    Id = c.Id,
                    Contenido = c.Contenido,
                    FechaCreacion = c.FechaCreacion,
                    UsuarioId = c.Usuario,
                    TicketId = c.Ticket
                }).ToList()
            }).ToList();
    }

    public void CrearTicket(TicketCommandDto ticketDto)
    {
        var usuario = context.Usuarios.Find(ticketDto.UsuarioAsignadoId);
        if (usuario == null)
            throw new KeyNotFoundException("Usuario asignado no encontrado");

        var ticket = new Ticket
        {
            Nombre = ticketDto.Nombre,
            Descripcion = ticketDto.Descripcion,
            Estado = ticketDto.Estado,
            Usuario = usuario.Id,
            FechaInicio = DateTime.Now
        };

        context.Tickets.Add(ticket);
        context.SaveChanges();
    }

    public void ActualizarTicket(Guid idTicket, TicketCommandDto ticketDto)
    {
        var ticket = context.Tickets
            .Include(t => t.Usuario)
            .FirstOrDefault(t => t.Id == idTicket);

        if (ticket == null)
            throw new KeyNotFoundException("Ticket no encontrado");

        ticket.Nombre = ticketDto.Nombre;
        ticket.Descripcion = ticketDto.Descripcion;
        ticket.Estado = ticketDto.Estado;

        context.SaveChanges();
    }

    public void DeleteTicket(Guid idTicket)
    {
        var ticket = context.Tickets
            .Include(t => t.Actividad)
            .FirstOrDefault(t => t.Id == idTicket);

        if (ticket == null)
            throw new KeyNotFoundException("Ticket no encontrado");

        context.Comentarios.RemoveRange(ticket.Actividad);
        context.Tickets.Remove(ticket);
        context.SaveChanges();
    }
}