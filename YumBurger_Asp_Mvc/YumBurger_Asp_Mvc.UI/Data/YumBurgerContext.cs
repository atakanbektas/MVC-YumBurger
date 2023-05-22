using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YumBurger_Asp_Mvc.UI.Models;

namespace YumBurger_Asp_Mvc.UI.Data
{
    public class YumBurgerContext : IdentityDbContext<IdentityUser>
    {
        public YumBurgerContext()
        {

        }

        public YumBurgerContext(DbContextOptions<YumBurgerContext> options)
            : base(options)
        {

        }
        public virtual DbSet<Extra> Extras { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrdersExtra> OrdersExtras { get; set; } = null!;
        public virtual DbSet<OrdersMenu> OrdersMenus { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Extra>(entity =>
            {
                entity.HasIndex(e => e.Name, "UK_Extras_Name")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PicturePath).HasMaxLength(500);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_Menus")
                    .IsUnique();


                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PicturePath).HasMaxLength(500);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.ActualArrivalDate).HasColumnType("datetime");

                entity.Property(e => e.EstimatedArrivalDate).HasColumnType("datetime");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UserAddress).HasMaxLength(500);

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Orders_AppUsers");
            });

            modelBuilder.Entity<OrdersExtra>(entity =>
            {
                //entity.HasKey(e => new { e.OrderId, e.ExtraId });

                entity.HasOne(d => d.Extra)
                    .WithMany(p => p.OrdersExtras)
                    .HasForeignKey(d => d.ExtraId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_OrdersExtras_Extras");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrdersExtras)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_OrdersExtras_Orders");
            });

            modelBuilder.Entity<OrdersMenu>(entity =>
            {
                //entity.HasKey(e => new { e.OrderId, e.MenuId });

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.OrdersMenus)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_OrdersMenus_Menus");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrdersMenus)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_OrdersMenus_Orders");
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Address).HasMaxLength(50);
            });

            base.OnModelCreating(modelBuilder);

        }

    }
}
