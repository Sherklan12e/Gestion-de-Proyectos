namespace Consola;

public class BloggingContext : BdContext
{
  public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source=db/sqlite.db");
}
