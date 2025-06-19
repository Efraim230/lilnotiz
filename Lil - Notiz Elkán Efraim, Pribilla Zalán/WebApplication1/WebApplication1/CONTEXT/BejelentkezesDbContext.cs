using CommonLibrary;
using Microsoft.EntityFrameworkCore;


namespace WebApplication1.CONTEXT
{
    public class BejelentkezesDbContext : DbContext
    {
        public BejelentkezesDbContext( DbContextOptions<BejelentkezesDbContext> options ) : base (options)
        {
            
        }

        public DbSet<Felhasznalok2> felhasznalok2 { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Felhasznalok2>().Property(x => x.ID).ValueGeneratedOnAdd();

        }

    }
}
