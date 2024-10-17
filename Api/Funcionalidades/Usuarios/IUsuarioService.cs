namespace Api.Funcionalidades.Usuarios;

public interface IUsuarioService
{
    IEnumerable<UsuarioQueryDto> GetUsuarios();
    UsuarioQueryDto? GetUsuarioById(Guid idUsuario);
    UsuarioQueryDto CreateUsuario(UsuarioCommandDto usuarioDto);
    bool UpdateUsuario(Guid idUsuario, UsuarioCommandDto usuarioDto);
    bool DeleteUsuario(Guid idUsuario);
}
