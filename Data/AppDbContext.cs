using Microsoft.EntityFrameworkCore;
using Prueba_Completa_NET.Models;

namespace Prueba_Completa_NET.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<DetalleOrden> DetallesOrden { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //Mapper
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Producto>().ToTable("Producto");
            modelBuilder.Entity<Orden>().ToTable("Orden");
            modelBuilder.Entity<DetalleOrden>().ToTable("DetalleOrden");

            // Configuración de relaciones
            modelBuilder.Entity<Orden>()
                .HasOne(o => o.Cliente)
                .WithMany(c => c.Ordenes)
                .HasForeignKey(o => o.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DetalleOrden>()
                .HasOne(d => d.Orden)
                .WithMany(o => o.Detalles)
                .HasForeignKey(d => d.OrdenId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DetalleOrden>()
                .HasOne(d => d.Producto)
                .WithMany(p => p.DetallesOrden)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Orden>()
                .Property(o => o.Total)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<DetalleOrden>()
                .Property(d => d.Subtotal)
                .HasColumnType("decimal(18,2)")
                .HasColumnType("decimal(18,2)");
        }
    }
}
