using Api.Persistencia;
using Microsoft.EntityFrameworkCore;
using Api.Funcionalidades.Proyectos;
using Api.Funcionalidades.Comentarios;
using Api.Funcionalidades.Tickets;
using biblioteca.Dominio;
using Microsoft.AspNetCore.Components.Web;
namespace Api.Funcionalidades.Usuarios;
public interface IUsuarioService
{
    List<UsuarioQueryDto> ObtenerUsuarios();
    // UsuarioQueryDto? ObtenerUsuarioPorId(Guid idUsuario);
    void CrearUsuario(UsuarioCommandDto usuarioDto);
    void ActualizarUsuario(Guid idUsuario, UsuarioCommandDto usuarioDto);
    void EliminarUsuario(Guid idUsuario);
    UsuarioQueryDto? AutenticarUsuario(string email, string password);
    UsuarioQueryDto TraerUsuario(Guid idUsuario);
}
public class UsuarioService : IUsuarioService
{
    private readonly GestionTareasDbContext context;

    public UsuarioService(GestionTareasDbContext context)
    {
        this.context = context;
    }

    UsuarioQueryDto? IUsuarioService.AutenticarUsuario(string email, string password)
    {
        var usuario = context.Usuarios.FirstOrDefault(u => u.Email == email && u.Password == password);
        if (usuario == null)
            return null;

        return new UsuarioQueryDto
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Email = usuario.Email,
            Password = usuario.Password,
            FechaCreacion = usuario.FechaCreacion,
            // Agrega otros campos que necesites
        };
    }
    public List<UsuarioQueryDto> ObtenerUsuarios()
    {
        return context.Usuarios
            .Include(u => u.ProyectoAsignados)
            .Include(u => u.ComentariosUsuario)
            .Include(u => u.TicketsAsignados)
                .ThenInclude(t => t.Actividad) // Include comments related to each ticket
            .AsEnumerable()
            .Select(u => new UsuarioQueryDto
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Email = u.Email,
                Password = u.Password,
                FechaCreacion = u.FechaCreacion,
                ProyectoAsignados = (u.ProyectoAsignados ?? new List<Proyecto>()).Select(p => new ProyectoQueryDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    FechaCreacion = p.FechaCreacion
                }).ToList(),
                ComentariosUsuario = (u.ComentariosUsuario ?? new List<Comentario>()).Select(c => new ComentarioQueryDto
                {
                    Id = c.Id,
                    Contenido = c.Contenido,
                    FechaCreacion = c.FechaCreacion,
                    UsuarioId = c.Usuario,
                    TicketId = c.Ticket
                }).ToList(),
                TicketsAsignados = (u.TicketsAsignados ?? new List<Ticket>()).Select(t => new TicketQueryDto
                {
                    Id = t.Id,
                    Nombre = t.Nombre,
                    Descripcion = t.Descripcion,
                    Estado = t.Estado,
                    FechaCreacion = t.FechaInicio,
                    FechaInicio = t.FechaInicio,
                    FechaFin = t.FechaFin,
                    Usuario = t.Usuario,
                    
                }).ToList()
            }).ToList();
    }



    public void CrearUsuario(UsuarioCommandDto usuarioDto)
    {
        var usuario = new Usuario
        {
            Nombre = usuarioDto.Nombre,
            Email = usuarioDto.Email,
            Password = usuarioDto.Password,
            CreacionUsuario = usuarioDto.CreacionUsuario,
            FechaCreacion = DateTime.Now
        };

        context.Usuarios.Add(usuario);
        context.SaveChanges();
    }

    public void ActualizarUsuario(Guid idUsuario, UsuarioCommandDto usuarioDto)
    {
        var usuario = context.Usuarios.Find(idUsuario);
        if (usuario == null)
            throw new KeyNotFoundException("Usuario no encontrado");

        usuario.Nombre = usuarioDto.Nombre;
        usuario.Email = usuarioDto.Email;
        usuario.Password = usuarioDto.Password;

        context.SaveChanges();
    }

    public void EliminarUsuario(Guid idUsuario)
    {
        var usuario = context.Usuarios
            .Include(u => u.ComentariosUsuario)
            .FirstOrDefault(u => u.Id == idUsuario);

        if (usuario == null)
            throw new KeyNotFoundException("Usuario no encontrado");

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
        FechaCreacion = usuario.FechaCreacion,
        ProyectoAsignados = (usuario.ProyectoAsignados ?? new List<Proyecto>()).Select(p => new ProyectoQueryDto
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Descripcion = p.Descripcion,
            FechaCreacion = p.FechaCreacion
        }).ToList(),
        ComentariosUsuario = (usuario.ComentariosUsuario ?? new List<Comentario>()).Select(c => new ComentarioQueryDto
        {
            Id = c.Id,
            Contenido = c.Contenido,
            FechaCreacion = c.FechaCreacion,
            UsuarioId = c.Usuario,
            TicketId = c.Ticket
        }).ToList(),
        TicketsAsignados = (usuario.TicketsAsignados ?? new List<Ticket>()).Select(t => new TicketQueryDto
        {
            Id = t.Id,
            Nombre = t.Nombre,
            Descripcion = t.Descripcion,
            Estado = t.Estado,
            FechaCreacion = t.FechaInicio,
            FechaInicio = t.FechaInicio,
            FechaFin = t.FechaFin,
            Usuario = t.Usuario,
            Actividad = (t.Actividad ?? new List<Comentario>()).Select(a => new ComentarioQueryDto
            {
                Id = a.Id,
                Contenido = a.Contenido,
                FechaCreacion = a.FechaCreacion,
                UsuarioId = a.Usuario,
                TicketId = a.Ticket
            }).ToList()
        }).ToList()
    };
}
   
    
}