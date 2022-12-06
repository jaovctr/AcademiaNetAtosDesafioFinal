using BuscoBicoBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BuscoBicoBackEnd
{
    public class Context : DbContext
    {
        public DbSet<Prestador> Prestadores { get; set; }
        public DbSet<Cliente> Clientes { get; set;}
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost; Initial Catalog=BuscoBicoDB;User ID=usuario;password=senha;
                language=Portuguese;Trusted_Connection=True;TrustServerCertificate=True;");
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>().HasOne(p => p.Prestador)
                .WithMany(r => r.Reviews)
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<Review>().HasOne(p => p.Autor).WithMany(c=>c.Reviews)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
