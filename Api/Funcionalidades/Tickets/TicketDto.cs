namespace Api.Funcionalidades.Tickets;
using Api.Funcionalidades.Comentarios;
public class TicketQueryDto : TicketCommandDto
{
    public Guid Id { get; set; }
    public DateTime FechaCreacion { get; set; }
    public Guid UsuarioAsignadoId { get; set; }
    public Guid ProyectoId { get; set; }
    public List<ComentarioQueryDto> Comentarios { get; set; } = new List<ComentarioQueryDto>();
}

public class TicketCommandDto
{
    public required string Titulo { get; set; }
    public required string Descripcion { get; set; }
    public required string Estado { get; set; }
    public required string Prioridad { get; set; }
}

