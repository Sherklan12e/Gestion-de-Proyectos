namespace biblioteca;

public class Comentario{
    public int Id { get; set; }
    public Ticket ticket { get; set; }
    public Usuario usuario  {get; set; }
    public string contenido {get; set; }
    public DateTime Fecha {get; set; }
    
}