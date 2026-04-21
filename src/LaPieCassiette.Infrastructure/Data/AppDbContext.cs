namespace LaPieCassiette.Infrastructure.Data;
using LaPieCassiette.Domain.Models;
using Microsoft.EntityFrameworkCore;


public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<ProductTag> ProductTags => Set<ProductTag>();

    public DbSet<Formula> Formulas => Set<Formula>();

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 🔹 Product
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Description).IsRequired();

            entity.Property(p => p.ImagePath).IsRequired(false);

            entity.Property(p => p.IsPublished).IsRequired();
        });

        // 🔹 Tag
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasIndex(t => t.Name).IsUnique(); // ⚠️ important
        });

        // 🔹 ProductTag (many-to-many)
        modelBuilder.Entity<ProductTag>(entity =>
        {
            entity.HasKey(pt => new { pt.ProductId, pt.TagId });

            entity
                .HasOne(pt => pt.Product)
                .WithMany(p => p.ProductTags)
                .HasForeignKey(pt => pt.ProductId);

            entity
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.ProductTags)
                .HasForeignKey(pt => pt.TagId);
        });

        // 🔹 Formula
        modelBuilder.Entity<Formula>(entity =>
        {
            entity.HasKey(f => f.Id);

            entity.Property(f => f.Name).IsRequired();
            entity.Property(f => f.Price).HasColumnType("decimal(6,2)");
        });

        // 🔹 Order
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.Id);

            entity
                .HasOne(o => o.Formula)
                .WithMany()
                .HasForeignKey(o => o.FormulaId);

            entity.Property(o => o.TotalPrice)
                .HasColumnType("decimal(6,2)");
        });

        // 🔹 OrderItem
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(oi => oi.Id);

            entity
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId);

            entity
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);
        });
    }
}