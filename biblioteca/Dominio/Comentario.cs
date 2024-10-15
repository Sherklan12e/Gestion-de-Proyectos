using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace biblioteca.Dominio;

[Table("Comentario")]
public class Comentario: Auditoria
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Ticket")]
    public required int Ticket { get; set; }
    [ForeignKey("Usuario")]
    public required int Usuario  {get; set; }
    [Required]
    [StringLength(50)]
    public required string Contenido {get; set; }
    [Timestamp]
    public DateTime Fecha {get; set; }
    
}