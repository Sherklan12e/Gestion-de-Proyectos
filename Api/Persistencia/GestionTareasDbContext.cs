using Microsoft.EntityFrameworkCore;
using biblioteca.Dominio;
namespace Api.Persistencia;

public class GestionTareasDbContext : DbContext
{
    public GestionTareasDbContext(DbContextOptions<GestionTareasDbContext> options) : base(options) { 

    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Comentario> Comentarios { get; set; }
    public DbSet<Proyecto> Proyectos { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
}
