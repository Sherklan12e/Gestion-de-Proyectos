using Api.Funcionalidades.Comentarios;
using Api.Funcionalidades.Usuarios;
using Api.Funcionalidades.Tickets;
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
            .Include(t => t.Actividad)  // Solo incluimos 'Actividad' si es una navegación a otra entidad.
            .Select(t => new TicketQueryDto
            {
                Id = t.Id,
                Nombre = t.Nombre,
                Descripcion = t.Descripcion,
                Estado = t.Estado,
                UsuarioAsignadoId = t.Usuario,  // Asignamos directamente la clave foránea.
                Actividad = t.Actividad.Select(c => new ComentarioQueryDto
                {
                    Id = c.Id,
                    Contenido = c.Contenido,
                    UsuarioId = c.Usuario,  // Asignamos la propiedad correspondiente.
                    TicketId = c.Ticket
                }).ToList()
            }).ToList();
    }


    public void CrearTicket(TicketCommandDto ticketDto)
    {
        // Buscar el usuario asignado incluyendo sus tickets
        var usuario = context.Usuarios
            .Include(u => u.TicketsAsignados)
            .SingleOrDefault(u => u.Id == ticketDto.UsuarioAsignadoId);
        if (usuario == null)
        {
            throw new KeyNotFoundException("Usuario asignado no encontrado");
        }

        // Buscar el proyecto incluyendo usuarios
        var proyecto = context.Proyectos
            .Include(p => p.Usuarios)
            .Include(p => p.Tickets)
            .SingleOrDefault(p => p.Id == ticketDto.ProyectoId);
        if (proyecto == null)
        {
            throw new KeyNotFoundException("Proyecto no encontrado");
        }

        // Verificar que el proyecto tenga usuarios asignados
        if (proyecto.Usuarios == null || !proyecto.Usuarios.Any())
        {
            throw new InvalidOperationException("El proyecto no tiene usuarios asignados.");
        }

        // Verificar si el usuario está asignado al proyecto
        var usuarioEnProyecto = proyecto.Usuarios.Any(u => u.Id == ticketDto.UsuarioAsignadoId);
        if (!usuarioEnProyecto)
        {
            throw new InvalidOperationException("El usuario no está asignado a este proyecto.");
        }

        // Crear el ticket
        var ticket = new Ticket
        {
            Nombre = ticketDto.Nombre,
            Descripcion = ticketDto.Descripcion,
            Estado = ticketDto.Estado ?? "Abierto",
            Usuario = ticketDto.UsuarioAsignadoId,
            Proyecto = ticketDto.ProyectoId,
            FechaInicio = null,
            FechaCreacion = DateTime.Now,

        };

        // Agregar el ticket a la base de datos
        context.Tickets.Add(ticket);

        // Asignar el ticket al usuario
        usuario.TicketsAsignados?.Add(ticket);
        context.Usuarios.Update(usuario);

        // Asignar el ticket al proyecto
        proyecto.Tickets?.Add(ticket);
        context.Proyectos.Update(proyecto);

        // Guardar los cambios
        context.SaveChanges();
    }


    public void ActualizarTicket(Guid idTicket, TicketCommandDto ticketDto)
    {
        var ticket = context.Tickets
            .FirstOrDefault(t => t.Id == idTicket);

        if (ticket == null)
            throw new KeyNotFoundException("Ticket no encontrado");

        ticket.Nombre = ticketDto.Nombre;
        ticket.Descripcion = ticketDto.Descripcion;
        if (ticketDto.Estado != ticketDto.Estado){
            ticket.FechaInicio = DateTime.Now;
        }
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