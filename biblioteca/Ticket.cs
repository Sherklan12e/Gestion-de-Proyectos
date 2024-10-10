namespace biblioteca;

public enum Tickets
{
    Abierto,
    EnProceso,
    Testing,
    Finalizado
}

public class Ticket
{
    public int Id { get; set; }
    public Usuario usuario { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public Tickets Estado { get; set; } = Tickets.Abierto;
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFini { get; set; }
    public List<Comentario> Actividad { get; set; } = new List<Comentario>();


    public void Iniciarproceso( )
    {
        if (Estado == Tickets.Abierto)
        {
            Estado = Tickets.EnProceso;
        }
        else
        {
            throw new Exception("El ticket no estaba abierto");
        }
    }

    public void IniciarTesting()
    {
        if (Estado == Tickets.Abierto)
        {
            Estado = Tickets.EnProceso;
        }
        else
        {
            throw new Exception("El ticket no estaba abierto");
        }
    }
    public void TerminarTesting()
    {
        if (Estado == Tickets.Abierto)
        {
            Estado = Tickets.EnProceso;
        }
        else
        {
            throw new Exception("El ticket no estaba abierto");
        }
    }
    public void ModificarInformacion(string? nombre = null, string? descripcion = null, Usuario? usuarioAsignado = null)
        {
            if (nombre != null)
                Nombre = nombre;

            if (descripcion != null)
                Descripcion = descripcion;

            if (usuarioAsignado != null)
                usuario = usuarioAsignado;
        }

    public void BorrarTicket()
    {
        Console.WriteLine("eliminado");
    }

    public void AgregarComentario(Usuario unnusuario, string contenido, Ticket unticket)
    {
        Actividad.Add(new Comentario(){
            ticket =  unticket,
            usuario = unnusuario,
            contenido = contenido,
            Fecha =  DateTime.Now
        });
    }

}
