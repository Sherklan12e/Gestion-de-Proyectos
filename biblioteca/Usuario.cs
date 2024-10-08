namespace biblioteca;
public class Usuario{
    public int Id { get; set;}
    public string nombre {get;set;}
    public string Password{get;set;}
    public string Email {get;set;}
    public List<Proyecto> ProyectoAsignados {get; set;}= new List<Proyecto>();
    public List<Comentario> ComentariosUsuario {get; set;}=new List<Comentario>();
    
}
