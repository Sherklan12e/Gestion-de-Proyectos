using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace biblioteca.Dominio;


[Table("Ticket")]
public class Ticket : Auditoria
{
    [Key]
    public Guid Id { get; set; }= Guid.NewGuid();
    [ForeignKey("Usuario")]
    public Guid  Usuario  { get; set; }
    
    [ForeignKey("Proyecto")]
    public Guid Proyecto {get;set;}

    public required string Nombre { get; set; }
    [Required]
    [StringLength(50)]
    public required string Descripcion { get; set; }
    [Required]
    [StringLength(40)]
    public string Estado { get; set; }= "Abierto";
   
    public DateTime? FechaInicio { get; set; }
   
    public DateTime? FechaFin { get; set; }
    
    [ForeignKey("Ticket")]
    public List<Comentario> Actividad { get; set; } = [];


}
