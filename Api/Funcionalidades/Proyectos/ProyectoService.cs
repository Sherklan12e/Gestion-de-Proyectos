using Api.Persistencia;
using Api.Funcionalidades.Usuarios;
using Api.Funcionalidades.Tickets;
using Api.Funcionalidades.Comentarios;
using Microsoft.EntityFrameworkCore;
using biblioteca.Dominio;

namespace Api.Funcionalidades.Proyectos;


public interface IProyectoService
{
    List<ProyectoQueryDto> ObtenerProyectos();
    void CrearProyeto(ProyectoCommandDto proyectoDto);
    void ActualizarProyecto(Guid idProyecto, ProyectoCommandDto proyectoDto);
    void EliminarProyecto(Guid idProyecto);
}

public class ProyectoService : IProyectoService
{
    private readonly GestionTareasDbContext _context;

    public ProyectoService(GestionTareasDbContext context)
    {
        this._context = context;
    }

public List<ProyectoQueryDto> ObtenerProyectos()
{
    return _context.Proyectos
        .Include(p => p.Usuarios)
        .Include(p => p.Tickets)
        .Select(p => new ProyectoQueryDto
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Descripcion = p.Descripcion,
            FechaCreacion = p.FechaCreacion,
            Usuarios = p.Usuarios != null 
                ? p.Usuarios.Select(u => new UsuarioQueryDto
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Email = u.Email,
                    Password = u.Password,
                    FechaCreacion = u.FechaCreacion,
                    ProyectoAsignados = new List<ProyectoQueryDto>(), 
                    TicketsAsignados = new List<TicketQueryDto>() 
                }).ToList() 
                : new List<UsuarioQueryDto>(),

            Tickets = p.Tickets != null 
                ? p.Tickets.Select(t => new TicketQueryDto
                {
                    Id = t.Id,
                    Nombre = t.Nombre,
                    Descripcion = t.Descripcion,
                    Estado = t.Estado,
                    FechaCreacion = DateTime.Now,
                    FechaInicio = t.FechaInicio,
                    FechaFin = t.FechaFin,
                    UsuarioAsignadoId = t.Usuario,
                    Actividad = new List<ComentarioQueryDto>() 
                }).ToList() 
                : new List<TicketQueryDto>()
        }).ToList();
}

    public void CrearProyeto(ProyectoCommandDto proyectoDto)
    {
        var proyecto = new Proyecto
        {
            Nombre = proyectoDto.Nombre,
            Descripcion = proyectoDto.Descripcion,
            CreacionUsuario = proyectoDto.CreacionUsuario,
            FechaCreacion = DateTime.Now
        };

        _context.Proyectos.Add(proyecto);
        _context.SaveChanges();
    }

    public void ActualizarProyecto(Guid idProyecto, ProyectoCommandDto proyectoDto)
    {
        var proyecto = _context.Proyectos.Find(idProyecto);
        if (proyecto == null)
            throw new KeyNotFoundException("Proyecto no encontrado");

        proyecto.Nombre = proyectoDto.Nombre;
        proyecto.Descripcion = proyectoDto.Descripcion;

        _context.SaveChanges();
    }

    public void EliminarProyecto(Guid idProyecto)
    {
        var proyecto = _context.Proyectos 
            .Include(p => p.Tickets)
            .ThenInclude(t => t.Actividad)
            .FirstOrDefault(p => p.Id == idProyecto);

        if (proyecto == null)
            throw new KeyNotFoundException("Proyecto no encontrado");

        // Eliminar comentarios de los tickets
        foreach (var ticket in proyecto.Tickets ?? new List<Ticket>())
        {
            _context.Comentarios.RemoveRange(ticket.Actividad);
        }

        // Eliminar tickets
        _context.Tickets.RemoveRange(proyecto.Tickets ?? new List<Ticket>());

        // Eliminar proyecto
        _context.Proyectos.Remove(proyecto);
        _context.SaveChanges();
    }
}