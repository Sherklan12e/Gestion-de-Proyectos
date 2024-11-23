using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace biblioteca.Dominio;

[Table("Usuario")]
public class Usuario : Auditoria
{
    [Key]
    public Guid Id { get; set;} = Guid.NewGuid();
    [Required]
    [StringLength(24)]
    public required string Nombre {get;set;}
    [Required]
    [StringLength(100)]
    public required string Password{get;set;}
    [Required]
    [StringLength(50)]
    public required string Email {get;set;}

    [ForeignKey("IdUsuario")]
    public List<Proyecto>? ProyectoAsignados {get; set;} = [];
   
    [ForeignKey("Usuario")]
    public List<Comentario>? ComentariosUsuario {get; set;} = [];
    public List<Ticket>? TicketsAsignados {get; set;} = [];
    
}
