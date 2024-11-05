namespace Api.Funcionalidades.Comentarios;
using Api.Funcionalidades.Usuarios;
using Api.Funcionalidades.Tickets;

public class ComentarioQueryDto : ComentarioCommandDto
{
    public Guid Id { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime Fecha { get; set; }
    public UsuarioQueryDto? Usuario { get; set; }
    public TicketQueryDto? Ticket { get; set; }
}

public class ComentarioCommandDto
{
    public required string Contenido { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid TicketId { get; set; }
}