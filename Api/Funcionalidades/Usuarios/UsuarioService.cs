// Funcionalidades
using Api.Funcionalidades.Proyectos;
using Api.Funcionalidades.Comentarios;
using Api.Funcionalidades.Tickets;
using biblioteca.Dominio;
using Api.Persistencia;

// Librerias
using Microsoft.EntityFrameworkCore;

namespace Api.Funcionalidades.Usuarios;
using BCrypt;
using BCrypt.Net;
using biblioteca.Validacion;

public interface IUsuarioService
{
    List<UsuarioQueryDto> ObtenerUsuarios();
    void CrearUsuario(UsuarioCommandDto usuarioDto);
    void ActualizarUsuario(Guid idUsuario, UsuarioCommandDto usuarioDto);
    void EliminarUsuario(Guid idUsuario);
    bool ValidarUsuario(string email, string password);
    UsuarioQueryDto TraerUsuario(Guid idUsuario);
}


public class UsuarioService : IUsuarioService
{
    private readonly GestionTareasDbContext context;

    public UsuarioService(GestionTareasDbContext context)
    {
        this.context = context;
    }



    public bool ValidarUsuario(string email, string password)
    {
        var usuario = context.Usuarios.FirstOrDefault(u => u.Email == email);

        if (usuario == null)
        {
            return false;
        }
        bool isPasswordValid = BCrypt.Verify(password, usuario.Password);

        return isPasswordValid;
    }

    public List<UsuarioQueryDto> ObtenerUsuarios()
    {
        return context.Usuarios
            .Include(u => u.ProyectoAsignados)
            .Include(u => u.ComentariosUsuario)
            .Include(u => u.TicketsAsignados)
                .ThenInclude(t => t.Actividad)
            .AsEnumerable()
            .Select(u => new UsuarioQueryDto
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Email = u.Email,
                Password = u.Password,
                CreacionUsuario = u.CreacionUsuario != Guid.Empty ? u.CreacionUsuario : u.Id,
                ProyectoAsignados = (u.ProyectoAsignados ?? new List<Proyecto>()).Select(p => new ProyectoQueryDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                }).ToList(),
                ComentariosUsuario = (u.ComentariosUsuario ?? new List<Comentario>()).Select(c => new ComentarioQueryDto
                {
                    Id = c.Id,
                    Contenido = c.Contenido,
                    CreacionUsuario = c.Usuario,
                    TicketId = c.Ticket,
                    FechaCreacion = c.FechaCreacion
                }).ToList(),
                TicketsAsignados = (u.TicketsAsignados ?? new List<Ticket>()).Select(t => new TicketQueryDto
                {
                    Id = t.Id,
                    Nombre = t.Nombre,
                    Descripcion = t.Descripcion,
                    Estado = t.Estado,
                    FechaInicio = t.FechaInicio,
                    FechaFin = t.FechaFin,
                    Usuario = t.Usuario
                }).ToList()
            }).ToList();
    }



    public void CrearUsuario(UsuarioCommandDto usuarioDto)
    {
        Guard.ValidarNull(usuarioDto, "Usuario");
        Guard.ValidarStringVacio(usuarioDto.Nombre, "Nombre");
        Guard.ValidarStringVacio(usuarioDto.Email, "Email");
        Guard.ValidarEmail(usuarioDto.Email);
        Guard.ValidarStringVacio(usuarioDto.Password, "Contraseña");
        Guard.ValidarLongitudMinima(usuarioDto.Password, 6, "Contraseña");
        var existeEmail = context.Usuarios.Any(u => u.Email == usuarioDto.Email);
        if (existeEmail)
        {
            throw new InvalidOperationException("Ya existe un usuario con este correo electrónico");
        }

        var usuario = new Usuario
        {
            Nombre = usuarioDto.Nombre,
            Email = usuarioDto.Email,
            Password = BCrypt.HashPassword(usuarioDto.Password),
            CreacionUsuario = usuarioDto.CreacionUsuario,
            FechaCreacion = DateTime.Now
        };

        context.Usuarios.Add(usuario);
        context.SaveChanges();
    }

    public void ActualizarUsuario(Guid idUsuario, UsuarioCommandDto usuarioDto)
    {
        Guard.ValidarGuid(idUsuario, "ID de usuario");
        Guard.ValidarNull(usuarioDto, "Usuario");
        Guard.ValidarStringVacio(usuarioDto.Nombre, "Nombre");
        Guard.ValidarStringVacio(usuarioDto.Email, "Email");
        Guard.ValidarEmail(usuarioDto.Email);

        var usuario = context.Usuarios.Find(idUsuario);
        if (usuario == null)
            throw new KeyNotFoundException("Usuario no encontrado");

        usuario.Nombre = usuarioDto.Nombre;
        usuario.Email = usuarioDto.Email;

        if (!string.IsNullOrEmpty(usuarioDto.Password))
        {
            usuario.Password = BCrypt.HashPassword(usuarioDto.Password);
        }

        context.SaveChanges();
    }

    public void EliminarUsuario(Guid idUsuario)
    {
        Guard.ValidarGuid(idUsuario, "ID de usuario");
        var usuario = context.Usuarios
            .Include(u => u.ProyectoAsignados)
            .FirstOrDefault(u => u.Id == idUsuario);
        var comentarios = context.Comentarios
            
            .FirstOrDefault(c => c.CreacionUsuario == idUsuario);
        
        Guard.ValidarNull(usuario,"usuario ");
        Guard.ValidarNull(comentarios, "comentarios");
        
        context.Comentarios.Remove(comentarios);
        var proyectosCreados = context.Proyectos
            .Where(p => p.CreacionUsuario == idUsuario)
            .ToList();

        if (proyectosCreados.Any())
        {
            context.Proyectos.RemoveRange(proyectosCreados);
        }

        context.Usuarios.Remove(usuario);
        context.SaveChanges();
    }



    UsuarioQueryDto IUsuarioService.TraerUsuario(Guid idUsuario)
    {
        var usuario = context.Usuarios
            .Include(u => u.ProyectoAsignados)
            .Include(u => u.ComentariosUsuario)
            .Include(u => u.TicketsAsignados)
                .ThenInclude(t => t.Actividad)
            .FirstOrDefault(u => u.Id == idUsuario);

        if (usuario == null)
            throw new KeyNotFoundException("Usuario no encontrado");

        return new UsuarioQueryDto
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Email = usuario.Email,
            Password = usuario.Password,
            ProyectoAsignados = (usuario.ProyectoAsignados ?? new List<Proyecto>()).Select(p => new ProyectoQueryDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
            }).ToList(),
            ComentariosUsuario = (usuario.ComentariosUsuario ?? new List<Comentario>()).Select(c => new ComentarioQueryDto
            {
                Id = c.Id,
                Contenido = c.Contenido,
                CreacionUsuario = c.Usuario,
                TicketId = c.Ticket
            }).ToList(),
            TicketsAsignados = (usuario.TicketsAsignados ?? new List<Ticket>()).Select(t => new TicketQueryDto
            {
                Id = t.Id,
                Nombre = t.Nombre,
                Descripcion = t.Descripcion,
                Estado = t.Estado,
                FechaInicio = t.FechaInicio,
                FechaFin = t.FechaFin,
                Usuario = t.Usuario,
                Actividad = (t.Actividad ?? new List<Comentario>()).Select(a => new ComentarioQueryDto
                {
                    Id = a.Id,
                    Contenido = a.Contenido,
                    CreacionUsuario = a.Usuario,
                    TicketId = a.Ticket
                }).ToList()
            }).ToList()
        };
    }


}