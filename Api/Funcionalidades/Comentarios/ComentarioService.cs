// Funcionalidades 
using Api.Funcionalidades.Usuarios;
using Api.Persistencia;
using biblioteca.Dominio;
using biblioteca.Validacion;

using Microsoft.EntityFrameworkCore;

namespace Api.Funcionalidades.Comentarios;

public interface IComentarioService
{
    IEnumerable<ComentarioQueryDto> ObtenerComentarios();
    void CrearComentario(ComentarioCommandDto comentarioDto);
    void ActualizarComentario(Guid idComentario, ComentarioCommandDto comentarioDto);
    void EliminarComentario(Guid idComentario);
}


public class ComentarioService : IComentarioService
{
    private readonly GestionTareasDbContext _context;

    public ComentarioService(GestionTareasDbContext context)
    {
        this._context = context;
    }

    public IEnumerable<ComentarioQueryDto> ObtenerComentarios()
    {
        return _context.Comentarios
            .Select(c => new ComentarioQueryDto
            {
                Id = c.Id,
                Contenido = c.Contenido,
                FechaCreacion = c.FechaCreacion,
                CreacionUsuario = c.Usuario,
                TicketId = c.Ticket
            }).ToList();
    }

    public void CrearComentario(ComentarioCommandDto comentarioDto)
    {
        Guard.ValidarNull(comentarioDto, "Comentario");
        Guard.ValidarStringVacio(comentarioDto.Contenido, "Contenido");
        Guard.ValidarGuid(comentarioDto.CreacionUsuario, "ID de usuario creador");
        Guard.ValidarGuid(comentarioDto.TicketId, "ID de ticket");

        // Verificar que el ticket existe
        var ticketExiste = _context.Tickets.Any(t => t.Id == comentarioDto.TicketId);
        if (!ticketExiste)
        {
            throw new KeyNotFoundException($"No se encontró el ticket con ID {comentarioDto.TicketId}");
        }

        // Verificar que el usuario existe
        var usuarioExiste = _context.Usuarios.Any(u => u.Id == comentarioDto.CreacionUsuario);
        if (!usuarioExiste)
        {
            throw new KeyNotFoundException($"No se encontró el usuario con ID {comentarioDto.CreacionUsuario}");
        }

        var comentario = new Comentario
        {
            Contenido = comentarioDto.Contenido,
            Usuario = comentarioDto.CreacionUsuario,
            Ticket = comentarioDto.TicketId,
            CreacionUsuario = comentarioDto.CreacionUsuario,
            FechaCreacion = DateTime.Now
        };

        _context.Comentarios.Add(comentario);
        _context.SaveChanges();
    }

    public void ActualizarComentario(Guid idComentario, ComentarioCommandDto comentarioDto)
    {
        Guard.ValidarGuid(idComentario, "ID de comentario");
        Guard.ValidarNull(comentarioDto, "Comentario");
        Guard.ValidarStringVacio(comentarioDto.Contenido, "Contenido");

        var comentario = _context.Comentarios.Find(idComentario);
        if (comentario == null)
            throw new KeyNotFoundException("Comentario no encontrado");

        comentario.Contenido = comentarioDto.Contenido;

        _context.SaveChanges();
    }

    public void EliminarComentario(Guid idComentario)
    {
        var comentario = _context.Comentarios.Find(idComentario);
        if (comentario == null)
            throw new KeyNotFoundException("Comentario no encontrado");

        _context.Comentarios.Remove(comentario);
        _context.SaveChanges();
    }
}
