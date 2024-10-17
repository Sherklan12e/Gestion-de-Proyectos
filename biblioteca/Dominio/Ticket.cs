using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace biblioteca.Dominio;


[Table("Ticket")]
public class Ticket
{
    [Key]
    public Guid Id { get; set; }= Guid.NewGuid();
    [ForeignKey("Usuario")]
    public Guid  Usuario  { get; set; }
    public required string Nombre { get; set; }
    [Required]
    [StringLength(50)]
    public required string Descripcion { get; set; }
    [Required]
    [StringLength(40)]
    public string Estado { get; set; } = "Abierto";
    [Timestamp]
    public DateTime FechaInicio { get; set; }
    [Timestamp]
    public DateTime? FechaFin { get; set; }
    public List<Comentario> Actividad { get; set; } = [];


    public void CambiarEstado(string nuevoEstado)
    {
        if (!string.IsNullOrEmpty(nuevoEstado))
        {
            Estado = nuevoEstado;
        }
        else
        {
            throw new Exception("El estado no puede ser vacío");
        }
    }
    public void ModificarInformacion(string? nombre = null, string? descripcion = null, Usuario? usuarioAsignado = null)
    {
        // if (nombre != null)
        //     Nombre = nombre;

        // if (descripcion != null)
        //     Descripcion = descripcion;

        // if (usuarioAsignado != null)
        //     Usuario = usuarioAsignado;
        Console.WriteLine("Modificar informacion");
    }

    public void BorrarTicket()
    {
        Console.WriteLine("eliminado");
    }

    public void AgregarComentario(Usuario unnusuario, string contenido, Ticket unticket)
    {
        // Actividad.Add(new Comentario()
        // {
        //     Ticket = unticket,
        //     Usuario = unnusuario,
        //     Contenido = contenido,
        //     Fecha = DateTime.Now,
        //     FechaCreacion = DateTime.Now,
        //     CreacionUsuario = unnusuario.Nombre
            
        // });
        Console.WriteLine("Agregar");
    }

}
