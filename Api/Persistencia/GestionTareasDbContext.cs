using Microsoft.EntityFrameworkCore;
using biblioteca.Dominio;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace Api.Persistencia;

public class GestionTareasDbContext : DbContext
{
    public GestionTareasDbContext(DbContextOptions<GestionTareasDbContext> options) : base(options) { 

    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Comentario> Comentarios { get; set; }
    public DbSet<Proyecto> Proyectos { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Usuario>().HasData(
            new Usuario { Nombre =  "juan", Email = "juan@gmail.com", Password= "1234"},
            new Usuario { Nombre =  "leon", Email = "leon@gmail.com", Password= "1234"}
        );
    }


}
