using Microsoft.EntityFrameworkCore;
using PreSaleForm.Application.Common.Interfaces;
using PreSaleForm.Domain.Entities;

namespace PreSaleForm.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<PreSaleFormEntity> PreSaleForms { get; set; }
    public DbSet<PreSaleFormProduct> PreSaleFormProducts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // PreSaleForm
        modelBuilder.Entity<PreSaleFormEntity>(entity =>
        {
            entity.ToTable("PreSaleForms");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.CustomerFullName)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.CustomerPhone)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(x => x.Note)
                .HasMaxLength(1000);

            entity.Property(x => x.PdfFilePath)
                .HasMaxLength(300);

            // ✔ Form → Products ilişkisi
            entity.HasMany(x => x.Products)
                .WithOne(x => x.PreSaleForm)
                .HasForeignKey(x => x.PreSaleFormId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // PreSaleFormProduct
        modelBuilder.Entity<PreSaleFormProduct>(entity =>
        {
            entity.ToTable("PreSaleFormProducts");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.DoorModel)
                .HasMaxLength(150);

            entity.Property(x => x.DoorSurfaceType)
                .HasMaxLength(150);

            entity.Property(x => x.Color)
                .HasMaxLength(100);
        });

        // User
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(x => x.PasswordHash)
                .IsRequired()
                .HasMaxLength(256);

            entity.HasIndex(x => x.Email)
                .IsUnique();
        });

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<Product>()
            .Property(p => p.PriceWithAssembly)
            .HasColumnType("decimal(18,2)");
            
        modelBuilder.Entity<Product>()
            .Property(p => p.PriceWithoutAssembly)
            .HasColumnType("decimal(18,2)");
    }
}