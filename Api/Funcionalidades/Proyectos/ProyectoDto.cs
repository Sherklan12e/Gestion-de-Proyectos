namespace Api.Funcionalidades.Proyectos;
using Api.Funcionalidades.Usuarios;
using Api.Funcionalidades.Tickets;
public class ProyectoQueryDto : ProyectoCommandDto
{
    public Guid Id { get; set; }
    public DateTime FechaCreacion { get; set; }
    public List<TicketQueryDto> Tickets { get; set; } = new List<TicketQueryDto>();
    public List<UsuarioQueryDto> Usuarios { get; set; } = new List<UsuarioQueryDto>();
}

public class ProyectoCommandDto
{
    public required string Nombre { get; set; }
    public required string Descripcion { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
}

