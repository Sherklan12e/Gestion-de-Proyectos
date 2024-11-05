using Api.Persistencia;
using Microsoft.EntityFrameworkCore;
using Api.Funcionalidades.Proyectos;
using Api.Funcionalidades.Comentarios;
using biblioteca.Dominio;
namespace Api.Funcionalidades.Usuarios;
public interface IUsuarioService
{
    List<UsuarioQueryDto> ObtenerUsuarios();
    // UsuarioQueryDto? ObtenerUsuarioPorId(Guid idUsuario);
    void CrearUsuario(UsuarioCommandDto usuarioDto);
    void ActualizarUsuario(Guid idUsuario, UsuarioCommandDto usuarioDto);
    void EliminarUsuario(Guid idUsuario);
    UsuarioQueryDto? AutenticarUsuario(string email,  string password);
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
                    Fecha = c.Fecha,
                    UsuarioId = c.Usuario,
                    TicketId = c.Ticket
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

    public UsuarioQueryDto? AutenticarUsuario(string email)
    {
        throw new NotImplementedException();
    }

    
   
}