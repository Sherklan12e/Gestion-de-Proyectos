using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace biblioteca.Dominio;

[Table("Usuario")]
public class Usuario : Auditoria
{
    [Key]
    public int Id { get; set;}
    [Required]
    [StringLength(24)]
    public required string Nombre {get;set;}
    [Required]
    [StringLength(50)]
    public required string Password{get;set;}
    [Required]
    [StringLength(50)]
    public required string Email {get;set;}
    public List<Proyecto>? ProyectoAsignados {get; set;} = [];
    public List<Comentario>? ComentariosUsuario {get; set;} = [];
    
}
