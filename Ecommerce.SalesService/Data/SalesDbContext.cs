using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecommerce.SalesService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;namespace Ecommerce.SalesService.Data
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.OrderId);
                entity.Property(o => o.Status).IsRequired().HasMaxLength(50);

                entity.HasMany(o => o.Items)
                      .WithOne()
                      .HasForeignKey(i => i.Id);
            });

            modelBuilder.Entity<OrderItem>(static entity =>
            {
                entity.HasKey(oi => oi.Id);
                entity.Property(oi => oi.Quantity).IsRequired();
                entity.Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)");
            });
        }

    }

}
