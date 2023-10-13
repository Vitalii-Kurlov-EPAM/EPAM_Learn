using Microsoft.EntityFrameworkCore;
using Module_02.Task_02.DAL.Entities;
using Сommon.DB.Abstractions;
using Сommon.DB.Exceptions;

namespace Module_02.Task_02.DAL.Context;

public class WithTrackingStudyDbContext : DbContext, IWithTrackingStudyDbContext
{
    private readonly IDbConnectionSettings _connectionStrings;
    public WithTrackingStudyDbContext(IDbConnectionSettings connectionStrings)
    {
        _connectionStrings = connectionStrings;
    }

    public DbSet<ProductEntity> Products { get; set; }
    
    public DbSet<CategoryEntity> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionStrings.ConnectionString);
        base.OnConfiguring(optionsBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new DbException(ex.Message, ex);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryEntity>(entity =>
        {
            entity.Property(e => e.CategoryId)
                .HasColumnName("category_id")
                .ValueGeneratedOnAdd();

            entity.HasKey(e => e.CategoryId)
                .HasName("PK_Categories");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsUnicode();

            entity.Property(e => e.Image)
                .HasColumnName("image")
                .HasMaxLength(50)
                .IsUnicode();

            entity.Property(e => e.ParentCategoryId)
                .HasColumnName("parent_category_id");

            entity
                .HasOne(e => e.ParentCategory)
                .WithMany()
                .HasForeignKey(e => e.ParentCategoryId)
                .HasConstraintName("FK_ParentCategory_Category");
        });

        modelBuilder.Entity<ProductEntity>(entity =>
        {
            entity.Property(e => e.ProductId)
                .HasColumnName("product_id")
                .ValueGeneratedOnAdd();

            entity.HasKey(e => e.ProductId)
                .HasName("PK_Products");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsUnicode();

            entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(50)
                .IsUnicode();

            entity.Property(e => e.Image)
                .HasColumnName("image")
                .HasMaxLength(50)
                .IsUnicode();

            entity.Property(e => e.CategoryId)
                .IsRequired()
                .HasColumnName("category_id");

            entity.Property(e => e.Price)
                .IsRequired()
                .HasColumnName("price");

            entity.Property(e => e.Amount)
                .IsRequired()
                .HasColumnName("amount");

            entity
                .HasOne(e => e.Category)
                .WithMany()
                .HasForeignKey(e => e.CategoryId)
                .HasConstraintName("FK_Product_Category");
        });
    }
}