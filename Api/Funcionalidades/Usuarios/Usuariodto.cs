namespace Api.Funcionalidades.Usuarios;
using Api.Funcionalidades.Proyectos;
using Api.Funcionalidades.Comentarios;

public class UsuarioQueryDto : UsuarioCommandDto
{
    public Guid Id { get; set; }
    public DateTime FechaCreacion { get; set; }
    public List<ProyectoQueryDto> ProyectoAsignados { get; set; } = [];
    public List<ComentarioQueryDto> ComentariosUsuario { get; set; } = [];
}

public class UsuarioCommandDto
{
    public required string Nombre { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
}