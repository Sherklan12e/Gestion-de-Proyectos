// using Microsoft.EntityFrameworkCore;
// using biblioteca.Dominio;
// using Api.Funcionalidades.Usuarios;
// using Api.Persistencia;
// using biblioteca.Dominio;
// namespace Api.Funcionalidades.Comentarios;

// public interface IComentarioService
// {
//     IEnumerable<ComentarioQueryDto> ObtenerComentarios();
//     void CrearComentario(ComentarioCommandDto comentarioDto);
//     void ActualizarComentario(Guid idComentario, ComentarioCommandDto comentarioDto);
//     void EliminarComentario(Guid idComentario);
// }


// public class ComentarioService : IComentarioService
// {
//     private readonly GestionTareasDbContext _context;

//     public ComentarioService(GestionTareasDbContext context)
//     {
//         this._context = context;
//     }

//     public IEnumerable<ComentarioQueryDto> ObtenerComentarios()
//     {
//         return _context.Comentarios
//             .Select(c => new ComentarioQueryDto
//             {
//                 Id = c.Id,
//                 Contenido = c.Contenido,
//                 FechaCreacion = c.FechaCreacion,
//                 UsuarioId = c.Usuario,
//                 TicketId = c.Ticket
//             }).ToList();
//     }

//     public void CrearComentario(ComentarioCommandDto comentarioDto)
//     {
//         var comentario = new Comentario
//         {
//             Contenido = comentarioDto.Contenido,
//             CreacionUsuario = "Sistema",
//             FechaCreacion = DateTime.Now,
//             Fecha = DateTime.Now
//         };

//         _context.Comentarios.Add(comentario);
//         _context.SaveChanges();
//     }

//     public void ActualizarComentario(Guid idComentario, ComentarioCommandDto comentarioDto)
//     {
//         var comentario = _context.Comentarios.Find(idComentario);
//         if (comentario == null)
//             throw new KeyNotFoundException("Comentario no encontrado");

//         comentario.Contenido = comentarioDto.Contenido;

//         _context.SaveChanges();
//     }

//     public void EliminarComentario(Guid idComentario)
//     {
//         var comentario = _context.Comentarios.Find(idComentario);
//         if (comentario == null)
//             throw new KeyNotFoundException("Comentario no encontrado");

//         _context.Comentarios.Remove(comentario);
//         _context.SaveChanges();
//     }
// }
