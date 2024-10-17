using Microsoft.EntityFrameworkCore;
using biblioteca.Dominio;
namespace Api.Funcionalidades.Comentarios;

public class ComentarioService : IComentarioService
{
    private readonly ApplicationDbContext _context;

    public ComentarioService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ComentarioQueryDto> GetComentarios()
    {
        return _context.Comentarios
            .Select(c => new ComentarioQueryDto
            {
                Id = c.Id,
                Contenido = c.Contenido,
                FechaCreacion = c.FechaCreacion,
                UsuarioId = c.UsuarioId,
                TicketId = c.TicketId
            })
            .ToList();
    }

    public void CreateComentario(ComentarioCommandDto comentarioDto)
    {
        var comentario = new Comentario
        {
            Contenido = comentarioDto.Contenido,
            FechaCreacion = DateTime.UtcNow,
            UsuarioId = comentarioDto.UsuarioId,
            TicketId = comentarioDto.TicketId
        };

        _context.Comentarios.Add(comentario);
        _context.SaveChanges();
    }

    public void UpdateComentario(Guid idComentario, ComentarioCommandDto comentarioDto)
    {
        var comentario = _context.Comentarios.Find(idComentario);
        if (comentario == null)
            throw new KeyNotFoundException("Comentario not found");

        comentario.Contenido = comentarioDto.Contenido;
        _context.SaveChanges();
    }

    public void DeleteComentario(Guid idComentario)
    {
        var comentario = _context.Comentarios.Find(idComentario);
        if (comentario == null)
            throw new KeyNotFoundException("Comentario not found");

        _context.Comentarios.Remove(comentario);
        _context.SaveChanges();
    }
}
