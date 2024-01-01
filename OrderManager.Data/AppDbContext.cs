

using Microsoft.EntityFrameworkCore;
using OrderManager.Data.Models;

namespace OrderManager.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        public DbSet<Order> Order { get; set; }
        public DbSet<Window> Window { get; set; }
        public DbSet<SubElement> SubElement { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            


            modelBuilder.HasDefaultSchema("Store");
            modelBuilder.Entity<Order>().Property(p => p.CreatedOn).HasDefaultValueSql("GetDate()").ValueGeneratedOnAdd();
            modelBuilder.Entity<Window>().Property(p => p.CreatedOn).HasDefaultValueSql("GetDate()").ValueGeneratedOnAdd();
            modelBuilder.Entity<SubElement>().Property(p => p.CreatedOn).HasDefaultValueSql("GetDate()").ValueGeneratedOnAdd();

            modelBuilder.Entity<Order>().Property(p => p.UpdatedAt).HasDefaultValueSql("GetDate()").ValueGeneratedOnUpdate();
            modelBuilder.Entity<Window>().Property(p => p.UpdatedAt).HasDefaultValueSql("GetDate()").ValueGeneratedOnUpdate();
            modelBuilder.Entity<SubElement>().Property(p => p.UpdatedAt).HasDefaultValueSql("GetDate()").ValueGeneratedOnUpdate();

            /*Configure Entities Columns */
            OrderConfiguration(modelBuilder);
            WindowConfiguration(modelBuilder);
            SubElementConfiguration(modelBuilder);

            /* Configure Entities Relationships */

            // Order => Window
            modelBuilder.Entity<Order>()
                .HasMany(e => e.Windows)
                .WithOne(e => e.Order)
                .HasForeignKey(e => e.OrderId)
                .HasPrincipalKey(e => e.Id);

            // Window => SubElement
            modelBuilder.Entity<Window>()
                .HasMany(e => e.SubElements)
                .WithOne(e => e.Window)
                .HasForeignKey(e => e.WindowId)
                .HasPrincipalKey(e => e.Id);


        }

        private static void OrderConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(u => u.Name).HasMaxLength(100).IsRequired();
                entity.Property(u => u.State).HasMaxLength(3).IsRequired();
            });
        }

        private static void WindowConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Window>(entity =>
            {
                entity.Property(u => u.Name).HasMaxLength(100).IsRequired();
                entity.Property(u => u.QuantityOfWindows).IsRequired().HasDefaultValue(0);
                entity.Property(u => u.TotalSubElements).HasDefaultValue(0);

            });
        }

        private static void SubElementConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubElement>(entity =>
            {
                entity.Property(u => u.Element).IsRequired();
                entity.Property(u => u.Type).HasMaxLength(20).IsRequired();
                entity.Property(u => u.Width).HasPrecision(18, 6).IsRequired();
                entity.Property(u => u.Height).HasPrecision(18, 6).IsRequired();
            });
        }
    }

}
