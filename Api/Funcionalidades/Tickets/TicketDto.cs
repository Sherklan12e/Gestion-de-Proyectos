using System.ComponentModel.DataAnnotations;
using Api.Funcionalidades.Comentarios;
using Api.Funcionalidades.Usuarios;

namespace Api.Funcionalidades.Tickets;
public class TicketQueryDto : TicketCommandDto
{
    public Guid Id { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public Guid Usuario { get; set; }
    public List<ComentarioQueryDto> Actividad { get; set; } = [];
}

public class TicketCommandDto
{
    [Required]
    public required string Nombre { get; set; }
    [Required]
    public required string Descripcion { get; set; }
    
    public string? Estado { get; set; }
    [Required]
    public Guid UsuarioAsignadoId { get; set; }
    [Required]
    public Guid ProyectoId {get;set;}


}