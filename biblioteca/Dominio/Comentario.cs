using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace biblioteca.Dominio;

[Table("Comentario")]
public class Comentario: Auditoria
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [ForeignKey("Ticket")]
    public Guid Ticket { get; set; }
    [ForeignKey("Usuario")]
    public Guid Usuario  {get; set; }

    [Required]
    [StringLength(50)]
    public required string Contenido {get; set; }
    [Timestamp]
    public DateTime Fecha {get; set; }
    
}