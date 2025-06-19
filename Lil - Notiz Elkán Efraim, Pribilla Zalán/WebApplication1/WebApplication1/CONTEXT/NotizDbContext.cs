using CommonLibrary;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.CONTEXT
{
    public class NotizDbContext : DbContext
    {
        public NotizDbContext(DbContextOptions<NotizDbContext> options) : base(options)
        {

        }

        public DbSet<Notiz> Notiz { get; set; }
    }
}
