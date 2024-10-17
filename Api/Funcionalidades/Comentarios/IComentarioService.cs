namespace Api.Funcionalidades.Comentarios;

public interface IComentarioService
{
    IEnumerable<ComentarioQueryDto> GetComentarios();
    void CreateComentario(ComentarioCommandDto comentarioDto);
    void UpdateComentario(Guid idComentario, ComentarioCommandDto comentarioDto);
    void DeleteComentario(Guid idComentario);
}
