   using Microsoft.EntityFrameworkCore;

   
   namespace biblioteca
   {
       public class ApplicationDbContext : DbContext
       {
           public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

           public DbSet<Usuario> Usuarios { get; set; }
       }
   }