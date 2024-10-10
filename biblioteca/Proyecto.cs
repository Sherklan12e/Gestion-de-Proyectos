namespace biblioteca;

public class Proyecto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public List<Usuario> Usuarios { get; set; } = new List<Usuario>();

    public List<Ticket> Tickets { get; set; } = new List<Ticket>();
}
