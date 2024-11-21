namespace Api.Funcionalidades.Comentarios;

public class ComentarioQueryDto : ComentarioCommandDto
{
    public Guid Id { get; set; }
    public DateTime FechaCreacion { get; set; }
}

public class ComentarioCommandDto
{
    public required string Contenido { get; set; }
    public Guid TicketId { get; set; }
    public Guid CreacionUsuario {get;set;}
}