namespace Api.Funcionalidades.Proyectos;
using Api.Funcionalidades.Usuarios;
using Api.Funcionalidades.Tickets;

public class ProyectoQueryDto : ProyectoCommandDto
{
    public Guid Id { get; set; }
    public DateTime FechaCreacion { get; set; }
    public List<UsuarioQueryDto> Usuarios { get; set; } = [];
    public List<TicketQueryDto> Tickets { get; set; } = [];
}

public class ProyectoCommandDto
{
    public required string Nombre { get; set; }
    public required string Descripcion { get; set; }
    public Guid CreacionUsuario {get;set;}
}