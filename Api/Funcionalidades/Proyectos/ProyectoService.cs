using Microsoft.EntityFrameworkCore;

namespace Api.Funcionalidades.Proyectos;

public class ProyectoService : IProyectoService
{
    private readonly ApplicationDbContext _context;

    public ProyectoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ProyectoQueryDto> GetProyectos()
    {
        return _context.Proyectos
            .Include(p => p.Tickets)
            .Include(p => p.Usuarios)
            .Select(p => new ProyectoQueryDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                FechaCreacion = p.FechaCreacion,
                FechaInicio = p.FechaInicio,
                FechaFin = p.FechaFin,
                Tickets = p.Tickets.Select(t => new TicketQueryDto
                {
                    Id = t.Id,
                    Titulo = t.Titulo,
                    Descripcion = t.Descripcion,
                    Estado = t.Estado,
                    Prioridad = t.Prioridad,
                    FechaCreacion = t.FechaCreacion,
                    UsuarioAsignadoId = t.UsuarioAsignadoId,
                    ProyectoId = t.ProyectoId
                }).ToList(),
                Usuarios = p.Usuarios.Select(u => new UsuarioQueryDto
                {
                    Id = u.Id,
                    NombreCompleto = u.NombreCompleto,
                    NombreUsuario = u.NombreUsuario,
                    Email = u.Email,
                    Rol = u.Rol
                }).ToList()
            })
            .ToList();
    }

    public void CreateProyecto(ProyectoCommandDto proyectoDto)
    {
        var proyecto = new Proyecto
        {
            Nombre = proyectoDto.Nombre,
            Descripcion = proyectoDto.Descripcion,
            FechaCreacion = DateTime.UtcNow,
            FechaInicio = proyectoDto.FechaInicio,
            FechaFin = proyectoDto.FechaFin
        };

        _context.Proyectos.Add(proyecto);
        _context.SaveChanges();
    }

    public void UpdateProyecto(Guid idProyecto, ProyectoCommandDto proyectoDto)
    {
        var proyecto = _context.Proyectos.Find(idProyecto);
        if (proyecto == null)
            throw new KeyNotFoundException("Proyecto not found");

        proyecto.Nombre = proyectoDto.Nombre;
        proyecto.Descripcion = proyectoDto.Descripcion;
        proyecto.FechaInicio = proyectoDto.FechaInicio;
        proyecto.FechaFin = proyectoDto.FechaFin;

        _context.SaveChanges();
    }

    public void DeleteProyecto(Guid idProyecto)
    {
        var proyecto = _context.Proyectos.Find(idProyecto);
        if (proyecto == null)
            throw new KeyNotFoundException("Proyecto not found");

        _context.Proyectos.Remove(proyecto);
        _context.SaveChanges();
    }
}
