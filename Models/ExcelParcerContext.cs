using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace server.Models;

public partial class ExcelParcerContext : DbContext
{
    public ExcelParcerContext()
    {
    }

    public ExcelParcerContext(DbContextOptions<ExcelParcerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoryGuide> CategoryGuides { get; set; }

    public virtual DbSet<Guide> Guides { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<CategoryGuide>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PRIMARY");

            entity.ToTable("category_guide");

            entity.HasIndex(e => e.NameCategory, "name_category").IsUnique();

            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.NameCategory)
                .HasMaxLength(200)
                .HasColumnName("name_category");
        });

        modelBuilder.Entity<Guide>(entity =>
        {
            entity.HasKey(e => e.IdRec).HasName("PRIMARY");

            entity.ToTable("guides");

            entity.HasIndex(e => e.IdCategory, "id_category");

            entity.HasIndex(e => e.IdDocumnet, "id_documnet").IsUnique();

            entity.Property(e => e.IdRec)
                .ValueGeneratedNever()
                .HasColumnName("id_rec");
            entity.Property(e => e.AcceptanceDate).HasColumnName("acceptance_date");
            entity.Property(e => e.CheckDate).HasColumnName("check_date");
            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.IdDocumnet)
                .HasMaxLength(20)
                .HasColumnName("id_documnet");
            entity.Property(e => e.NameGuid)
                .HasMaxLength(90)
                .HasColumnName("name_guid");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Guides)
                .HasForeignKey(d => d.IdCategory)
                .HasConstraintName("guides_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
