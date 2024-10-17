using Microsoft.EntityFrameworkCore;

namespace Api.Funcionalidades.Usuarios;

public class UsuarioService : IUsuarioService
{
    private readonly ApplicationDbContext _context;

    public UsuarioService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<UsuarioQueryDto> GetUsuarios()
    {
        return _context.Usuarios
            .Include(u => u.Proyectos)
            .Include(u => u.TicketsAsignados)
            .Select(u => new UsuarioQueryDto
            {
                Id = u.Id,
                NombreCompleto = u.NombreCompleto,
                NombreUsuario = u.NombreUsuario,
                Email = u.Email,
                Rol = u.Rol,
                FechaCreacion = u.FechaCreacion,
                Proyectos = u.Proyectos.Select(p => new ProyectoQueryDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    FechaCreacion = p.FechaCreacion,
                    FechaInicio = p.FechaInicio,
                    FechaFin = p.FechaFin
                }).ToList(),
                TicketsAsignados = u.TicketsAsignados.Select(t => new TicketQueryDto
                {
                    Id = t.Id,
                    Titulo = t.Titulo,
                    Descripcion = t.Descripcion,
                    Estado = t.Estado,
                    Prioridad = t.Prioridad,
                    FechaCreacion = t.FechaCreacion,
                    UsuarioAsignadoId = t.UsuarioAsignadoId,
                    ProyectoId = t.ProyectoId
                }).ToList()
            })
            .ToList();
    }

    public UsuarioQueryDto? GetUsuarioById(Guid idUsuario)
    {
        return _context.Usuarios
            .Include(u => u.Proyectos)
            .Include(u => u.TicketsAsignados)
            .Where(u => u.Id == idUsuario)
            .Select(u => new UsuarioQueryDto
            {
                Id = u.Id,
                NombreCompleto = u.NombreCompleto,
                NombreUsuario = u.NombreUsuario,
                Email = u.Email,
                Rol = u.Rol,
                FechaCreacion = u.FechaCreacion,
                Proyectos = u.Proyectos.Select(p => new ProyectoQueryDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    FechaCreacion = p.FechaCreacion,
                    FechaInicio = p.FechaInicio,
                    FechaFin = p.FechaFin
                }).ToList(),
                TicketsAsignados = u.TicketsAsignados.Select(t => new TicketQueryDto
                {
                    Id = t.Id,
                    Titulo = t.Titulo,
                    Descripcion = t.Descripcion,
                    Estado = t.Estado,
                    Prioridad = t.Prioridad,
                    FechaCreacion = t.FechaCreacion,
                    UsuarioAsignadoId = t.UsuarioAsignadoId,
                    ProyectoId = t.ProyectoId
                }).ToList()
            })
            .FirstOrDefault();
    }

    public UsuarioQueryDto CreateUsuario(UsuarioCommandDto usuarioDto)
    {
        var usuario = new Usuario
        {
            NombreCompleto = usuarioDto.NombreCompleto,
            NombreUsuario = usuarioDto.NombreUsuario,
            Email = usuarioDto.Email,
            Contraseña = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Contraseña),
            Rol = usuarioDto.Rol,
            FechaCreacion = DateTime.UtcNow
        };

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

        return new UsuarioQueryDto
        {
            Id = usuario.Id,
            NombreCompleto = usuario.NombreCompleto,
            NombreUsuario = usuario.NombreUsuario,
            Email = usuario.Email,
            Rol = usuario.Rol,
            FechaCreacion = usuario.FechaCreacion
        };
    }

    public bool UpdateUsuario(Guid idUsuario, UsuarioCommandDto usuarioDto)
    {
        var usuario = _context.Usuarios.Find(idUsuario);
        if (usuario == null)
            return false;

        usuario.NombreCompleto = usuarioDto.NombreCompleto;
        usuario.NombreUsuario = usuarioDto.NombreUsuario;
        usuario.Email = usuarioDto.Email;
        if (!string.IsNullOrEmpty(usuarioDto.Contraseña))
        {
            usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Contraseña);
        }
        usuario.Rol = usuarioDto.Rol;

        _context.SaveChanges();
        return true;
    }

    public bool DeleteUsuario(Guid idUsuario)
    {
        var usuario = _context.Usuarios.Find(idUsuario);
        if (usuario == null)
            return false;

        _context.Usuarios.Remove(usuario);
        _context.SaveChanges();
        return true;
    }
}
