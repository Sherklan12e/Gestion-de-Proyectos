namespace Api.Funcionalidades.Usuarios;
using Api.Funcionalidades.Proyectos;
using Api.Funcionalidades.Tickets;
public class UsuarioQueryDto : UsuarioCommandDto
{
    public Guid Id { get; set; }
    public DateTime FechaCreacion { get; set; }
    public List<ProyectoQueryDto> Proyectos { get; set; } = new List<ProyectoQueryDto>();
    public List<TicketQueryDto> TicketsAsignados { get; set; } = new List<TicketQueryDto>();
}

public class UsuarioCommandDto
{
    public required string NombreCompleto { get; set; }
    public required string NombreUsuario { get; set; }
    public required string Email { get; set; }
    public required string Contraseña { get; set; }
    public string? Rol { get; set; }
}

