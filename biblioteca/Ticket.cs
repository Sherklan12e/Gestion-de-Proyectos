namespace biblioteca;

public class Ticket{
    public int Id { get; set; }
    public Usuario usuario { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string Estado { get; set; } = "Abierto";
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFini { get; set; }
    public List<Comentario> comentarios{ get; set; }=new List<Comentario>();

}
