using Api.Funcionalidades.Comentarios;
using Api.Funcionalidades.Usuarios;
using Api.Funcionalidades.Tickets;
using Api.Persistencia;
using biblioteca.Dominio;
using biblioteca.Validacion;
using Microsoft.EntityFrameworkCore;

namespace Api.Funcionalidades.Tickets;
public interface ITicketService
{
    List<TicketQueryDto> ObtenerTickets();
    void CrearTicket(TicketCommandDto ticketDto);
    void ActualizarTicket(Guid idTicket, TicketCommandDto ticketDto);
    void DeleteTicket(Guid idTicket);
    void ActualizarEstadoTicket(Guid idTicket, string nuevoEstado);
}

public class TicketService : ITicketService
{
    private readonly GestionTareasDbContext context;
    private DateTime fechaInicioGlobal;

    public TicketService(GestionTareasDbContext context)
    {
        this.context = context;
    }

    public List<TicketQueryDto> ObtenerTickets()
    {
        return context.Tickets
            .Include(t => t.Actividad)
            .Select(t => new TicketQueryDto
            {
                Id = t.Id,
                Nombre = t.Nombre,
                Descripcion = t.Descripcion,
                Estado = t.Estado,
                Usuario = t.Usuario,
                UsuarioAsignadoId = t.Usuario,
                ProyectoId = t.Proyecto,
                FechaInicio = t.Estado.ToLower() == "abierto" ? null :  t.FechaInicio,
                FechaFin = (t.Estado.ToLower() == "cerrado" || t.Estado.ToLower() == "completado" ) ? t.FechaFin : null,
                FechaCreacion = t.FechaCreacion,
                Actividad = t.Actividad.Select(c => new ComentarioQueryDto
                {
                    Id = c.Id,
                    Contenido = c.Contenido,
                    CreacionUsuario = c.Usuario,
                    TicketId = c.Ticket
                }).ToList(),
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
            FechaFin = null,
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
        if (ticket.Estado != ticketDto.Estado){
            if (ticket.Estado.ToLower() == "abierto" )
            {
                ticket.FechaInicio = DateTime.Now;
            }
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

    public void ActualizarEstadoTicket(Guid idTicket, string nuevoEstado)
    {
        Guard.ValidarGuid(idTicket, "ID de ticket");
        Guard.ValidarStringVacio(nuevoEstado, "Estado");

        var ticket = context.Tickets
            .FirstOrDefault(t => t.Id == idTicket);

        if (ticket == null)
            throw new KeyNotFoundException("Ticket no encontrado");

        // Si el estado es diferente, actualiza las fechas correspondientes
        if (ticket.Estado != nuevoEstado)
        {
            if (ticket.Estado.ToLower() == "abierto")
            {   
                ticket.FechaInicio = DateTime.Now;
                fechaInicioGlobal = (DateTime)ticket.FechaInicio;
            }

            // Si el nuevo estado es "Cerrado" o "Completado", establece la fecha fin
            if (nuevoEstado.ToLower() == "cerrado" || nuevoEstado.ToLower() == "completado")
            {
                ticket.FechaFin = DateTime.Now;
            }
            // Si se reabre el ticket (cambia de Cerrado/Completado a otro estado), limpia la fecha fin
            else if (ticket.FechaFin.HasValue)
            {
                ticket.FechaFin = null;
            }
        }

        ticket.Estado = nuevoEstado;
        context.SaveChanges();
    }
}