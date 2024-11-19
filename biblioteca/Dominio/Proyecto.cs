using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace biblioteca.Dominio;

[Table("Proyecto")]
public class Proyecto : Auditoria
{
    [Key]
    public Guid Id { get; set; }= Guid.NewGuid();
    [Required]
    [StringLength(50)]
    public required string Nombre { get; set; }
    [Required]
    [StringLength(50)]
    public required string Descripcion { get; set; }
    [ForeignKey("IdProyecto")]
    public List<Usuario>? Usuarios { get; set; } = [];
   
    public List<Ticket>? Tickets { get; set; } = [];
}
