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

    public virtual DbSet<Guide> Guides { get; set; }

    public virtual DbSet<StructUnit> StructUnits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Guide>(entity =>
        {
            entity.HasKey(e => new { e.IdDocument, e.IdStructUnit })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("guides");

            entity.HasIndex(e => e.IdStructUnit, "id_struct_unit");

            entity.Property(e => e.IdDocument)
                .HasMaxLength(20)
                .HasColumnName("id_document");
            entity.Property(e => e.IdStructUnit).HasColumnName("id_struct_unit");
            entity.Property(e => e.AcceptanceDate).HasColumnName("acceptance_date");
            entity.Property(e => e.CheckDate).HasColumnName("check_date");
            entity.Property(e => e.NameGuid)
                .HasMaxLength(90)
                .HasColumnName("name_guid");

            entity.HasOne(d => d.IdStructUnitNavigation).WithMany(p => p.Guides)
                .HasForeignKey(d => d.IdStructUnit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("guides_ibfk_1");
        });

        modelBuilder.Entity<StructUnit>(entity =>
        {
            entity.HasKey(e => e.IdUnit).HasName("PRIMARY");

            entity.ToTable("struct_units");

            entity.HasIndex(e => e.NameStruct, "name_struct").IsUnique();

            entity.Property(e => e.IdUnit)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_unit");
            entity.Property(e => e.NameStruct)
                .HasMaxLength(200)
                .HasColumnName("name_struct");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
