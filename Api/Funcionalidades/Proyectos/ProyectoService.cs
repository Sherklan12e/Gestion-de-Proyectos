// Funcionalidades 
using Api.Persistencia;
using Api.Funcionalidades.Usuarios;
using Api.Funcionalidades.Tickets;
using Api.Funcionalidades.Comentarios;
using biblioteca.Dominio;
using biblioteca.Validacion;

// Librerias
using Microsoft.EntityFrameworkCore;

namespace Api.Funcionalidades.Proyectos;


public interface IProyectoService
{
    List<ProyectoQueryDto> ObtenerProyectos();
    void CrearProyeto(ProyectoCommandDto proyectoDto);
    void ActualizarProyecto(Guid idProyecto, ProyectoCommandDto proyectoDto);
    void EliminarProyecto(Guid idProyecto);
    void AsicnarUsuario(Guid idProyecto, Guid idUsuario);

    ProyectoQueryDto ProyectoId(Guid idProyecto);
}


public class ProyectoService : IProyectoService
{
    private readonly GestionTareasDbContext _context;

    public ProyectoService(GestionTareasDbContext context)
    {
        this._context = context;
    }

    public void AsicnarUsuario(Guid idProyecto, Guid idUsuario)
    {
        var proyecto = _context.Proyectos
            .Include(p => p.Usuarios)
            .SingleOrDefault(proyecto => proyecto.Id == idProyecto);
        var usuario = _context.Usuarios.SingleOrDefault(usuario => usuario.Id == idUsuario);

        if (proyecto == null)
        {
            throw new KeyNotFoundException("Proyecto no encontrado");


        }

        if (usuario == null)
        {
            throw new KeyNotFoundException("Usuario no encontrado");
        }

       
        if (proyecto.Usuarios.Any(u => u.Id == idUsuario))
        {
            throw new InvalidOperationException("El usuario ya está asignado a este proyecto");
        }

        
        proyecto.Usuarios.Add(usuario);
        _context.SaveChanges();
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
                CreacionUsuario = p.CreacionUsuario,
                Usuarios = p.Usuarios != null
                    ? p.Usuarios.Select(u => new UsuarioQueryDto
                    {
                        Id = u.Id,
                        Nombre = u.Nombre,
                        Email = u.Email,
                        Password = u.Password, 
                        FechaCreacion = u.FechaCreacion,
                        CreacionUsuario = u.CreacionUsuario
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
        Guard.ValidarNull(proyectoDto, "Proyecto");
        Guard.ValidarStringVacio(proyectoDto.Nombre, "Nombre");
        Guard.ValidarStringVacio(proyectoDto.Descripcion, "Descripción");
        Guard.ValidarGuid(proyectoDto.CreacionUsuario, "ID de usuario creador");

        var usuario = _context.Usuarios.SingleOrDefault(user => user.Id == proyectoDto.CreacionUsuario);
        if (usuario is not null)
        {
            var proyecto = new Proyecto
            {
                Nombre = proyectoDto.Nombre,
                Descripcion = proyectoDto.Descripcion,
                CreacionUsuario = proyectoDto.CreacionUsuario,
                FechaCreacion = DateTime.Now
            };
            // Guarda al creardor
            usuario.ProyectoAsignados.Add(proyecto);
            _context.Usuarios.Update(usuario);

            _context.Proyectos.Add(proyecto);
            _context.SaveChanges();
        }

    }

    public void ActualizarProyecto(Guid idProyecto, ProyectoCommandDto proyectoDto)
    {
        Guard.ValidarGuid(idProyecto, "ID de proyecto");
        Guard.ValidarNull(proyectoDto, "Proyecto");
        Guard.ValidarStringVacio(proyectoDto.Nombre, "Nombre");
        Guard.ValidarStringVacio(proyectoDto.Descripcion, "Descripción");

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

    public ProyectoQueryDto ProyectoId(Guid idProyecto)
    {
        var proyecto = _context.Proyectos
            .Include(p => p.Usuarios)
            .Include(p => p.Tickets)
            .FirstOrDefault(p => p.Id == idProyecto);

        if (proyecto == null)
        {
            throw new KeyNotFoundException("Proyecto no encontrado");
        }

        return new ProyectoQueryDto
        {
            Id = proyecto.Id,
            Nombre = proyecto.Nombre,
            Descripcion = proyecto.Descripcion,
            FechaCreacion = proyecto.FechaCreacion,
            CreacionUsuario = proyecto.CreacionUsuario,
            Usuarios = proyecto.Usuarios?.Select(u => new UsuarioQueryDto
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Email = u.Email,
                Password = u.Password,
                FechaCreacion = u.FechaCreacion,
                CreacionUsuario = u.CreacionUsuario
            }).ToList() ?? new List<UsuarioQueryDto>(),
            Tickets = proyecto.Tickets?.Select(t => new TicketQueryDto
            {
                Id = t.Id,
                Nombre = t.Nombre,
                Descripcion = t.Descripcion,
                Estado = t.Estado,
                FechaCreacion = t.FechaCreacion,
                FechaInicio = t.FechaInicio,
                FechaFin = t.FechaFin,
                UsuarioAsignadoId = t.Usuario
            }).ToList() ?? new List<TicketQueryDto>()
        };
    }
}