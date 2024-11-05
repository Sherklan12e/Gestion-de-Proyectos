namespace Api.Funcionalidades.Tickets;
using Api.Funcionalidades.Comentarios;
using Api.Funcionalidades.Usuarios;

public class TicketQueryDto : TicketCommandDto
{
    public Guid Id { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public UsuarioQueryDto? Usuario { get; set; }
    public List<ComentarioQueryDto> Actividad { get; set; } = [];
}

public class TicketCommandDto
{
    public required string Nombre { get; set; }
    public required string Descripcion { get; set; }
    public string Estado { get; set; } = "Abierto";
    public Guid UsuarioAsignadoId { get; set; }
    public Guid ProyectoId {get;set;}


}